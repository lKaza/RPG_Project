using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement{


public class SavingWrapper : MonoBehaviour
{
    [SerializeField] float fadeInTime = 0.8f;
    const string defaultSaveFile = "save";
    
    IEnumerator Start()
    {
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutInmediate();
           yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
           yield return fader.FadeIn(fadeInTime);
            
    }

   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L)){
            Load();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }
    }
    public void Load(){
        GetComponent<SavingSystem>().Load(defaultSaveFile);
    }
    public void Save()
    {
        GetComponent<SavingSystem>().Save(defaultSaveFile);
    }
}
}