using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement{
    public class Fader : MonoBehaviour
        {
           
           CanvasGroup canvasGroup;
           float deltaAlpha;
           private void Awake() {
               canvasGroup = GetComponent<CanvasGroup>();
           }
          

           public IEnumerator FadeOut(float transitionTime)
           {
              
            while(canvasGroup.alpha <1)
            {
                deltaAlpha = Time.deltaTime / transitionTime;
                canvasGroup.alpha += deltaAlpha;
                yield return null;
            }

           
        }
        public IEnumerator FadeIn(float transitionTime)
        {
           
            while (canvasGroup.alpha > 0)
            {
                deltaAlpha = Time.deltaTime / transitionTime;
                canvasGroup.alpha -= deltaAlpha;
                yield return null;
            }

        }
        public void FadeOutInmediate(){
            canvasGroup.alpha = 1;
        }
        }

}
