using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes{

    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Image foreGround = null;
        [SerializeField] Canvas canvas = null;
        Health health;
        private void Awake() {
            health = GetComponentInParent<Health>();
        }
        // Update is called once per frame
        void Update()
        {
            
            if(Mathf.Approximately(health.GetFraction(),0) 
            || Mathf.Approximately(health.GetFraction(),1))
            {
                canvas.enabled = false;
                return;
            }
            
          canvas.enabled = true;
          foreGround.rectTransform.localScale = new Vector3(health.GetFraction(),1,1);
        }
        
    }

}