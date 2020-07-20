using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Resources;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
public class EnemyHealthDisplay : MonoBehaviour
{
        [SerializeField] Text healthValue;

        Fighter health;
        private void Start()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }



        private void Update()
        {
            if(healthValue.GetComponent<Fighter>() == null){
                healthValue.text = "N/A";
                
            }
            if(health.GetComponent<Fighter>().GetTarget()== null){
                healthValue.text = "N/A";
                return;
            }
           
            healthValue.text = String.Format("{1}/{2} ({0:0})%", health.GetComponent<Fighter>().GetTarget().GetComponent<Health>().getPercentage().ToString(),
            health.GetComponent<Fighter>().GetTarget().GetComponent<Health>().getCurrentHP().ToString(),
            health.GetComponent<Fighter>().GetTarget().GetComponent<Health>().getMaxHP().ToString());

        }
    
}
}
