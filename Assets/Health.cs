using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat{


public class Health : MonoBehaviour
{
        [SerializeField] int maxHealth=100;
        private int currentHealth;
        // Start is called before the first frame update
        void Start()
        {
            currentHealth = maxHealth;
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        public void TakeDmg(int dmg)
        {
            currentHealth = currentHealth - dmg;
            if(currentHealth<=0){
                print("me dead");
            }
        }
    }
}