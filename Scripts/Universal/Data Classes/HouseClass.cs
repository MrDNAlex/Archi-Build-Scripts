using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HouseClass 
{
    public GameObject houseObject;
    public Vector3 housePos;
    public Vector3 houseDim;
    [SerializeReference] public List<FloorClass> floorList;
    [SerializeReference] public List<ObjectInfoClass> outsideDecorationList; //Maybe?

    public int floorStart;

    public HouseClass (Vector3 pos, Vector3 dim)
    {
        this.housePos = pos;
        this.houseDim = dim;

        floorList = new List<FloorClass> ();
        outsideDecorationList = new List<ObjectInfoClass> ();

        createNewFloor(0, Vector3.zero);

        
    }


    public void createNewFloor (int floorNum, Vector3 pos)
    {
        FloorClass floor = new FloorClass (floorNum, pos, houseDim);
        floorList.Add(floor);
        // Debug.Log("Added floor");
       // Debug.Log(floorNum);
       // Debug.Log("Floor Made");
        updateHouseDim();
       
    }

    void updateHouseDim ()
    {
        houseDim = new Vector3 (houseDim.x, 3 * floorList.Count , houseDim.z);
    }

    public void test ()
    {
        floorStart = -1;

        for (int i = floorStart; i < 3; i++)
        {
            //Debug.Log(i);
            createNewFloor(i, new Vector3(0, 0, 0));
        }
    }

    public void createBlueprint (List<FloorClass> floors)
    {

        floorList.Clear();

        floorList = new List<FloorClass>();
        //int floor = 0;
        for (int i = 0; i < floors.Count; i ++)
        {

            //Debug.Log("Loop i: " + i);
            createNewFloor(i, housePos);

            Debug.Log("Floor Num: " + floorList[i].floorNum);
           

            for (int j = 0; j < floors[i].roomList.Count; j ++)
            {
                floorList[i].createNewRoom(j, floors[i].roomList[j].roomPos, floors[i].roomList[j].roomDim);

                floorList[i].roomList[j].assignRoomInfo(floors[i].roomList[j].roomType, floors[i].roomList[j].roomDirVec);

               floorList[i].roomList[j].generateRoomObjects(floorList[i].roomList[j]);

                Debug.Log(floorList[i].roomList.Count);

                Debug.Log(floorList[i].roomList[j].objList.Count);


                //floorList[i].roomList[j].
            }
            //floor = floor + 1;
        }
    }

}
