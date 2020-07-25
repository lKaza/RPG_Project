using System.Collections;
using System.Collections.Generic;
using GameDevTV.Utils;
using RPG.Core;
using RPG.Movement;
using RPG.Resources;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour,IAction , ISaveable, IModifierProvider
        {
            Animator myAnim;
            
            [SerializeField] float TimeBetweenAttacks = 1f; 
            [SerializeField] Transform rightHandTransform = null;
            [SerializeField] Transform leftHandTransform = null;
            [SerializeField] Weapon defaultWeapon = null;
            
           

            float timeSinceLastAttack = Mathf.Infinity;
            Transform target;
            public LazyValue<Weapon> currentWeapon;

        private void Awake() {
            myAnim = GetComponent<Animator>();
            currentWeapon = new LazyValue<Weapon>(EquiDefaultWeapon);
        }

        private void Start() {
            currentWeapon.ForceInit();
        }
        private Weapon EquiDefaultWeapon() {
            AttachWeapon(defaultWeapon);
             return defaultWeapon; 
           
        }

            private void Update() {
                timeSinceLastAttack +=Time.deltaTime;

                if (target == null) return;
                if (target.GetComponent<Health>().IsDead()) return;

                    GetComponent<Mover>().MoveTo(target.position,1f);
                    float distance = Vector3.Distance(transform.position,target.position);
                    if(distance <=currentWeapon.value.GetWeapRange)
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
            float damage = GetComponent<BaseStats>().GetStat(Stat.Damage);
            if (currentWeapon.value.HasProjectile()){
                currentWeapon.value.LaunchProjectile(leftHandTransform,rightHandTransform,target.transform,gameObject,damage);
                
            }else
                target.GetComponent<Health>().TakeDmg(damage,gameObject);
                               
            }
            
        void Shoot(){
            Hit();
        }

        public void EquipWeapon(Weapon weapon)
        {

            currentWeapon.value = weapon;
            AttachWeapon(weapon);

        }

        private void AttachWeapon(Weapon weapon)
        {
            weapon.Spawn(leftHandTransform, rightHandTransform, myAnim);
        }

        public Health GetTarget()
        {
            if(target == null) return null;
        
            return target.GetComponent<Health>();
        }

        public object CaptureState()
        {
           
            return currentWeapon.value.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;      
          Weapon weapon = UnityEngine.Resources.Load<Weapon>(weaponName);
         
          EquipWeapon(weapon);
        }

       
        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            if(stat == Stat.Damage){

            yield return currentWeapon.value.GetPercentageBonus;
            }
        }


        public IEnumerable<float> GetAdditiveModifers(Stat stat)
        {
            if(stat == Stat.Damage){
                yield return currentWeapon.value.GetWeaponDamage;
            }
        }
    }

        
}
