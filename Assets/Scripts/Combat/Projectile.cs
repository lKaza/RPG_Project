using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat{ 
public class Projectile : MonoBehaviour
{
    [SerializeField] float speed=5f;
    [SerializeField] bool homing;
    [SerializeField] GameObject hitEffect = null;
    [SerializeField] float maxLifeTime=3f;
    [SerializeField] GameObject[] destroyOnHit = null;
    [SerializeField] float lifeAfeterImpact = 2f;
    Transform target = null;
    int damage = 0;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.LookAt(GetAimLocation());
    }

    // Update is called once per frame
    void Update()
    {
        
        if(target == null) 
        {
            return;
        }
        
        
        this.transform.Translate(Vector3.forward*speed*Time.deltaTime);
        if(homing && !target.GetComponent<Health>().IsDead()){
            this.transform.LookAt(GetAimLocation());
        }
        
    }

    public void SetTarget(Transform newtarget,int damage)
    {
       
        target = newtarget;
        this.damage +=damage;
        Destroy(gameObject,maxLifeTime);
    }

    private Vector3 GetAimLocation()
    {
        CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
        if(targetCapsule == null)
        {
            return target.transform.position;
        }else{
            return target.transform.position + Vector3.up * targetCapsule.height / 2;
        }
        
    }
    private void OnTriggerEnter(Collider other) {
        
        if(target != null){
            if(hitEffect != null){
                Instantiate(hitEffect,GetAimLocation(),transform.rotation);
               
            }
           
            target.GetComponent<Health>().TakeDmg(damage);
            speed = 0;
           
            foreach(GameObject toDestroy in destroyOnHit){
                Destroy(toDestroy);
            }
            Destroy(gameObject,lifeAfeterImpact);
           
        }
    }
}
}