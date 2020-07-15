using UnityEngine;
namespace RPG.Combat{

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make NewWeapon", order = 0)]
public class Weapon : ScriptableObject {
        [SerializeField] AnimatorOverrideController animatorOverride;
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] int weaponDmg = 5;
        [SerializeField] float weaponRange = 2f;

        public void Spawn(Transform handTransform, Animator animator)
        {
            if(weaponPrefab != null)
            {
                Instantiate(weaponPrefab, handTransform);
            }
            if(animatorOverride != null){

            animator.runtimeAnimatorController = animatorOverride;
            }
        }
        public int GetWeaponDamage{
            get {return weaponDmg;}
        }
        public float GetWeapRange{
            get { return weaponRange;}
        }

    }
}