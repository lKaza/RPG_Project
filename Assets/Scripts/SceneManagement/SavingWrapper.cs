using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using RPG.SceneManagement;
using UnityEngine;


namespace RPG.SceneManagement{
    public class SavingWrapper : MonoBehaviour
    {
        SavingSystem savingSystem;
        const string savelocation ="save";
        [SerializeField] float fadeInTime = 0.5f;
        // Start is called before the first frame update
        private void Awake() {
            savingSystem = GetComponent<SavingSystem>();
        }
        IEnumerator Start()
        {   
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutInmediatly();    
            yield return savingSystem.LoadLastScene(savelocation);
            yield return fader.FadeIn(fadeInTime);
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
        }

        public void Save()
        {
            savingSystem.Save(savelocation);
        }

        public void Load()
        {
            savingSystem.Load(savelocation);
        }
    }

}
