using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;

namespace RPG.Resources{


public class Health : MonoBehaviour, ISaveable
{
        Animator myAnim;
        float maxHealth=-1;
        public float currentHealth;
        bool isDead = false;

        // Start is called before the first frame update
        private void Awake() {
                myAnim = GetComponent<Animator>();
        }
        
        private void Start()
        {
            if(maxHealth  <0){

            maxHealth = GetComponent<BaseStats>().GetStat(Stat.Health);
            currentHealth = maxHealth;
            
            }
            
        }
        private void OnEnable() {
           GetComponent<BaseStats>().onLevelUp += LevelUpRegen;
            
        }
        private void OnDisable() {
            GetComponent<BaseStats>().onLevelUp -= LevelUpRegen;
        }

        public void TakeDmg(float dmg, GameObject instigator)
        {
           
            currentHealth = currentHealth - dmg;
            if(currentHealth<=0 && !isDead)
            {
                myAnim.ResetTrigger("resurrect");
                Die();
                GiveEXP(instigator);
            }
        }

        private void Update() {
            if(isDead){
                currentHealth = 0;
            }
        }
        private void GiveEXP(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if(experience == null){
                return;
            }
            
            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        public bool IsDead(){
           return isDead;
       }
        private void Die()
        {
            
            isDead = true;
            myAnim.SetTrigger("isAlive");
            GetComponent<Scheduler>().CancelCurrentAction();
        }

        public object CaptureState()
        {
            float[] HPStats = {maxHealth,currentHealth};
           
            return HPStats;
        }

        public void RestoreState(object state)
        {
            float[] hpStats = (float[])state;
            maxHealth = hpStats[0];
            currentHealth = hpStats[1];
            if(currentHealth>0){
                isDead = false;
                myAnim.SetTrigger("resurrect");
            }
            if (currentHealth <= 0 && !isDead)
            {
                myAnim.ResetTrigger("resurrect");
                currentHealth =0;
                Die();
            }
        }
        public float getPercentage(){
           maxHealth = GetComponent<BaseStats>().GetStat(Stat.Health);
            return ((float)currentHealth/(float)maxHealth) *100;
        }
        public void LevelUpRegen(){

            currentHealth = GetComponent<BaseStats>().GetStat(Stat.Health);
            
        }
        public float getCurrentHP(){
            return currentHealth;
        }
        public float getMaxHP(){
            return maxHealth;
        }
    }
}