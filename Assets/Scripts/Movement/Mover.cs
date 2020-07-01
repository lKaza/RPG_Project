using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement{

public class Mover : MonoBehaviour, IAction
{
    [SerializeField] Transform target;
    [SerializeField] float maxSpeed = 5f;
    Animator myAnim;
    NavMeshAgent navMesh;

   
    // Start is called before the first frame update
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
        Animator();
    
    }

   
   public void StartMoveAction(Vector3 destination, float speedFraction){
     GetComponent<Scheduler>().StartAction(this);
     navMesh.speed = maxSpeed * Mathf.Clamp01(speedFraction);
     MoveTo(destination,speedFraction);          
   }

    public void MoveTo(Vector3 destination, float speedFraction)
    {
        navMesh.speed = maxSpeed * Mathf.Clamp01(speedFraction);
        navMesh.isStopped = false;
        navMesh.SetDestination(destination);
        
    }

    private void Animator()
    {
       Vector3 velocity = navMesh.velocity;
       Vector3 localVelocity = transform.InverseTransformDirection(velocity);
       float speed = localVelocity.z;
        myAnim.SetFloat("forwardSpeed",speed);
    }
    public void Disengage(){
       
        navMesh.isStopped = true;
    }
    
}
}