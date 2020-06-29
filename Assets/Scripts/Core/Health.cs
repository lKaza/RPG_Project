using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace RPG.Core{


public class Health : MonoBehaviour
{
        Animator myAnim;
        [SerializeField] int maxHealth=100;
        private int currentHealth;
        bool isDead = false;
        // Start is called before the first frame update
        void Start()
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
    }
}