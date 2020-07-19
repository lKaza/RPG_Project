using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Resources{

    public class HealthDisplay : MonoBehaviour
    {
        [SerializeField] Text healthValue;
        double todecimal;
        Health health;
        private void Awake() {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();        
        }



        private void Update() {
            todecimal = Math.Truncate(health.getPercentage()*100)/100;
            todecimal = Math.Truncate(todecimal);
           

            healthValue.text = String.Format("{0:0}%", todecimal.ToString());
        }
    }
}