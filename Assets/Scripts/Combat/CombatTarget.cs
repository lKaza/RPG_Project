﻿using System.Collections;
using System.Collections.Generic;
using RPG.Control;
using RPG.Resources;
using UnityEngine;


namespace RPG.Combat{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IRaycastable
    {
       [SerializeField] CursorType cursorType;

        public CursorType GetCursorType()
        {
            return cursorType;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
           
         
            if (!callingController.GetComponent<Fighter>().CanAttack(gameObject))
            {
                
                return false;
            }
            if (Input.GetMouseButton(0))
            {
              
                callingController.GetComponent<Fighter>().Attack(gameObject);
            
            }
            return true;
        }
      
    }
}

