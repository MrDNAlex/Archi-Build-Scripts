using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[System.Serializable]
public class ObjectInfoClass
{
    //Game Object
    public GameObject gameObj;

    public Vector3 Dim;

    public Vector3 DimR;

   // public Sprite ImageIcon; //Probably change this to the string path
    public string Name;
    public string ImageIconPath;
    public int FloorNum;
    public int RoomNum;

    public bool completed;
   
    public string objID;

    public Vector3 position;
    //Use either or of these
    Vector3 rotation;
    public Quaternion rotationQuaternion;



    public ObjectInfoClass (GameObject obj)
    {
        this.gameObj = obj;
        GetDimensions();

    }

    void GetDimensions ()
    {
        this.Dim = new Vector3(gameObj.transform.localScale.x, gameObj.transform.localScale.y, gameObj.transform.localScale.z);
    }

    public void genID (int num)
    {
        objID = "F" + FloorNum + "R" + RoomNum + "O" + num;
    }


}
