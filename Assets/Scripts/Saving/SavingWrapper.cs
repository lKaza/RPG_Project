using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Saving{
    public class SavingWrapper : MonoBehaviour
    {
        SavingSystem savingSystem;
        const string savelocation ="save";
        // Start is called before the first frame update
        void Start()
        {
            savingSystem = GetComponent<SavingSystem>();
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                savingSystem.Save(savelocation);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                savingSystem.Load(savelocation);
            }
        }
    }

}
