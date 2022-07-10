using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FloorClass 
{
    public GameObject floorObject;
    public int floorNum;
    [SerializeReference] public List<RoomClass> roomList;
    public Vector3 floorPos;
    public Vector3 floorDim;
    public List<string> roomDir;


    //Possibly?
    //Dimensions of the floor underneath?
    public Vector3 lastFloorDim;

    public FloorClass (int num, Vector3 pos, Vector3 dim)
    {
        this.floorNum = num;
        this.floorPos = pos;
        this.floorDim = dim;

        roomList = new List<RoomClass>();

    }

    public void createObject ()
    {
        floorObject = new GameObject();
        floorObject.name = "Floor " + floorNum;
        floorObject.transform.position = floorPos;  
    }


    public void createNewRoom (int roomNum, Vector3 pos, Vector3 dim)
    {
        //Variables such as wall thickness needed, 
        //Create a system where the user will start with a click and then drag. the program will then take the closest point to the floors origin and the farthest point to the floors origin 
        RoomClass room = new RoomClass(roomNum, floorNum, pos, dim);
        roomList.Add(room);
       // Debug.Log("added room");
    }

    public void test ()
    {
        for (int i = 0; i < 4; i++)
        {
            createNewRoom(i, new Vector3(0, 0, 0), new Vector3(0, 0, 0));
        }
    }


}
