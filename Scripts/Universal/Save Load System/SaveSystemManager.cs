using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Usage: using SaveLoadSystem

// New Game: call SaveSystemManager.NewGame(string playerName)
//      The saveData is stored in SaveSystemManager.CurrentSaveData;
//      Save() call needed later to store the saveData as files

// Save Game: call SaveSystemManager.Save()
//      Stores the data in SaveSystemManager.CurrentSaveData into Application.persistentDataPath + SavePath + FileName;
//      When quitting game, call it

// Load Game: call SaveSystemManager.Load()
//      The saveData is stored in SaveSystemManager.CurrentSaveData;
//      Constantly call it when game starts

// Advance Level: call SaveSystemManager.AdvanceLevel()
//      It will create a new HouseClass for the newly unlock level in SaveSystemManager.CurrentSaveData;

// Save Level: call SaveSystemManager.SaveLevel(int level, HouseClass houseData)
//      Saves houseData of current level to SaveSystemManager.CurrentSaveData;

// Load Level: call SaveSystemManager.LoadLevel(int level)
//      Returns HouseClass data at level in SaveSystemManager.CurrentSaveData.
//      If in the SaveSystemManager.CurrentSaveData, data for level does not exist, return null



namespace SaveLoadSystem
{
    public static class SaveSystemManager
    {
        public static SaveData CurrentSaveData = new SaveData();

        public const string SavePath = "/SaveData/";
        public const string FileName = "SaveGame.sav";


        public static void NewGame(string playerName) {
            SaveData tempData = new SaveData();
            tempData.playerName = playerName;
            tempData.levelReached = 0;
            tempData.levelData = new List<HouseClass>();
            CurrentSaveData = tempData;
        }

        public static void Save() {
            var dir = Application.persistentDataPath + SavePath;
            
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            string jsonData = JsonUtility.ToJson(CurrentSaveData, true);
            File.WriteAllText(dir + FileName, jsonData);

            // GUIUtility.systemCopyBuffer = dir + FileName;
           // Debug.Log(jsonData);
           // Debug.Log(dir + FileName);
        }


        public static bool Load() {
            string fullPath = Application.persistentDataPath + SavePath + FileName;
            // SaveData tempData = new SaveData();
            
            if (File.Exists(fullPath)) {
                string jsonData = File.ReadAllText(fullPath);
               // Debug.Log("JSON: " + jsonData);
                // tempData = JsonUtility.FromJson<SaveData>(jsonData);
                CurrentSaveData = JsonUtility.FromJson<SaveData>(jsonData);
            } else {
              //  Debug.Log("Save file does not exist!");
                return false;
            }

            // CurrentSaveData = tempData;
            return true;
        }

        public static void AdvanceLevel() {
            CurrentSaveData.levelReached += 1;
            Debug.Log("Reached level " + CurrentSaveData.levelReached.ToString());
            HouseClass emptyHouseClass = new HouseClass(new Vector3(0,0,0), new Vector3(0,0,0));
            CurrentSaveData.levelData.Add(emptyHouseClass);
            // Here need to be modified later, HouseClass doesn't have a constructor with no args
        }

        public static void SaveLevel(int level, HouseClass houseData) {
            if (level > CurrentSaveData.levelReached) {
                Debug.Log("Didn't reach level " + level.ToString());
            } else {
                CurrentSaveData.levelData[level - 1] = houseData;
            }
        }


        public static HouseClass LoadLevel(int level) {
            if (level > CurrentSaveData.levelReached) {
                Debug.Log("Didn't reach level " + level.ToString());
                return null;
            } else {
                return CurrentSaveData.levelData[level - 1];
            }
            
        }

    }
}
