﻿using System.Collections;
using System.Collections.Generic;
using RPG.Control;
using RPG.Core;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics{


    public class CinematicControlRemover : MonoBehaviour
    {
        private GameObject player;

        private void Awake() {
              player = GameObject.FindGameObjectWithTag("Player");
            
        }
        private void OnEnabable() {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
        }

        private void OnDisable() {
            GetComponent<PlayableDirector>().played -= DisableControl;
            GetComponent<PlayableDirector>().stopped -= EnableControl;
        }
        void DisableControl(PlayableDirector aDirector){
          
            player.GetComponent<Scheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }
        void EnableControl(PlayableDirector aDirector){
            
            player.GetComponent<PlayerController>().enabled = true;
            
        }
    }
}