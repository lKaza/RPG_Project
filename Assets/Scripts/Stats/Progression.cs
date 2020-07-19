
using UnityEngine;
namespace RPG.Stats{

[CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
public class Progression : ScriptableObject 
{
    [SerializeField] ProgressionCharacterClass[] characterClasses = null;
    
   

   public int GetStat(Stat stat,CharacterClass character, int level)
   {
       foreach(ProgressionCharacterClass classes in characterClasses){
           if(classes.getClases()!=character){
              continue;              //return classes.getLevelHealth(level-1);
           } return classes.GetStatList(stat,level);
           
       }
       return 15;
   }


    [System.Serializable]
    class ProgressionCharacterClass {
       
        
        [SerializeField] CharacterClass CharacterClase;
        [SerializeField] ProgressionCharacterStats[] stats = null;
       

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

