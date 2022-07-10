using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SaveLoadSystem;

public class ControllerClass
{
    CameraClass cameraClass;
    ModObjClass modifyObj;

    Button PerspOrthoTog;
    Text PerspOrthoTogText;
    Button Front;
    Button Side;
    Button Top;
    //Button Sky;


    //Mod Controllers
    Button PlusX;
    Button PlusY;
    Button PlusZ;
    Button MinusX;
    Button MinusY;
    Button MinusZ;

    InputField InX;
    InputField InY;
    InputField InZ;

    Text InXText;
    Text InYText;
    Text InZText;

    Button Save;



    Vector3 Vals;
    //Vals in scale units
   // float ValX;
   // float ValY;
   // float ValZ;


    bool PerspOrtho; //True = persp False = ortho

    float OneUnit = 10f;



    public ControllerClass(CameraClass cam, ModObjClass modObj)
    {
        //Set Class Variables
        this.cameraClass = cam;
        this.modifyObj = modObj;




        //Get all the Buttons 
        PerspOrthoTog = GameObject.Find("OrthoPersp").GetComponent<Button>();
        Front = GameObject.Find("Front").GetComponent<Button>();
        Side = GameObject.Find("Side").GetComponent<Button>();
        Top = GameObject.Find("Top").GetComponent<Button>();

        PlusX = GameObject.Find("PlusX").GetComponent<Button>();
        PlusY = GameObject.Find("PlusY").GetComponent<Button>();
        PlusZ = GameObject.Find("PlusZ").GetComponent<Button>();

        MinusX = GameObject.Find("MinusX").GetComponent<Button>();
        MinusY = GameObject.Find("MinusY").GetComponent<Button>();
        MinusZ = GameObject.Find("MinusZ").GetComponent<Button>();

        InX = GameObject.Find("SizeX").GetComponent<InputField>();
        InY = GameObject.Find("SizeY").GetComponent<InputField>();
        InZ = GameObject.Find("SizeZ").GetComponent<InputField>();


        Save = GameObject.Find("SaveButton").GetComponent<Button>();


        PerspOrthoTogText = GameObject.Find("OrthoText").GetComponent<Text>();

        InXText = InX.textComponent;
        InYText = InY.textComponent;
        InZText = InZ.textComponent;

        //Set Regular Varibles
        PerspOrtho = false;

        Vals = modifyObj.iDim;
       // ValX = modObj.IdimX;
       // ValY = modObj.IdimY;
       // ValZ = modObj.IdimZ;



        //Set up Listeners
        PerspOrthoTog.onClick.AddListener(POTog);
        Front.onClick.AddListener(FrontView);
        Side.onClick.AddListener(SideView);
        Top.onClick.AddListener(TopView);

        PlusX.onClick.AddListener(PlusXBut);
        PlusY.onClick.AddListener(PlusYBut);
        PlusZ.onClick.AddListener(PlusZBut);

        MinusX.onClick.AddListener(MinusXBut);
        MinusY.onClick.AddListener(MinusYBut);
        MinusZ.onClick.AddListener(MinusZBut);

        InX.onEndEdit.AddListener(delegate { InputX(); });
        InY.onEndEdit.AddListener(delegate { InputY(); });
        InZ.onEndEdit.AddListener(delegate { InputZ(); });

        //Set Things
        InXText.horizontalOverflow = HorizontalWrapMode.Wrap;
        InYText.horizontalOverflow = HorizontalWrapMode.Wrap;
        InZText.horizontalOverflow = HorizontalWrapMode.Wrap;

        Save.onClick.AddListener(SaveObject);

        //Set View to FrontView
        FrontView();

        UpdateInputFields();

    }


    void POTog()
    {
        if (PerspOrtho)
        {
            FrontView();

        }
        else
        {
            PerspView();
        }
    }

    void FrontView()
    {
        cameraClass.VNum = 1;
        cameraClass.UpdateCamView();
        PerspOrtho = false;
        FixButText(PerspOrtho);
    }

    void SideView()
    {


        cameraClass.VNum = 2;
        cameraClass.UpdateCamView();
        PerspOrtho = false;
        FixButText(PerspOrtho);
    }

    void TopView()
    {

        cameraClass.VNum = 3;
        cameraClass.UpdateCamView();
        PerspOrtho = false;
        FixButText(PerspOrtho);
    }

    void PerspView()
    {
        //Make the view perspective
        cameraClass.VNum = 4;
        cameraClass.UpdateCamView();
        PerspOrtho = true;
        FixButText(PerspOrtho);
    }



    void PlusXBut()
    {
        Vals.x += OneUnit;
        UpdateInputFields();

    }

