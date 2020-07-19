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
        private void Awake()
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
            healthValue.text = String.Format("{0:0}%", health.GetComponent<Fighter>().GetTarget().GetComponent<Health>().getPercentage().ToString());

        }
    
}
}
