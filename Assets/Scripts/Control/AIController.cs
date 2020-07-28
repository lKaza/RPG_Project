using System;
using System.Collections;
using System.Collections.Generic;
using GameDevTV.Utils;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using RPG.Attributes;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Control{

    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseRange = 5f;
        [SerializeField] float suspicionTime = 6f;
        [SerializeField] float aggroCooldown = 10f;
        [SerializeField] float shoutAggroDistance = 5f;
        [SerializeField] PatrolPath patrolPath = null;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float PatrolDwellTime = 5f;
        [Range (0,1)]
        [SerializeField] float patrolSpeedfraction = 0.25f;
        Fighter fighter;
        GameObject player;
        Mover mover;
        LazyValue<Vector3> guardPosition;
        float timeSinceLastSeenPlayer= Mathf.Infinity;
        float timeSinceLastaggravatedTime = Mathf.Infinity;
        float timeScouting = 0;
        int currentWaypointIndex = 0;
        
    private void Awake() {
        mover = GetComponent<Mover>();
        fighter = GetComponent<Fighter>();
        player = GameObject.FindGameObjectWithTag("Player");
        guardPosition = new LazyValue<Vector3>(GetInitialPosition);
    }
        
    private Vector3 GetInitialPosition() {
        return transform.position;
    }
    private void Start() {
        guardPosition.ForceInit();
    }

    private void Update()
        {   

            if(GetComponent<Health>().IsDead()) return;
            if (IsAggravated() && fighter.CanAttack(player))
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
            timeSinceLastaggravatedTime += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition.value;
            
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
            AggravateNearbyEnemies();
        }

        private void AggravateNearbyEnemies()
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position,shoutAggroDistance,Vector3.up,0f);
            foreach(RaycastHit hit in hits){
               

              AIController ai = hit.transform.GetComponent<AIController>();
              if(ai == null) continue;
              ai.Aggro();
                
                
            }
        }

        public void Aggro(){
            timeSinceLastaggravatedTime = 0f;
        }

        private bool IsAggravated()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if(timeSinceLastaggravatedTime< aggroCooldown){
                return true;
            }
            if(distanceToPlayer < chaseRange){
                return true;
            }
            return false;
        }
        public float getRange(){
            return chaseRange;
        }
    }

}
