using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attributes{

    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Image foreGround;
        Health health;
        private void Awake() {
            health = GetComponentInParent<Health>();
        }
        // Update is called once per frame
        void Update()
        {
          foreGround.rectTransform.localScale = new Vector3(health.GetFraction(),1,1);
        }
        
    }

}