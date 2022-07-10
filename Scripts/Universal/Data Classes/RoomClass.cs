using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomClass 
{
    public GameObject roomObject;
    public int roomNum;
    public int roomFloorNum;
    public Vector3 roomPos;
    public Vector3 roomDim;
    [SerializeReference] public List<ObjectInfoClass> objList;
    [SerializeReference] public List<ObjectInfoClass> decorationList;
   // public float wallThickness;
    public string roomDirVec;
    public string roomType;


    public List<string> objDir;

    public RoomClass (int roomNum, int floorNum, Vector3 pos, Vector3 dim)
    {
        //Passthrough these variables I think 
        this.roomNum = roomNum;
        this.roomFloorNum = floorNum;
        this.roomPos = pos;
        this.roomDim = dim;
        //this.wallThickness = thickness;

        objList = new List<ObjectInfoClass>();

        //test();

        //Make the list of all the objects

        //Make a function that creates all the objects needed for a room

    }

    public void assignRoomInfo (string roomType, string roomDir)
    {
        this.roomType = roomType;
        this.roomDirVec = roomDir;
    }

    public void generateRoomObjects (RoomClass roomClass)
    {
      //Define room type and direction in the other gen space


        if (objList == null)
        {
            objList = new List<ObjectInfoClass>();
        }


        float wallHeight = 150;
        float wallWidth = 15;
        float floorHeight = 15;

        //IRL doors are 2m tall
        switch (roomType)
        {
            case "Room":

                //Generate roof for each room but the stairs
                switch (roomDirVec)
                {
                    case "Up":
                        //   ^
                        //   |

                        genWall("Back Wall", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, wallHeight, wallWidth), 1, 1);
                        genWall("Side Wall 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 5), new Vector3(wallWidth, wallHeight, (roomClass.roomDim.z / 2 * 100) - wallWidth * 2), 5, 2);
                        genWall("Side Wall 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 7), new Vector3(wallWidth, wallHeight, (roomClass.roomDim.z / 2 * 100) - wallWidth * 2), 7, 3);

                        genFloor("Room Floor", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, floorHeight, roomClass.roomDim.z / 2 * 100), 1, 4);

                        if (roomClass.roomDim.x > 0.8f)
                        {
                            float doorwayWidth = (((roomClass.roomDim.x / 2) - 0.4f) / 2) * 100;
                            genWall("Doorway Wall 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 2), new Vector3(doorwayWidth, wallHeight, wallWidth), 2, 5);
                            genWall("Doorway Wall 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 3), new Vector3(doorwayWidth, wallHeight, wallWidth), 3, 6);
                        }
                        else
                        {
                            float doorwayWidth = ((roomClass.roomDim.x / 4) - (roomClass.roomDim.x / 8)) * 100;
                            genWall("Doorway Wall 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 2), new Vector3(doorwayWidth, wallHeight, wallWidth), 2, 5);
                            genWall("Doorway Wall 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 3), new Vector3(doorwayWidth, wallHeight, wallWidth), 3, 6);
                        }
                        //Roof
                        genFloor("Roof", DNAMath.determineCorner(roomClass.roomPos + new Vector3(0, 2.7f, 0), roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, floorHeight, roomClass.roomDim.z / 2 * 100), 1, 7);

                        break;
                    case "Right":
                        //  -->
                        genWall("Back Wall", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(wallWidth, wallHeight, roomClass.roomDim.z / 2 * 100), 1, 1);
                        genWall("Side Wall 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 6), new Vector3((roomClass.roomDim.x / 2 * 100) - wallWidth * 2, wallHeight, wallWidth), 6, 2);
                        genWall("Side Wall 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 8), new Vector3((roomClass.roomDim.x / 2 * 100) - wallWidth * 2, wallHeight, wallWidth), 8, 3);

                        genFloor("Floor", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, floorHeight, roomClass.roomDim.z / 2 * 100), 1, 4);
                        if (roomClass.roomDim.x > 0.8f)
                        {
                            float doorwayWidth = (((roomClass.roomDim.z / 2) - 0.4f) / 2) * 100;
                            genWall("Doorway Wall 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 3), new Vector3(wallWidth, wallHeight, doorwayWidth), 3, 5);
                            genWall("Doorway Wall 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 4), new Vector3(wallWidth, wallHeight, doorwayWidth), 4, 6);
                        }
                        else
                        {
                            float doorwayWidth = ((roomClass.roomDim.z / 4) - (roomClass.roomDim.z / 8)) * 100;
                            genWall("Doorway Wall 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 3), new Vector3(wallWidth, wallHeight, doorwayWidth), 3, 5);
                            genWall("Doorway Wall 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 4), new Vector3(wallWidth, wallHeight, doorwayWidth), 4, 6);
                        }
                        //Roof
                        genFloor("Roof", DNAMath.determineCorner(roomClass.roomPos + new Vector3(0, 2.7f, 0), roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, floorHeight, roomClass.roomDim.z / 2 * 100), 1, 7);

                        break;
                    case "Left":
                        //  <--

                        genWall("Back Wall", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 3), new Vector3(wallWidth, wallHeight, roomClass.roomDim.z / 2 * 100), 3, 1);
                        genWall("Side Wall 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 6), new Vector3((roomClass.roomDim.x / 2 * 100) - wallWidth * 2, wallHeight, wallWidth), 6, 2);
                        genWall("Side Wall 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 8), new Vector3((roomClass.roomDim.x / 2 * 100) - wallWidth * 2, wallHeight, wallWidth), 8, 3);

                        genFloor("Floor", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, floorHeight, roomClass.roomDim.z / 2 * 100), 1, 4);
                        if (roomClass.roomDim.x > 0.8f)
                        {
                            float doorwayWidth = (((roomClass.roomDim.z / 2) - 0.4f) / 2) * 100;
                            genWall("Doorway Wall 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 2), new Vector3(wallWidth, wallHeight, doorwayWidth), 2, 5);
                            genWall("Doorway Wall 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(wallWidth, wallHeight, doorwayWidth), 1, 6);
                        }
                        else
                        {
                            float doorwayWidth = ((roomClass.roomDim.z / 4) - (roomClass.roomDim.z / 8)) * 100;
                            genWall("Doorway Wall 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 2), new Vector3(wallWidth, wallHeight, doorwayWidth), 2, 5);
                            genWall("Doorway Wall 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(wallWidth, wallHeight, doorwayWidth), 1, 6);
                        }
                        //Roof
                        genFloor("Roof", DNAMath.determineCorner(roomClass.roomPos + new Vector3(0, 2.7f, 0), roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, floorHeight, roomClass.roomDim.z / 2 * 100), 1, 7);

                        break;
                    case "Down":
                        genWall("Back Wall", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 2), new Vector3(roomClass.roomDim.x / 2 * 100, wallHeight, wallWidth), 2, 1);
                        genWall("Side Wall 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 5), new Vector3(wallWidth, wallHeight, (roomClass.roomDim.z / 2 * 100) - wallWidth * 2), 5, 2);
                        genWall("Side Wall 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 7), new Vector3(wallWidth, wallHeight, (roomClass.roomDim.z / 2 * 100) - wallWidth * 2), 7, 3);

                        genFloor("Floor", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, floorHeight, roomClass.roomDim.z / 2 * 100), 1, 4);
                        if (roomClass.roomDim.x > 0.8f)
                        {
                            float doorwayWidth = (((roomClass.roomDim.x / 2) - 0.4f) / 2) * 100;
                            genWall("Doorway Wall 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(doorwayWidth, wallHeight, wallWidth), 1, 5);
                            genWall("Doorway Wall 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 4), new Vector3(doorwayWidth, wallHeight, wallWidth), 4, 6);

                        }
                        else
                        {
                            float doorwayWidth = ((roomClass.roomDim.x / 4) - (roomClass.roomDim.x / 8)) * 100;
                            genWall("Doorway Wall 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(doorwayWidth, wallHeight, wallWidth), 1, 5);
                            genWall("Doorway Wall 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 4), new Vector3(doorwayWidth, wallHeight, wallWidth), 4, 6);
                        }
                        //Roof
                        genFloor("Roof", DNAMath.determineCorner(roomClass.roomPos + new Vector3(0, 2.7f, 0), roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, floorHeight, roomClass.roomDim.z / 2 * 100), 1, 7);

                        break;
                }

                break;
            case "Hallway":

                genFloor("Floor", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, floorHeight, roomClass.roomDim.z / 2 * 100), 1, 1);

                //Roof
                genFloor("Roof", DNAMath.determineCorner(roomClass.roomPos + new Vector3(0, 2.7f, 0), roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, floorHeight, roomClass.roomDim.z / 2 * 100), 1, 2);
                break;
            case "Stairs":

                switch (roomClass.roomDirVec)
                {
                    case "Up":
                        //   ^
                        //   |
                        genStairs("Stairs", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, 150, roomClass.roomDim.z / 2 * 100), 0, 1, 1, false);
                        break;
                    case "Right":
                        //  -->
                        genStairs("Stairs", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.z / 2 * 100, 150, roomClass.roomDim.x / 2 * 100), 90, 1, 1, true);
                        break;
                    case "Left":
                        //  <--
                        genStairs("Stairs", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.z / 2 * 100, 150, roomClass.roomDim.x / 2 * 100), 270, 1, 1, true);
                        break;
                    case "Down":
                        genStairs("Stairs", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, 150, roomClass.roomDim.z / 2 * 100), 180, 1,1, false);
                        break;
                    default:

                        break;

                }

                break;
        } 

        //Variables such as wall thickness needed, 
        //Create a system where the user will start with a click and then drag. the program will then take the closest point to the floors origin and the farthest point to the floors origin 

    }


    void genWall(string name, Vector3 pos, Vector3 dim, int pointNum,  int objectNum)
    {
        GameObject wallObj = Resources.Load("Models/floor_dec_tile") as GameObject;

        Debug.Log(wallObj);

        ObjectInfoClass wall = new ObjectInfoClass(wallObj);
        wall.DimR = dim;
        wall.position = DNAMath.alignToPoint(pos, dim, pointNum);
        wall.RoomNum = roomNum;
        wall.FloorNum = roomFloorNum;
        wall.ImageIconPath = "Icons/block4";
        wall.genID(objectNum);
        wall.Name = name + " " + "(" + wall.objID + ")";
        wall.completed = false;

        Debug.Log(wall.gameObj);

        objList.Add(wall);
        
    }

    void genFloor (string name, Vector3 pos, Vector3 dim, int pointNum, int objectNum)
    {
        GameObject floorObj = Resources.Load("Models/floor_dec_tile") as GameObject;
        
        ObjectInfoClass floor = new ObjectInfoClass(floorObj);
        floor.DimR = dim;
        floor.position = DNAMath.alignToPoint(pos, dim, pointNum);
        floor.RoomNum = roomNum;
        floor.FloorNum = roomFloorNum;
        floor.ImageIconPath = "Icons/block2";
        floor.completed = false;

        floor.genID(objectNum);
        floor.Name = name + " " + "(" + floor.objID + ")";

        objList.Add(floor);

    }

    void genStairs (string name, Vector3 pos, Vector3 dim, int pointNum,float rotation, int objectNum, bool horizontal)
    {
        GameObject stairsObj = Resources.Load("Models/stairs") as GameObject;

        ObjectInfoClass stairs = new ObjectInfoClass(stairsObj);
        stairs.DimR = dim;
       
        if (horizontal)
        {
            stairs.position = DNAMath.alignToPoint(pos, new Vector3(dim.z, dim.y, dim.x), pointNum);
        } else
        {
            stairs.position = DNAMath.alignToPoint(pos, dim, pointNum);
        }

        stairs.rotationQuaternion = Quaternion.Euler(0, rotation, 0);
        stairs.RoomNum = roomNum;
        stairs.FloorNum = roomFloorNum;
        stairs.ImageIconPath = "Icons/stairs";
        stairs.completed = false;


        stairs.genID(objectNum);
        stairs.Name = name + " " + "(" + stairs.objID + ")";

        objList.Add(stairs);
    }

    /*
    public void test ()
    {
        GameObject test = Resources.Load("Models/floor_1_way_insul_stone") as GameObject;
        GameObject test1 = Resources.Load("Models/floor_1_way_insul_wood") as GameObject;
        GameObject test2 = Resources.Load("Models/floor_2_way_insul_stone") as GameObject;
        GameObject test3 = Resources.Load("Models/floor_2_way_insul_wood") as GameObject;
        GameObject test4 = Resources.Load("Models/floor_2_way_U_insul_wood") as GameObject;
        GameObject test5 = Resources.Load("Models/floor_2_way_U_insul_stone") as GameObject;

        Texture2D image = null;

        ObjectInfoClass info1 = new ObjectInfoClass(test1);
        image = Resources.Load("Icons/ball1") as Texture2D;
        info1.ImageIconPath = "Icons/ball1";
        info1.Name = "Regular";
        info1.RoomNum = roomNum;
        info1.FloorNum = roomFloorNum;
        info1.completed = false;

        info1.DimR = new Vector3 (convertToMeter(3), convertToMeter(1), convertToMeter(3));
       
        info1.genID(objList.Count);
       
        objList.Add(info1);

        ObjectInfoClass info2 = new ObjectInfoClass(test2);
        image = Resources.Load("Icons/ball2") as Texture2D;
        info2.ImageIconPath = "Icons/ball2";
        info2.Name = "Blue";
        info2.RoomNum = roomNum;
        info2.FloorNum = roomFloorNum;
        info2.completed = false;
        info2.DimR = new Vector3(convertToMeter(3), convertToMeter(1), convertToMeter(3));
        info2.genID(objList.Count);

        objList.Add(info2);

        ObjectInfoClass info3 = new ObjectInfoClass(test3);
        image = Resources.Load("Icons/ball3") as Texture2D;
        //info3.ImageIcon = Sprite.Create(image, new Rect(0.0f, 0.0f, image.width, image.height), new Vector2(0.5f, 0.5f), 100.0f);
        info3.ImageIconPath = "Icons/ball3";
        info3.Name = "Great Ball";
        info3.RoomNum = roomNum;
        info3.FloorNum = roomFloorNum;
        info3.completed = true;
        info3.DimR = new Vector3(convertToMeter(3), convertToMeter(1), convertToMeter(3));
       // info3.DimXR = convertToMeter(3);
       // info3.DimYR = convertToMeter(1);
       // info3.DimZR = convertToMeter(3);
        info3.genID(objList.Count);
        // info3.objID = 3;

        objList.Add(info3);

        ObjectInfoClass info4 = new ObjectInfoClass(test4);
        image = Resources.Load("Icons/ball4") as Texture2D;
        //info4.ImageIcon = Sprite.Create(image, new Rect(0.0f, 0.0f, image.width, image.height), new Vector2(0.5f, 0.5f), 100.0f);
        info4.ImageIconPath = "Icons/ball4";
        info4.Name = "Ultra Ball";
        info4.RoomNum = roomNum;
        info4.FloorNum = roomFloorNum;
        info4.completed = true;
        info4.DimR = new Vector3(convertToMeter(3), convertToMeter(1), convertToMeter(3));
       // info4.DimXR = convertToMeter(3);
       // info4.DimYR = convertToMeter(1);
       // info4.DimZR = convertToMeter(3);
        info4.genID(objList.Count);
        // info4.objID = 4;

        objList.Add(info4);

        ObjectInfoClass info5 = new ObjectInfoClass(test5);
        image = Resources.Load("Icons/ball5") as Texture2D;
        //info5.ImageIcon = Sprite.Create(image, new Rect(0.0f, 0.0f, image.width, image.height), new Vector2(0.5f, 0.5f), 100.0f);
        info5.ImageIconPath = "Icons/ball5";
        info5.Name = "MasterBall";
        info5.RoomNum = roomNum;
        info5.FloorNum = roomFloorNum;
        info5.completed = true;
        info5.DimR = new Vector3(convertToMeter(3), convertToMeter(1), convertToMeter(3));
       // info5.DimXR = convertToMeter(3);
       // info5.DimYR = convertToMeter(1);
       // info5.DimZR = convertToMeter(3);
        info5.genID(objList.Count);
        // info5.objID = 5;

        objList.Add(info5);

       // Debug.Log("Added objs");

    }
    */

    float convertToMeter (float num)
    {
        return num * 100;
    }





}
