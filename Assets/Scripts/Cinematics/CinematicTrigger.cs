using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics{


public class CinematicTrigger : MonoBehaviour
{
    public bool playedOnce = false;
    private void OnTriggerEnter(Collider other) {
        if(playedOnce == false && other.tag == "Player"){
            playedOnce = true;
        
            GetComponent<PlayableDirector>().Play();
        }return;
    
        }
    }
}