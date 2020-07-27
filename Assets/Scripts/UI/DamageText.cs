using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI.DamageText{

    public class DamageText : MonoBehaviour
    {
        [SerializeField] Text damageTextComponent = null;
        // Start is called before the first frame update

        public void SetValue(float amount)
        {   
          damageTextComponent.text = amount.ToString();
        }
    }
}
