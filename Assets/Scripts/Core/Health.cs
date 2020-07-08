using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;

namespace RPG.Core{


public class Health : MonoBehaviour, ISaveable
{
        Animator myAnim;
        [SerializeField] int maxHealth=100;
        public int currentHealth;
        bool isDead = false;
        // Start is called before the first frame update
        void Awake()
        {
            myAnim = GetComponent<Animator>();
            currentHealth = maxHealth;
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        public void TakeDmg(int dmg)
        {
            
            currentHealth = currentHealth - dmg;
            if(currentHealth<=0 && !isDead)
            {
                Die();
            }
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
            return currentHealth;
        }

        public void RestoreState(object state)
        {
            currentHealth = (int)state;
            if (currentHealth <= 0 && !isDead)
            {
                Die();
            }
        }
    }
}