using System.Collections;
using System.Collections.Generic;
using GameDevTV.Utils;
using RPG.Core;
using RPG.Movement;
using RPG.Attributes;
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
            [SerializeField] WeaponConfig defaultWeaponConfig = null;
            
            Transform target;
            float timeSinceLastAttack = Mathf.Infinity;
            public WeaponConfig currentWeaponConfig;
            LazyValue<Weapon> currentWeapon;

        private void Awake() {
            myAnim = GetComponent<Animator>();
            currentWeaponConfig = defaultWeaponConfig;
            currentWeapon = new LazyValue<Weapon>(EquipDefaultWeapon);
        }

        private void Start() {
         currentWeapon.ForceInit();
        }

        private Weapon EquipDefaultWeapon() {
           return AttachWeapon(defaultWeaponConfig);
        }

            private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.GetComponent<Health>().IsDead()) return;

            GetComponent<Mover>().MoveTo(target.position, 1f);
            if(IsInRange(target.transform)){

            GetComponent<Mover>().Disengage();
            if (timeSinceLastAttack >= TimeBetweenAttacks)
            {
                //Triggers Hit() animation dmg
                AttackBehaviour();
                timeSinceLastAttack = 0;
            }
            }



        }

        private bool IsInRange(Transform targetTransform)
        {
           
            return Vector3.Distance(transform.position, targetTransform.position) < currentWeaponConfig.GetWeapRange;
            
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
            if(!GetComponent<Mover>().CanMoveTo(combatTarget.transform.position) 
            && !IsInRange(combatTarget.transform)
            ){
                return false;
            }
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
            if(currentWeapon.value !=null){
                currentWeapon.value.OnHit();
            }
            if (currentWeaponConfig.HasProjectile()){
                currentWeaponConfig.LaunchProjectile(leftHandTransform,rightHandTransform,target.transform,gameObject,damage);
                
            }else{
                    target.GetComponent<Health>().TakeDmg(damage,gameObject);
                 }                    
            }
            
        void Shoot(){
            Hit();
        }

        public void EquipWeapon(WeaponConfig weapon)
        {

            currentWeaponConfig = weapon;
            currentWeapon.value = AttachWeapon(weapon);

        }

        private Weapon AttachWeapon(WeaponConfig weapon)
        {
            return weapon.Spawn(leftHandTransform, rightHandTransform, myAnim);
        }

        public Health GetTarget()
        {
            if(target == null) return null;
        
            return target.GetComponent<Health>();
        }

        public object CaptureState()
        {
           
            return currentWeaponConfig.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;      
          WeaponConfig weapon = UnityEngine.Resources.Load<WeaponConfig>(weaponName);
         
          EquipWeapon(weapon);
        }

       
        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            if(stat == Stat.Damage){

            yield return currentWeaponConfig.GetPercentageBonus;
            }
        }


        public IEnumerable<float> GetAdditiveModifers(Stat stat)
        {
            if(stat == Stat.Damage){
                yield return currentWeaponConfig.GetWeaponDamage;
            }
        }
    }

        
}
