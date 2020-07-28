using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Combat{

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make NewWeapon", order = 0)]
public class WeaponConfig : ScriptableObject {
#pragma warning disable 0649
        [SerializeField] AnimatorOverrideController animatorOverride;
        [SerializeField] Weapon weaponPrefab = null;
        [SerializeField] float weaponDmg = 5;
        [SerializeField] float percentageBonus = 0;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;
        
#pragma warning disable 0649
        
        const string weaponName = "Weapon";
        public Weapon Spawn(Transform leftHand ,Transform rightHand, Animator animator)
        {
            Weapon weapon = null;
            DestroyOldWeapon(leftHand,rightHand);
            if(weaponPrefab != null)
            {
                Transform handTransform = GetHandTransform(leftHand, rightHand);
               weapon = Instantiate(weaponPrefab, handTransform);
               weapon.gameObject.name = weaponName;
            }
            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (animatorOverride != null){

            animator.runtimeAnimatorController = animatorOverride;
            }
            else if(overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }
            return weapon;
        }

        private Transform GetHandTransform(Transform leftHand, Transform rightHand)
        {
            Transform handTransform;
            if (isRightHanded)
                handTransform = rightHand;
            else handTransform = leftHand;
            return handTransform;
        }

        public float GetWeaponDamage{
            get {return weaponDmg;}
        }
        public float GetWeapRange{
            get { return weaponRange;}
        }
        public float GetPercentageBonus{
            get { return percentageBonus;}
        }

        public void LaunchProjectile(Transform leftHand, Transform rightHand,Transform target,GameObject instigator, float calculatedDmg)
        {
            Projectile projectileInstance = Instantiate(projectile,GetHandTransform(leftHand,rightHand).position,Quaternion.identity);
           
            projectileInstance.SetTarget(target,calculatedDmg,instigator);
        }
        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void DestroyOldWeapon(Transform leftHand,Transform rightHand){
            Transform oldWeapon = rightHand.Find(weaponName);
            if(oldWeapon == null){
                oldWeapon = leftHand.Find(weaponName);
            }
            if( oldWeapon == null) return;
            oldWeapon.name = "Destroying";
            Destroy(oldWeapon.gameObject);
        }
      
    }
}