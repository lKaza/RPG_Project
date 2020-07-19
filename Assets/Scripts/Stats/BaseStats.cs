using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats{

     class BaseStats : MonoBehaviour
    {
        [Range(1,99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression= null;

       
       public int GetStat(Stat stat){
           return progression.GetStat(stat,characterClass,startingLevel);
           
       }
    }
    
}
