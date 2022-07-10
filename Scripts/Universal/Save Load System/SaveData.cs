using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveLoadSystem
{
    [System.Serializable]
    public class SaveData {
        public string playerName;
        public int levelReached;
        public List<HouseClass> levelData;



        //Alex Added Stuff
        public int currentLevel;
        public HouseClass currentHouse;
        public ObjectInfoClass editObject;
        public float timeElapsed;
        public bool firstNarratorObjectMod;


        // public List<PlayerStatus> levelPlayer;
        // public List<ObjectInfoClass> levelObject;
    }
}
