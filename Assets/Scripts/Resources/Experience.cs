using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;
namespace RPG.Resources{

    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiencePoints = 0;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        public void GainExperience(float experience){
            experiencePoints += experience;

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