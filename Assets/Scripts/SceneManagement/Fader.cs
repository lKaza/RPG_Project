using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement{
    public class Fader : MonoBehaviour
        {
           
           CanvasGroup canvasGroup;
           Coroutine currentlyActiveFade = null;

           float deltaAlpha;
           private void Awake() {
               canvasGroup = GetComponent<CanvasGroup>();
           }
          

           public Coroutine FadeOut(float transitionTime)
           {
              return Fade(1,transitionTime);
            }

            public Coroutine FadeIn(float transitionTime)
            {
                
            return Fade(0,transitionTime);

            }
            
            public Coroutine Fade(float target, float time){

            if (currentlyActiveFade != null)
            {
                StopCoroutine(currentlyActiveFade);
            }
            currentlyActiveFade = StartCoroutine(FadeRoutine(target,time));

            return currentlyActiveFade;

        }

            private IEnumerator FadeRoutine(float target,float time)
            {
            while(!Mathf.Approximately(canvasGroup.alpha,target))
            {
                deltaAlpha = Time.deltaTime / time;
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha,target,deltaAlpha);
                yield return null;
            }
        }

        private IEnumerator FadeInRoutine(float time){
            while (canvasGroup.alpha > 0)
            {
                deltaAlpha = Time.deltaTime / time;
                canvasGroup.alpha -= deltaAlpha;
                yield return null;
            }
        }
        public void FadeOutInmediate(){
            canvasGroup.alpha = 1;
        }
        }

}
