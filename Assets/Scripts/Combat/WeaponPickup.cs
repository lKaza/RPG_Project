using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
using RPG.Control;
using UnityEngine;

namespace RPG.Combat{


public class WeaponPickup : MonoBehaviour,IRaycastable
{
        [SerializeField] WeaponConfig weaponPrefab = null;
        [SerializeField] float healthToRestore = 0;
        [SerializeField] float respawnTime = 3f;
        [SerializeField] CursorType cursorType;

        public CursorType GetCursorType()
        {
            return cursorType;
        }


        public bool HandleRaycast(PlayerController controller)
        {
            if(Input.GetMouseButtonDown(0)){
                Pickup(controller.gameObject);
            }  
            return true;
        }

        private void OnTriggerEnter(Collider other) {
        
        if(other.tag=="Player")
            {
                Pickup(other.gameObject);
            }

        }

        private void Pickup(GameObject subject)
        {
           if(weaponPrefab != null){
            subject.GetComponent<Fighter>().EquipWeapon(weaponPrefab);

           }
           if(healthToRestore>0){
               subject.GetComponent<Health>().Heal(healthToRestore);
           }
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