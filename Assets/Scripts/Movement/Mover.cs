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

   
   public void StartMoveAction(Vector3 destination){
     GetComponent<Scheduler>().StartAction(this);
       MoveTo(destination);          
   }

    public void MoveTo(Vector3 destination)
    {
        
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