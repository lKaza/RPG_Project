using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Resources;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

        private static Ray GetClickRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        // Update is called once per frame
        void Update()
        {
            if (GetComponent<Health>().IsDead()) return;
            if(InteractWithCombat()) return;
            if(InteractWithMovement()) return;
            
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetClickRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                if(!GetComponent<Fighter>().CanAttack(target.gameObject))
                {
                    continue;
                }
                    if(Input.GetMouseButton(0)){
                        GetComponent<Fighter>().Attack(target.gameObject);                    
                    }
                    return true;
                
            }return false;
        }

      
        private bool InteractWithMovement()
        {
            RaycastHit hit;
            
            bool hasHit = Physics.Raycast(GetClickRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                
                GetComponent<Mover>().StartMoveAction(hit.point,1f);
                    
                }
                return true;
            }
            return false;
        }
    }
}
