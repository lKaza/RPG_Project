using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using RPG.Control;

namespace RPG.SceneManagement{


    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier{
            A,B,C,D,E
        }
#pragma warning disable 0649
        [SerializeField] int sceneIndex=0;
        [SerializeField] Transform spawnPoint = null;
        [SerializeField] DestinationIdentifier currentPortal;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeInTime = 2f;
        [SerializeField] float blackscreenTime = 0.3f;
#pragma warning disable 0649

        // Start is called before the first frame update
        private void OnTriggerEnter(Collider other) {
        if(other.tag=="Player"){
             
            StartCoroutine(Transition());
        }
        
    }
        private IEnumerator Transition()
        {
            
            Fader fader = FindObjectOfType<Fader>();
            SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
            PlayerController playerController = FindObjectOfType<PlayerController>();
            
            playerController.enabled = false;
            DontDestroyOnLoad(this.gameObject);
            yield return fader.FadeOut(fadeInTime);
            wrapper.Save();
            
            
            yield return SceneManager.LoadSceneAsync(sceneIndex);
            PlayerController newplayerController = FindObjectOfType<PlayerController>();
            newplayerController.enabled = false;
            wrapper.Load();

            Portal otherPortal = GetOtherPortal();          
            UpdatePlayer(otherPortal);
            wrapper.Save();

            yield return new WaitForSeconds(blackscreenTime);
            fader.FadeIn(fadeInTime);
            newplayerController.enabled = true;

            Destroy(this.gameObject);
        }
       
        private void UpdatePlayer(Portal otherPortal)
        {
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
        player.transform.rotation = otherPortal.spawnPoint.rotation;
        }

        private Portal GetOtherPortal()
        {
           
           foreach(Portal p in FindObjectsOfType<Portal>()){
               if(p.currentPortal == destination){
                   return p;
               }
           }
            return null;
        }
    }
}