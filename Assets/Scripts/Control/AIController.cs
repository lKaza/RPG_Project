using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Control{

    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseRange = 5f;
        [SerializeField] float suspicionTime = 6f;
        Fighter fighter;
        GameObject player;
        Mover mover;
        NavMeshAgent navMesh;
        Vector3 guardPosition;
        float timeSinceLastSeenPlayer= Mathf.Infinity;
        

        
    private void Start() {
        guardPosition = transform.position;
        navMesh = GetComponent<NavMeshAgent>();
        mover = GetComponent<Mover>();
        fighter = GetComponent<Fighter>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
        {   

            if(GetComponent<Health>().IsDead()) return;
            if (InAttackRange() && fighter.CanAttack(player))
            {
                timeSinceLastSeenPlayer = 0;
                AttackBehaviour();
            }
            else if(suspicionTime>timeSinceLastSeenPlayer)
            {
                SuspicionBehaviour();
            }
            else
            {
                GuardBehaviour();
            }
            timeSinceLastSeenPlayer += Time.deltaTime;
        }

        private void GuardBehaviour()
        {
            mover.StartMoveAction(guardPosition);
        }

        private void SuspicionBehaviour()
        {
            GetComponent<Scheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player);
        }

        private bool InAttackRange()
        {
           
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            return  distanceToPlayer < chaseRange;
        }
        public float getRange(){
            return chaseRange;
        }
    }

}
