using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;
namespace RPG.Stats{

    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] int experiencePoints = 0;

        
        public event Action onExperienceGained;
     
        void Update()
        {
         
        }

        public void GainExperience(int experience){
            experiencePoints += experience;
            onExperienceGained();

        }
        public int GetCurrentExp(){
            return experiencePoints;
        }
       

        public object CaptureState()
        {
            return experiencePoints;
        }

        public void RestoreState(object state)
        {
           experiencePoints = (int)state;
        }

    }

}