using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats{

    public class BaseStats : MonoBehaviour
    {
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression= null;
        [SerializeField] GameObject levelUpVFX;
        [Tooltip("Only for monsters")]
        [Range(1,99)]
        [SerializeField] int monsterLevel = 2;

        public event Action onLevelUp;

        private int currentLevel;
        Experience experience;

        private void Awake() {
            experience = GetComponent<Experience>();
        }

        private void Start() 
        {
            currentLevel = CalculateLevel();
        }

        private void OnEnable() {
           
            if(experience != null)
            {
             experience.onExperienceGained += UpdateLevel;   
            }
            
        }

        private void OnDisable() {
            if (experience != null)
            {
                experience.onExperienceGained -= UpdateLevel;
            }
        }


       private void UpdateLevel() 
       {
          int newLevel = CalculateLevel();
          if(newLevel>currentLevel)
            {
                currentLevel = newLevel;
                LevelUpEffect();
                onLevelUp();
            }

        }

        private void LevelUpEffect()
        {
            Instantiate(levelUpVFX, this.transform);
        }

        public float GetStat(Stat stat)
        {

            return GetBaseStat(stat) + GetAdditiveModifier(stat) * (1 + GetPercentageModifier(stat)/100);
        }


        private int GetBaseStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }

        private float GetAdditiveModifier(Stat stat)
        {
            float sum= 0;
           foreach(IModifierProvider provider in GetComponents<IModifierProvider>()){
                
               foreach(float modifier in provider.GetAdditiveModifers(stat) ){
                 sum += modifier;
               }
           }
           return sum;
        }
        private float GetPercentageModifier(Stat stat)
        {
            float sum = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float percentage in provider.GetPercentageModifiers(stat))
                {
                    sum += percentage;
                }
            }
            return sum;
        }

        public int GetLevel()
        {
            if(currentLevel <1){
                currentLevel = CalculateLevel();
            }
            return currentLevel;
        }

        private int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();
           
            if(experience == null){
                return monsterLevel;
            }
            float currentXP = experience.GetCurrentExp();
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
