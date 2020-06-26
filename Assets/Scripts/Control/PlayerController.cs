using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using System;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        
        // Start is called before the first frame update
        void Start()
        {
            
        }

        private static Ray GetClickRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        // Update is called once per frame
        void Update()
        {
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
                {
                    if(Input.GetMouseButtonDown(0)){
                        GetComponent<Fighter>().Attack(target);                    
                    }
                    return true;
                }
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
                
                GetComponent<Mover>().StartMoveAction(hit.point);
                    
                }
                return true;
            }
            return false;
        }
    }
}
