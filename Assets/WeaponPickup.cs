using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Control;
using UnityEngine;

namespace RPG.Combat{


public class WeaponPickup : MonoBehaviour,IRaycastable
{
        [SerializeField] Weapon weaponPrefab = null;
        [SerializeField] float respawnTime = 3f;
        [SerializeField] CursorType cursorType;

        public CursorType GetCursorType()
        {
            return cursorType;
        }


        public bool HandleRaycast(PlayerController controller)
        {
            if(Input.GetMouseButtonDown(0)){
                Pickup(controller.GetComponent<Fighter>());
            }  
            return true;
        }

        private void OnTriggerEnter(Collider other) {
        
        if(other.tag=="Player")
            {
                Pickup(other.GetComponent<Fighter>());
            }

        }

        private void Pickup(Fighter other)
        {
           

            other.EquipWeapon(weaponPrefab);
            StartCoroutine(Respawn());
            
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