using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core.Camera{

public class FollowCamera : MonoBehaviour
{
        [SerializeField] Transform player = null;
    
        // Update is called once per frame
        void LateUpdate()
        {
            
            transform.position = player.position;
            
        }
    }
}