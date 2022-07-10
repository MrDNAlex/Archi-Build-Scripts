using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    GameObject newWall;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Spawning Wall");
        newWall = GameObject.Instantiate((GameObject)Resources.Load("Models/floor_1_way_insul_stone")) as GameObject;
        newWall.name = "Wall";
        newWall.transform.localScale = new Vector3(250, 300, 25);
        //newWall.transform.SetParent(currentRoomTrans);
        //newWall.transform.parent = this.transform;
        newWall.transform.localPosition = DNAMath.alignToPoint(Vector3.zero, new Vector3(250, 300, 25), 1);
        Debug.Log("Done Wall");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update");
    }
}
