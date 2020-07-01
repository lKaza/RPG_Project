using System;
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
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float PatrolDwellTime = 5f;
        [Range (0,1)]
        [SerializeField] float patrolSpeedfraction = 0.25f;
        Fighter fighter;
        GameObject player;
        Mover mover;
        Vector3 guardPosition;
        float timeSinceLastSeenPlayer= Mathf.Infinity;
        float timeScouting = 0;
        int currentWaypointIndex = 0;
        

        
    private void Start() {
        guardPosition = transform.position;
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
                PatrolBehaviour();
            }
            timeSinceLastSeenPlayer += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;
            
            if(patrolPath != null)
            {
                if(AtWaypoint(currentWaypointIndex))
                {
                    timeScouting += Time.deltaTime;
                    if(timeScouting>PatrolDwellTime){
                        timeScouting = 0;
                        CycleWaypoint(currentWaypointIndex);
                    }
                    
                }
                nextPosition = GetCurrentWaypoint();
                print(nextPosition);
               
                
            }
            mover.StartMoveAction(nextPosition,patrolSpeedfraction);
        }

        private bool AtWaypoint(int index)
        {
            float distanceToWaypoint = Vector3.Distance(transform.position,GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void CycleWaypoint(int index)
        {
           currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
           
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
