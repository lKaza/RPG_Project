using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed=5f;
    Transform target = null;
    int damage = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(target == null) 
        {
            return;
        }
        
        this.transform.LookAt(GetAimLocation());
        this.transform.Translate(Vector3.forward*speed*Time.deltaTime);
        
    }

    public void SetTarget(Transform newtarget,int damage)
    {
       
        target = newtarget;
        this.damage +=damage;
        
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
            target.GetComponent<Health>().TakeDmg(damage);
            Destroy(gameObject);
        }
    }
}