    void PlusYBut()
    {
        Vals.y += OneUnit;
        UpdateInputFields();
    }
    void PlusZBut()
    {
        Vals.z += OneUnit;
        UpdateInputFields();
    }
    void MinusXBut()
    {
        Vals.x -= OneUnit;
        UpdateInputFields();
    }
    void MinusYBut()
    {
        Vals.y -= OneUnit;
        UpdateInputFields();
    }
    void MinusZBut()
    {
        Vals.z -= OneUnit;
        UpdateInputFields();
    }

    void FixButText(bool bo)
    {

        if (bo)
        {
            PerspOrthoTogText.text = "3D";
        }
        else
        {
            PerspOrthoTogText.text = "2D";
        }

    }

    void UpdateInputFields()
    {
        InX.text = Vals.x / 100 + "m";
        InY.text = Vals.y / 100 + "m";
        InZ.text = Vals.z / 100 + "m";
        modifyObj.UpdateSizeVals(TransferValsSize());
        cameraClass.UpdateCamView();
    }

    void InputX()
    {
        float oldVal = Vals.x;
        float newVal;
        try
        {
            newVal = float.Parse(InX.text);
            Vals.x = newVal * 100;
            UpdateInputFields();
        }
        catch
        {
            Vals.x = DNAMath.getMeasurement(InX.text, oldVal) * 100;

            UpdateInputFields();
        }
    }

    void InputY()
    {
        float oldVal = Vals.y;
        float newVal;
        try
        {
            newVal = float.Parse(InY.text);
            Vals.y = newVal * 100;
            UpdateInputFields();
        }
        catch
        {
            Vals.y = DNAMath.getMeasurement(InY.text, oldVal) * 100;
            UpdateInputFields();
        }
    }

    void InputZ()
    {
        float oldVal = Vals.z;
        float newVal;
        try
        {
            newVal = float.Parse(InZ.text);
            Vals.z = newVal * 100;
            UpdateInputFields();
        }
        catch
        {
            Vals.z = DNAMath.getMeasurement(InZ.text, oldVal) * 100;
            UpdateInputFields();
        }
    }

    public Vector3 TransferValsSize()
    {
        //float[] vals = new float[3];
        //vals[0] = ValX;
        //vals[1] = ValY;
        //vals[2] = ValZ;

        return Vals;
    }

    public bool TargetBool(bool target)
    {
        return target;
    }

    void SaveObject ()
    {
        if (modifyObj.CheckReq())
        {
            SaveSystemManager.Load();
            //Add a system that loads the infopackage, modify's it to the current information in the world, puts the completd to yes and then force pushes it into the HouseClass by checking if it matches an ID
            //Then save it to storage for tomorrow 

           // ObjectInfoClass info = (ObjectInfoClass)SaveDataClass.loadData(Application.persistentDataPath + "/" + "EditObject.txt", "ObjectInfoClass");

            ObjectInfoClass info = SaveSystemManager.CurrentSaveData.editObject;

            info.Dim = modifyObj.Dim / 2;
            info.gameObj.transform.localScale = info.Dim;

            info.gameObj = modifyObj.gameObj;

           

           // info.DimX = modifyObj.dimX;
           // info.DimY = modifyObj.dimY;
           // info.DimZ = modifyObj.dimZ;
            info.completed = true;

           

            HouseClass house = SaveSystemManager.LoadLevel(1);
            house = SaveSystemManager.CurrentSaveData.currentHouse;
           // HouseClass house = (HouseClass)SaveDataClass.loadData(Application.persistentDataPath + "/" + "House" + "/" + "House.txt", "HouseClass");

            Debug.Log(info.objID);

            if (info != null && house != null )
            {
                for (int i = 0; i < house.floorList.Count; i++)
                {
                    for (int j = 0; j < house.floorList[i].roomList.Count; j++)
                    {
                        for (int g = 0; g < house.floorList[i].roomList[j].objList.Count; g++)
                        {
                            Debug.Log(house.floorList[i].roomList[j].objList[g].objID);
                            if (info.objID.Equals(house.floorList[i].roomList[j].objList[g].objID))
                            {
                                house.floorList[i].roomList[j].objList[g] = info;
                                Debug.Log("Match");
                            }
                        }
                    }
                }
            } else
            {
                Debug.Log("Failing");
            }


            //SaveDataClass.saveHouse(house);
            SaveSystemManager.SaveLevel(1, house);
            SaveSystemManager.CurrentSaveData.currentHouse = house;
            SaveSystemManager.Save();

            Debug.Log("Back to Object List");

            UnityEngine.SceneManagement.SceneManager.LoadScene(4);
        } else
        {



            DialogueTrigger idk = GameObject.Find("ObjectView").GetComponent<DialogueTrigger>();

            idk.TriggerDialogue();


        }
    }

  





}
