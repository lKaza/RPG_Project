﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat{


public class DestroyEffect : MonoBehaviour
{
  

    // Update is called once per frame
    void Update()
    {
     if(!GetComponent<ParticleSystem>().IsAlive()){
         Destroy(gameObject);
     }   
    }
}
}