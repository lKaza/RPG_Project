using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control{


public class DrawWaypoint : MonoBehaviour
{
    void OnDrawGizmos()
    {
        
        Gizmos.color = Color.red;
        for(int i=0; i<transform.childCount;i++)
        {
           
            Gizmos.DrawSphere(transform.GetChild(i).position, 0.3f);
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i+1).position);

        }
    }
}
}