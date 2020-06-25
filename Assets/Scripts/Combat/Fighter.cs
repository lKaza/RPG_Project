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
            float timeSinceLastAttack = 0;

            Transform target;
        private void Start() {
           
            myAnim = GetComponent<Animator>();
           
        }

            private void Update() {
                timeSinceLastAttack +=Time.deltaTime;

                if(target != null){

                    GetComponent<Mover>().MoveTo(target.position);
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
        }

        private void AttackBehaviour()
        {
            myAnim.SetTrigger("Attack");
            
        }

        public void Attack(CombatTarget combatTarget){
                
                GetComponent<Scheduler>().StartAction(this);
                target = combatTarget.transform;
            }
            public void Disengage(){
                if(target == null) return;
            
                target = null;
            }

            //Animation event
            void Hit(){
                    target.GetComponent<Health>().TakeDmg(weaponDmg);
            }                   
        }
}
