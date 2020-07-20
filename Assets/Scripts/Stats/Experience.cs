﻿using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;
namespace RPG.Stats{

    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiencePoints = 0;

        
        public event Action onExperienceGained;
     
        void Update()
        {
         
        }

        public void GainExperience(float experience){
            experiencePoints += experience;
            onExperienceGained();

        }
        public float GetCurrentExp(){
            return experiencePoints;
        }
       

        public object CaptureState()
        {
            return experiencePoints;
        }

        public void RestoreState(object state)
        {
           experiencePoints = (float)state;
        }

    }

}