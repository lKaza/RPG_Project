using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Saving;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement{

public class Mover : MonoBehaviour, IAction ,ISaveable
{
#pragma warning disable 0649
    
    [SerializeField] float maxSpeed = 5f;
    Animator myAnim;
    NavMeshAgent navMesh;
#pragma warning disable 0649

        // Start is called before the first frame update
        void Awake()
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

        public object CaptureState()
        {
            Dictionary< string,object> data = new Dictionary<string, object>();
            data["position"] = new SerializableVector3(transform.position);
            data["rotation"] = new SerializableVector3(transform.eulerAngles);
           return data;
        }

        public void RestoreState(object state)
        {
            Dictionary<string, object> data = (Dictionary<string, object>)state;
           navMesh.enabled = false;
           transform.position = ((SerializableVector3)data["position"]).ToVector();
           transform.eulerAngles = ((SerializableVector3)data["rotation"]).ToVector();
            navMesh.enabled = true;
        }
        // struct save/load data method
        /* struct MoverSaveData
        {
        [System.Serializable]
           public SerializableVector3 position;
           public SerializableVector3 rotation;
        } 
        CaptureState(){
            Mover data = new MoverSaveData();
            data.position = new SerializableVector3(transform.position);
            data.rotation = new SerializableVector3(transform.eulerAngles);
            return data;
        }
        RestoreState()
        MoverSaveData data = (MoverSaveData)state;
         GetComponent<NavMeshAgent>().enabled = false;
         transform.position = data.position.ToVector();
         transform.eulerAngles = data.rotation.ToVector();
         GetComponent<NavMeshAgent>().enabled = true;
        */
    }
}