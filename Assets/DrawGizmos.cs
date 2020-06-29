﻿using System.Collections;
using System.Collections.Generic;
using RPG.Control;
using UnityEngine;

public class DrawGizmos : MonoBehaviour
{
    [SerializeField] Color color = new Color(1, 1, 0, 0.75F);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = color;
        Gizmos.DrawWireSphere(transform.position, GetComponent<AIController>().getRange());

    }
}
