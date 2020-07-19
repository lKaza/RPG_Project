using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{

    public class ExperienceDisplay : MonoBehaviour
    {
        [SerializeField] Text expValue;
      

        Experience exp;
    
        private void Awake()
        {
            exp = GameObject.FindWithTag("Player").GetComponent<Experience>();
           
        }



        private void Update()
        {
            expValue.text = String.Format("{0:0}", exp.GetCurrentExp().ToString());
            
         
        }
    }
}