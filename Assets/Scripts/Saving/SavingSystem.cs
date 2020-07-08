﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        public IEnumerator LoadLastScene(string saveFile)
        {
            Dictionary<string,object> state = LoadFile(saveFile);
            if(state.ContainsKey("lastSceneBuildIndex")){
                int sceneBuildIndex = (int)state["lastSceneBuildIndex"];
                if (sceneBuildIndex != SceneManager.GetActiveScene().buildIndex)
                {
                    yield return SceneManager.LoadSceneAsync(sceneBuildIndex);
                }
            }        
            RestoreState(state);
        }


        public void Save(string saveFile)
        {
         Dictionary<string, object> state = LoadFile(saveFile);   
         CaptureState(state);
         SaveFile(saveFile,state);

        }

        private void SaveFile(string saveFile, object state)
        {

            string path = GetPathFromSaveFile(saveFile);
            print("Saving to " + path);
            using (FileStream stream = File.Open(path,FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream,state);
            }
        }

        public void Load(string saveFile)
        {
            
            RestoreState(LoadFile(saveFile));

        }

        private Dictionary<string, object> LoadFile(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            
            if(!File.Exists(path)){

                return new Dictionary<string, object>();
            }
            using (FileStream stream = File.Open(path, FileMode.Open,FileAccess.Read))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
        }

        private void CaptureState(Dictionary<string, object> state)
        {
           
            foreach(SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();

            }
          state["lastSceneBuildIndex"] = SceneManager.GetActiveScene().buildIndex;
        }

        private void RestoreState(Dictionary<string, object> state)
        {
            
            foreach(SaveableEntity saveable in FindObjectsOfType<SaveableEntity>()){

                string key = saveable.GetUniqueIdentifier();
                if (state.ContainsKey(key)) {
                    saveable.RestoreState(state[key]);
                   }
                  
                
            }

        }

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath,saveFile + ".sabroso");
        }
    }
}