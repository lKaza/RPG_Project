using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Combat{

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make NewWeapon", order = 0)]
public class Weapon : ScriptableObject {
        [SerializeField] AnimatorOverrideController animatorOverride;
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] int weaponDmg = 5;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;


        public void Spawn(Transform leftHand ,Transform rightHand, Animator animator)
        {
            if(weaponPrefab != null)
            {
                Transform handTransform = GetHandTransform(leftHand, rightHand);
                Instantiate(weaponPrefab, handTransform);
            }
            if (animatorOverride != null){

            animator.runtimeAnimatorController = animatorOverride;
            }
        }

        private Transform GetHandTransform(Transform leftHand, Transform rightHand)
        {
            Transform handTransform;
            if (isRightHanded)
                handTransform = rightHand;
            else handTransform = leftHand;
            return handTransform;
        }

        public int GetWeaponDamage{
            get {return weaponDmg;}
        }
        public float GetWeapRange{
            get { return weaponRange;}
        }
        public void LaunchProjectile(Transform leftHand, Transform rightHand,Transform target)
        {
            Projectile projectileInstance = Instantiate(projectile,GetHandTransform(leftHand,rightHand).position,Quaternion.identity);
            projectileInstance.SetTarget(target,weaponDmg);
        }
        public bool HasProjectile()
        {
            return projectile != null;
        }
    }
}