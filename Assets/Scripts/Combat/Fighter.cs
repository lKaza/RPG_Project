using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Movement;
using RPG.Saving;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour,IAction , ISaveable
        {
            Animator myAnim;
            
            [SerializeField] float TimeBetweenAttacks = 1f; 
            [SerializeField] Transform rightHandTransform = null;
            [SerializeField] Transform leftHandTransform = null;
            [SerializeField] Weapon defaultWeapon = null;
            
           

            float timeSinceLastAttack = Mathf.Infinity;
            Transform target;
            public Weapon currentWeapon;

        private void Awake() {
            myAnim = GetComponent<Animator>();
            if (currentWeapon == null)
            {
                EquipWeapon(defaultWeapon);
            }
        }

        private void Start() {      
              
           
        }

            private void Update() {
                timeSinceLastAttack +=Time.deltaTime;

                if (target == null) return;
                if (target.GetComponent<Health>().IsDead()) return;

                    GetComponent<Mover>().MoveTo(target.position,1f);
                    float distance = Vector3.Distance(transform.position,target.position);
                    if(distance <=currentWeapon.GetWeapRange)
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
            if (currentWeapon.HasProjectile()){
                currentWeapon.LaunchProjectile(leftHandTransform,rightHandTransform,target.transform);
                
            }else
                target.GetComponent<Health>().TakeDmg(currentWeapon.GetWeaponDamage);
                               
            }
        void Shoot(){
            Hit();
        }

        public void EquipWeapon(Weapon weapon)
        {
            
            currentWeapon = weapon;
            weapon.Spawn(leftHandTransform,rightHandTransform,myAnim);

        }

        public object CaptureState()
        {
            print("salvando.."+currentWeapon.name);
            return currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;      
          Weapon weapon = Resources.Load<Weapon>(weaponName);
          print(weapon);
          EquipWeapon(weapon);
        }
    }

        
}
