using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats{

    public class BaseStats : MonoBehaviour
    {
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression= null;
        [Tooltip("Only for monsters")]
        [Range(1,99)]
        [SerializeField] int monsterLevel = 2;

        private int currentLevel;

        private void Start() 
        {
            currentLevel = CalculateLevel();
            Experience experience = GetComponent<Experience>();
            if(experience != null){
             experience.onExperienceGained += UpdateLevel;   
            }
        }

       private void UpdateLevel() 
       {
          int newLevel = CalculateLevel();
          if(newLevel>currentLevel)
          {
              currentLevel = newLevel;
              print("Level up");
          }
          
       }

       public int GetStat(Stat stat){
           
           return progression.GetStat(stat,characterClass,GetLevel());
       }
        public int GetLevel()
        {
            if(currentLevel <1){
                currentLevel = CalculateLevel();
            }
            return currentLevel;
        }

        public int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();
           
            if(experience == null){
                return monsterLevel;
            }
            int currentXP = experience.GetCurrentExp();
            int penultimateLevel = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);
            for(int level = 1; level <= penultimateLevel ;level++)
            {
                
                int XPToLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass,level);
                if(XPToLevelUp>currentXP){
                   
                    return level;
                }
            }
            return penultimateLevel +1;
        }

    }
    
}
