
using System;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Stats{

[CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
public class Progression : ScriptableObject 
{
    [SerializeField] ProgressionCharacterClass[] characterClasses = null;
    
   Dictionary<CharacterClass, Dictionary<Stat, int[]>> lookupTable = null;

   public int GetStat(Stat stat,CharacterClass character, int level)
   {
       BuildLookup();
      int[] levels =  lookupTable[character][stat];

      if(levels.Length < level){
          return 0;
      }
      return levels[level -1];
 
   }
    public int GetLevels (Stat stat , CharacterClass characterClass){
        BuildLookup();

        int[] levels = lookupTable[characterClass][stat];
        return levels.Length;
    }

        private void BuildLookup()
        {
            if(lookupTable!= null) return;
            lookupTable = new Dictionary<CharacterClass, Dictionary<Stat, int[]>>();
            foreach (ProgressionCharacterClass classes in characterClasses)
            {    
                    var statTable = new Dictionary<Stat, int[]>();

                   
               foreach(ProgressionCharacterStats progressionCharacterStats in classes.stats){
                   statTable.Add(progressionCharacterStats.stat,progressionCharacterStats.levels);
                   
               }
                lookupTable.Add(classes.CharacterClase, statTable);
            }
        }

    
        

        [System.Serializable]
    class ProgressionCharacterClass {
       
        
        public CharacterClass CharacterClase;
        public ProgressionCharacterStats[] stats = null;
       

        public CharacterClass getClases(){
            return CharacterClase;
        }
       /* public int getLevelHealth(int level){
            //return Health[level];
       */ 

       public int GetStatList(Stat stat,int level){
           foreach(ProgressionCharacterStats characterstats in stats){
               if(characterstats.GetStatName() == stat){
                  
                   return characterstats.GetLevelStat(level);
               }
           }
           return 1;
        }
     }
    

        [System.Serializable]
        class ProgressionCharacterStats
        {        
            public Stat stat;
            public int[] levels;


            public Stat GetStatName(){
                return stat;
            }

            public int GetLevelStat(int level)
            {
                return levels[level-1];
            }
       
    }
}
}

