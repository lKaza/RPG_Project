using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat{


public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Weapon weaponPrefab = null;

    private void OnTriggerEnter(Collider other) {
        
        if(other.tag=="Player"){        
         other.GetComponent<Fighter>().EquipWeapon(weaponPrefab);
            Destroy(this.gameObject);
        }
        
    }
}
}