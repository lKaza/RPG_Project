using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stats
{

    public class LevelDisplay : MonoBehaviour
    {
        [SerializeField] Text levelValue;
        

        BaseStats baseStats;
    
        private void Awake()
        {
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
           
        }



        private void Update()
        {
            levelValue.text = String.Format("{0:0}", baseStats.GetLevel());
            
         
        }
    }
}