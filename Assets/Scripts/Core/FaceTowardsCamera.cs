using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core{

    public class FaceTowardsCamera : MonoBehaviour
    {
        private void Update() {
            transform.forward = Camera.main.transform.forward;
        }
    }
}












        // private GameObject camera;
        // Also Works
        // void Start()
        // {   
        // camera = GameObject.FindGameObjectWithTag("FollowCamera");
        // }

        // // Update is called once per frame
        // void Update()
        // {
        //     transform.LookAt(this.transform.position -(camera.transform.position-this.transform.position));
        // }