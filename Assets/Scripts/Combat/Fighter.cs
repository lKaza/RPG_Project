using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour,IAction
        {
            [SerializeField] float weaponRange =2f;
            Transform target;
            private void Update() {
                if(target != null){

                    GetComponent<Mover>().MoveTo(target.position);
                    float distance = Vector3.Distance(transform.position,target.position);
                    if(distance <=weaponRange){                   
                    GetComponent<Mover>().Disengage();
                    }

                }
            }

            public void Attack(CombatTarget combatTarget){
                GetComponent<Scheduler>().StartAction(this);
                target = combatTarget.transform;
            }
            public void Disengage(){
                if(target == null) return;
            
                target = null;
            }
        }

}
