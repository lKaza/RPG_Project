using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour,IAction
        {
            Animator myAnim;
            
            [SerializeField] float weaponRange =2f;
            [SerializeField] float TimeBetweenAttacks = 1f;
            [SerializeField] int weaponDmg=5;
            float timeSinceLastAttack = Mathf.Infinity;

            Transform target;
        private void Start() {
           
            myAnim = GetComponent<Animator>();
           
        }

            private void Update() {
                timeSinceLastAttack +=Time.deltaTime;

                if (target == null) return;
                if (target.GetComponent<Health>().IsDead()) return;

                    GetComponent<Mover>().MoveTo(target.position,1f);
                    float distance = Vector3.Distance(transform.position,target.position);
                    if(distance <=weaponRange)
                {
                    GetComponent<Mover>().Disengage();
                    if(timeSinceLastAttack>=TimeBetweenAttacks)
                    {
                        //Triggers Hit() animation dmg
                        AttackBehaviour();
                        timeSinceLastAttack = 0;
                    }
                }
                
            
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target);

            TriggerAttack();

        }

        private void TriggerAttack()
        {
            myAnim.ResetTrigger("stopAttacking");
            myAnim.SetTrigger("Attack");
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) { return false; }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(GameObject combatTarget)
        {              
                GetComponent<Scheduler>().StartAction(this);    
                target = combatTarget.transform;            
        }
      

            public void Disengage()
        {
            if (target == null) return;
           GetComponent<Mover>().Disengage();
            TriggerStopAttacking();
            target = null;
        }

        private void TriggerStopAttacking()
        {
            myAnim.ResetTrigger("Attack");
            myAnim.SetTrigger("stopAttacking");
        }

        //Animation event
        void Hit(){
            if(target == null) return;
                target.GetComponent<Health>().TakeDmg(weaponDmg);       
            }                   
        }
}
