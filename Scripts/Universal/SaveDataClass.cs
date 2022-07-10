using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class SaveDataClass
{


    //Required Dimensions


    // public string UnitX;
    // public string UnitY;
    // public string UnitZ;
    string Path;
    string fileName;

    // BinaryFormatter formatter;
    // // FileStream file;


    public SaveDataClass(string Name, bool newFile)
    {
        this.fileName = Name;
        
        Directory.CreateDirectory(Application.persistentDataPath + "/" + this.fileName);
        this.Path = Application.persistentDataPath + "/" + this.fileName;

    }
    //     return info;
    // }

    // public object GetClass(string ClssType)
    // {

    //     switch (ClssType)
    //     {
    //         case "ObjInfo":
    //             // JsonUtility.FromJson(File.Open(Path, FileMode.Open));
    //             return JsonUtility.FromJson<ObjectInfoClass>(File.ReadAllText(Path));
    //             break;
    //         case "HouseClass":
    //             return JsonUtility.FromJson<HouseClass>(File.ReadAllText(Path));
    //             break;
    //         default:
    //             return null;
    //             break;
    //     }


    public static void saveHouse(HouseClass house)
    {
        string basicPath = Application.persistentDataPath;
        string housePath = basicPath + "/" + "House";
        string fileEnd = ".txt";
        // string floorPath = housePath + "/" + "Floor"; //Add the floor number when generating files
        // string roomPath = floorPath + "/" + "Room"; //Add room number
        // string objPath = roomPath + "/" + "Object"; //Add number of object


        /*
        string floorPath;
        string roomPath;
        Directory.CreateDirectory(housePath);
        for (int i = 0; i < house.floorList.Count; i++)
        {
            floorPath = housePath + "/" + "Floor" + (house.floorStart + i);
            Directory.CreateDirectory(floorPath);
            for (int j = 0; j < house.floorList[i].roomList.Count; j++)
            {
                roomPath = floorPath + "/" + "Room" + j;
                Directory.CreateDirectory(roomPath);
                for (int g = 0; g < house.floorList[i].roomList[j].objList.Count; g++)
                {
                    string objFilePath = roomPath + "/" + "Object" + g + fileEnd;
                    saveData(house.floorList[i].roomList[j].objList[g], objFilePath);
                }
                string roomFilePath = roomPath + "/" + "Room" + fileEnd;
                saveData(house.floorList[i].roomList[j], roomFilePath);

            }
            string floorFilePath = floorPath + "/" + "Floor" + fileEnd;
            saveData(house.floorList[i], floorFilePath);
        }
        */
        Directory.CreateDirectory(housePath);
        string houseFilePath = housePath + "/" + "House" + fileEnd;
        saveData(house, houseFilePath);

       

        //  loadHouse();

    }


    public static void saveData (object data, string Path)
    {

        string finalData = JsonUtility.ToJson(data);
        Debug.Log(finalData);
        File.WriteAllText(Path, finalData);
    }

    public static object loadData (string path, string type)
    {
        
        switch (type)
        {
            case "HouseClass":
                return JsonUtility.FromJson<HouseClass>(File.ReadAllText(path));
               
            case "ObjectInfoClass":
                return JsonUtility.FromJson<ObjectInfoClass>(File.ReadAllText(path));

            default:
                return null;

        }
    }


    public static HouseClass loadHouse ()
    {
        string basicPath = Application.persistentDataPath;
        string housePath = basicPath + "/" + "House" + "/" + "House.txt";
      
        HouseClass house = JsonUtility.FromJson<HouseClass>(File.ReadAllText(housePath));


        string data = JsonUtility.ToJson (house);
       // Debug.Log(data);

        return house;
    }








}
