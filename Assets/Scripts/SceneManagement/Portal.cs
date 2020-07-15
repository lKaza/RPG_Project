using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using RPG.Saving;

namespace RPG.SceneManagement{


    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier{
            A,B,C,D,E
        }
        [SerializeField] int sceneIndex=0;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier currentPortal;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = 2f;
        [SerializeField] float fadeInTime = 2f;
        [SerializeField] float blackscreenTime = 0.3f;

        // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        if(other.tag=="Player"){
             
            StartCoroutine(Transition());
        }
        
    }
        private IEnumerator Transition()
        {
            Fader fader = FindObjectOfType<Fader>();
           
            
            DontDestroyOnLoad(this.gameObject);
            yield return StartCoroutine(fader.FadeOut(fadeInTime));
            SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
            wrapper.Save();
            
            yield return SceneManager.LoadSceneAsync(sceneIndex);

            wrapper.Load();

            Portal otherPortal = GetOtherPortal();          
            UpdatePlayer(otherPortal);
            wrapper.Save();
            yield return new WaitForSeconds(blackscreenTime);
            yield return StartCoroutine(fader.FadeIn(fadeInTime));
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