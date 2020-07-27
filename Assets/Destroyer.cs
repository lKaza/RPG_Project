using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
   
   [SerializeField] GameObject targetParentToDestroy = null;

    private void Start() {
       Destroy(targetParentToDestroy,0.5f);
        
    }

}
