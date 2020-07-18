using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat{


public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Weapon weaponPrefab = null;
        [SerializeField] float respawnTime = 3f;
        

    private void OnTriggerEnter(Collider other) {
        
        if(other.tag=="Player"){        
         other.GetComponent<Fighter>().EquipWeapon(weaponPrefab);
            StartCoroutine(Respawn());
        }
        
    }
    private IEnumerator Respawn(){
        TimeoutPickUp(false); //hide
        yield return new WaitForSeconds(respawnTime);
        TimeoutPickUp(true); //show
       
    }

        private void TimeoutPickUp(bool state)
        {
            GetComponent<SphereCollider>().enabled = state;
            foreach(Transform child in transform ){
                child.gameObject.SetActive(state);
            }
        }

       
    }
}