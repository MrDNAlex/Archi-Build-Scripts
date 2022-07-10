using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SaveLoadSystem;

public class HouseBuildScript : MonoBehaviour
{

    [SerializeField] Camera cam;
    [SerializeField] RectTransform stream;
    [SerializeField] RectTransform panel;

    [Header("Sections")]
    [SerializeField] RectTransform buttons;
    [SerializeField] RectTransform controller;
    [SerializeField] RectTransform complete;

    [Header("Buttons Sections")]
    [SerializeField] RectTransform floorCont;
    [SerializeField] RectTransform roomCont;

    [SerializeField] RectTransform floorDrop;
    [SerializeField] RectTransform roomDrop;
    [SerializeField] RectTransform insideTog;
    [SerializeField] RectTransform modifyTog;

    [Header("Controller Sections")]
    [SerializeField] RectTransform MaterialSelect;
    [SerializeField] RectTransform ObjectSelect;
    [SerializeField] RectTransform MaterialSelectDrop;
    [SerializeField] RectTransform ObjectSelectDrop;
    [SerializeField] RectTransform XTitle;
    [SerializeField] RectTransform ControllerX;
    [SerializeField] RectTransform MinusX;
    [SerializeField] RectTransform SizeX;
    [SerializeField] RectTransform PlusX;
    [SerializeField] RectTransform YTitle;
    [SerializeField] RectTransform ControllerY;
    [SerializeField] RectTransform MinusY;
    [SerializeField] RectTransform SizeY;
    [SerializeField] RectTransform PlusY;
    [SerializeField] RectTransform ZTitle;
    [SerializeField] RectTransform ControllerZ;
    [SerializeField] RectTransform MinusZ;
    [SerializeField] RectTransform SizeZ;
    [SerializeField] RectTransform PlusZ;
    [SerializeField] RectTransform Rotate;
    [SerializeField] RectTransform Remove;

    [Header("Complete Sections")]
    [SerializeField] RectTransform completeButton;


    [Header("Final Stuff")]
    [SerializeField] Camera finalCam;
    [SerializeField] Texture streamTexture;
    [SerializeField] Material streamMat;
    [SerializeField] GameObject House;
    [SerializeField] GameObject decoration;

    [SerializeField] Texture StreamTexture2;
    [SerializeField] Material streamMat2;

    [SerializeField] TimerScript timer;

    [Header("Final Panel")]
    [SerializeField] RectTransform finalPanel;

    [SerializeField] RectTransform Title;
    [SerializeField] RectTransform Achievements;
    [SerializeField] RectTransform Points;
    [SerializeField] RectTransform ThankYou;

    [Header("Achievements")]
    //Achievements
    [SerializeField] RectTransform AchievementTitle;
    [SerializeField] RectTransform AchievementImages;
    [SerializeField] RectTransform Achievement1;
    [SerializeField] RectTransform Achievement2;
    [SerializeField] RectTransform Achievement3;
    [SerializeField] RectTransform Achievement4;
    [SerializeField] RectTransform Achievement5;

    //Points
    [Header("Points")]
    [SerializeField] RectTransform PointsTitle;
    [SerializeField] RectTransform PointSection;

    [SerializeField] RectTransform Section1;
    [SerializeField] RectTransform Section1Title;
    [SerializeField] RectTransform Section1Points;

    [SerializeField] RectTransform Section2;
    [SerializeField] RectTransform Section2Title;
    [SerializeField] RectTransform Section2Points;

    [SerializeField] RectTransform Section3;
    [SerializeField] RectTransform Section3Title;
    [SerializeField] RectTransform Section3Points;

    [SerializeField] RectTransform Section4;
    [SerializeField] RectTransform Section4Title;
    [SerializeField] RectTransform Section4Points;

    [SerializeField] RectTransform Section5;
    [SerializeField] RectTransform Section5Title;
    [SerializeField] RectTransform Section5Points;

    [SerializeField] RectTransform PointTotal;
    [SerializeField] RectTransform PointTotalTitle;
    [SerializeField] RectTransform PointTotalpnt;

    //Thank you
    [Header("Thank You")]
    [SerializeField] RectTransform ThankYouSec;
    [SerializeField] RectTransform ThankYouText;

    [Header("Materials")]
    [Header("WallMats")]
    [SerializeField] Material wallMat1;
    [SerializeField] Material wallMat2;
    [SerializeField] Material wallMat3;
    [SerializeField] Material wallMat4;
    [SerializeField] Material wallMat5;

    [Header("floorMat")]
    [SerializeField] Material floorMat1;
    [SerializeField] Material floorMat2;

    [SerializeField] Material roofMat;
    [SerializeField] Material exteriorWall;

    HouseClass house;

    Dropdown materialSelectDropDown;
    Dropdown objectSelectDropDown;
    Text materialSelectDropDownText;
    Text objectSelectDropDownText;
    Button XPlus;
    Button XMinus;
    Button YPlus;
    Button YMinus;
    Button ZPlus;
    Button ZMinus;

    InputField InX;
    InputField InY;
    InputField InZ;

    Text InXText;
    Text InYText;
    Text InZText;

    Button modifyTogBtn;
    Text modifyTogText;

    Button removeBtn;
    Text removeText;

    Button rotateBtn;

    Button insideTogBtn;
    Text insideTogText;
    Button completeBtn;
    Dropdown floorDropDown;
    Dropdown roomDropDown;
    Text roomDropDownText;
    Text floorDropDownText;

    Material currentMat;
    GameObject currentObject;

    Vector3 outsidePos = new Vector3(20, 8, -9);
    Vector3 outsideRot = new Vector3(20, 0, 0);

    Vector3 insidePos;
    Vector3 insideRot = Vector3.zero;


    Vector3 objPos;
    Vector3 objRot;

    Vector3 outOfBounds = new Vector3(10000, 10000, 10000);

    int currentLayer;
    int roomCount;

    bool inside = false;
    bool modify = false;
    bool mouseDown;
    bool validMat;
    bool validObject;
    bool validDelete;

    float FPS;
    float OneUnit = 0.1f;


    //End Stuff
    float switchTime = 24f;
    float firstTimer = 5f;
    float curTime = 0;
    bool gameComplete = false;
    bool pointSysStart = true;

    float pointTimer = 7f;
    float curPointTimer = 0;
    float pointType = 0;


    float floorPoints;
    float roomPoints;
    float objPoints;
    float decPoints;
    float timePoints;

    int floorNum;
    int roomNum;
    int objNum;
    int decNum;
    float timeElap;

    float totalPoints;

    Image AchievementImage1;
    Image AchievementImage2;
    Image AchievementImage3;
    Image AchievementImage4;
    Image AchievementImage5;

    Text PointSec1T;
    Text PointSec1P;

    Text PointSec2T;
    Text PointSec2P;

    Text PointSec3T;
    Text PointSec3P;

    Text PointSec4T;
    Text PointSec4P;

    Text PointSec5T;
    Text PointSec5P;

    Text PointFinalT;
    Text PointFinalP;

    PointSystem floorTMan;
    PointSystem floorPMan;

    PointSystem roomTMan;
    PointSystem roomPMan;

    PointSystem objectTMan;
    PointSystem objectPMan;

    PointSystem decoTMan;
    PointSystem decoPMan;

    PointSystem TimeTMan;
    PointSystem TimePMan;

    PointSystem TotalPointsMan;

    //List of positions of every room
    List<Vector3> roomPositions = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 120;


        loadInfo();
        setUI();
        getUI();
        updateFloorDrop();

        updateHouseStruct();

        insideTogText.text = "Outside";

        DialogueTrigger idk = this.gameObject.GetComponent<DialogueTrigger>();

        idk.TriggerDialogue();
    }

    // Update is called once per frame
    void Update()
    {

        FPS = 1 / Time.deltaTime;

        if (gameComplete)
        {
            //Spinning camera stuff
            updateTimer();

            pointController();

        }
        else
        {
            if (Input.GetKey(KeyCode.Mouse0) || Input.touchCount > 0)
            {
                mouseDown = true;
                applyMaterial();
                placeObject();
            }
            else
            {
                mouseDown = false;
            }

            if (inside)
            {
                if (!DNAMath.offScreen(0.75f, true, true))
                {
                    updateRot();
                }
            }
        }

    }

    void loadInfo()
    {
        SaveSystemManager.Load();
        house = SaveSystemManager.CurrentSaveData.currentHouse;
    }

    void setUI()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float streamWidth = Screen.width * 0.75f;
        float panelWidth = Screen.width * 0.25f;

        float horPad = panel.gameObject.GetComponent<VerticalLayoutGroup>().padding.horizontal;
        float vertPad = panel.gameObject.GetComponent<VerticalLayoutGroup>().padding.vertical;
        float spacing = panel.gameObject.GetComponent<VerticalLayoutGroup>().spacing * 2;

        float panelHeight = screenHeight - (vertPad + spacing);

        float everythingWidth = panelWidth - horPad;


        float buttonSec = (panelHeight * 0.3f);

        float buttonHeight = buttonSec / 4;

        float controllerSec = (panelHeight * 0.7f) - buttonHeight;

        float textHeight = (controllerSec - (buttonHeight * 7)) / 3;

        float sizeWidth = everythingWidth - (buttonHeight * 2);

        //Maybe move this to later
        StreamTexture2.width = (int)Mathf.Floor(screenWidth);
        StreamTexture2.height = (int)Mathf.Floor(screenHeight);


        streamTexture.width = (int)Mathf.Floor(streamWidth);
        streamTexture.height = (int)Mathf.Floor(screenHeight);

        stream.sizeDelta = new Vector2(streamWidth, screenHeight);

        panel.sizeDelta = new Vector2(panelWidth, screenHeight);

        buttons.sizeDelta = new Vector2(everythingWidth, buttonSec);
        controller.sizeDelta = new Vector2(everythingWidth, controllerSec);

        floorCont.sizeDelta = new Vector2(everythingWidth, buttonHeight);
        roomCont.sizeDelta = new Vector2(everythingWidth, buttonHeight);

        floorDrop.sizeDelta = new Vector2(everythingWidth, buttonHeight);
        roomDrop.sizeDelta = new Vector2(everythingWidth, buttonHeight);
        insideTog.sizeDelta = new Vector3(everythingWidth, buttonHeight);
        modifyTog.sizeDelta = new Vector3(everythingWidth, buttonHeight);


        MaterialSelect.sizeDelta = new Vector3(everythingWidth, buttonHeight);
        ObjectSelect.sizeDelta = new Vector3(everythingWidth, buttonHeight);

        MaterialSelectDrop.sizeDelta = new Vector3(everythingWidth, buttonHeight);
        ObjectSelectDrop.sizeDelta = new Vector3(everythingWidth, buttonHeight);

        ControllerX.sizeDelta = new Vector3(everythingWidth, buttonHeight);
        ControllerY.sizeDelta = new Vector3(everythingWidth, buttonHeight);
        ControllerZ.sizeDelta = new Vector3(everythingWidth, buttonHeight);

        //X Section
        PlusX.sizeDelta = new Vector3(buttonHeight, buttonHeight);
        SizeX.sizeDelta = new Vector3(sizeWidth, buttonHeight);
        MinusX.sizeDelta = new Vector3(buttonHeight, buttonHeight);

        //Y Section
        PlusY.sizeDelta = new Vector3(buttonHeight, buttonHeight);
        SizeY.sizeDelta = new Vector3(sizeWidth, buttonHeight);
        MinusY.sizeDelta = new Vector3(buttonHeight, buttonHeight);

        //Z Section
        PlusZ.sizeDelta = new Vector3(buttonHeight, buttonHeight);
        SizeZ.sizeDelta = new Vector3(sizeWidth, buttonHeight);
        MinusZ.sizeDelta = new Vector3(buttonHeight, buttonHeight);

        XTitle.sizeDelta = new Vector3(everythingWidth, textHeight);
        YTitle.sizeDelta = new Vector3(everythingWidth, textHeight);
        ZTitle.sizeDelta = new Vector3(everythingWidth, textHeight);

        Rotate.sizeDelta = new Vector3(everythingWidth, buttonHeight);
        Remove.sizeDelta = new Vector3(everythingWidth, buttonHeight);

        complete.sizeDelta = new Vector2(everythingWidth, buttonHeight);
        completeButton.sizeDelta = new Vector2(everythingWidth, buttonHeight);

    }

    void getUI()
    {
        completeBtn = completeButton.gameObject.GetComponent<Button>();

        floorDropDown = floorDrop.gameObject.GetComponent<Dropdown>();
        roomDropDown = roomDrop.gameObject.GetComponent<Dropdown>();

        floorDropDownText = floorDrop.gameObject.transform.Find("Label").GetComponent<Text>();
        roomDropDownText = roomDrop.gameObject.transform.Find("Label").GetComponent<Text>();

        insideTogBtn = insideTog.gameObject.GetComponent<Button>();
        insideTogText = insideTog.gameObject.transform.Find("InsideText").GetComponent<Text>();

        modifyTogBtn = modifyTog.gameObject.GetComponent<Button>();
        modifyTogText = modifyTog.gameObject.transform.Find("InsideText").GetComponent<Text>();

        materialSelectDropDown = MaterialSelectDrop.gameObject.GetComponent<Dropdown>();
        objectSelectDropDown = ObjectSelectDrop.gameObject.GetComponent<Dropdown>();

        materialSelectDropDownText = MaterialSelectDrop.gameObject.transform.Find("Label").GetComponent<Text>();
        objectSelectDropDownText = ObjectSelectDrop.gameObject.transform.Find("Label").GetComponent<Text>();

        XPlus = PlusX.gameObject.GetComponent<Button>();
        XMinus = MinusX.gameObject.GetComponent<Button>();
        YPlus = PlusY.gameObject.GetComponent<Button>();
        YMinus = MinusY.gameObject.GetComponent<Button>();
        ZPlus = PlusZ.gameObject.GetComponent<Button>();
        ZMinus = MinusZ.gameObject.GetComponent<Button>();

        InX = SizeX.gameObject.GetComponent<InputField>();
        InY = SizeY.gameObject.GetComponent<InputField>();
        InZ = SizeZ.gameObject.GetComponent<InputField>();

        InXText = InX.textComponent;
        InYText = InY.textComponent;
        InZText = InZ.textComponent;

        rotateBtn = Rotate.GetComponent<Button>();
        removeBtn = Remove.GetComponent<Button>();

        //Set Listeners 
        Debug.Log("Listeners");

        insideTogBtn.onClick.AddListener(toggleInside);

        InX.onEndEdit.AddListener(delegate { InputX(); });
        InY.onEndEdit.AddListener(delegate { InputY(); });
        InZ.onEndEdit.AddListener(delegate { InputZ(); });


        XPlus.onClick.AddListener(PlusXBut);
        YPlus.onClick.AddListener(PlusYBut);
        ZPlus.onClick.AddListener(PlusZBut);

        XMinus.onClick.AddListener(MinusXBut);
        YMinus.onClick.AddListener(MinusYBut);
        ZMinus.onClick.AddListener(MinusZBut);

        modifyTogBtn.onClick.AddListener(modifyToggle);

        rotateBtn.onClick.AddListener(rotateObj);

        removeBtn.onClick.AddListener(removeObject);

        completeBtn.onClick.AddListener(completeBuild);

    }

    void updateFloorDrop()
    {
        // Debug.Log("Update Floor Drop");
        // int lastFloor = floorDropDown.value;

        floorDropDown.ClearOptions();

        //  debug.text = "cleared";

        for (int i = 0; i < house.floorList.Count; i++)
        {
            //Replace the number with i probably?
            floorDropDown.options.Add(new Dropdown.OptionData() { text = "Floor " + (i+1) });
        }

        // floorNum = floorDropDown.options.Count;

        floorDropDown.onValueChanged.RemoveAllListeners();

        // debug.text = "cleared 2";

        floorDropDown.onValueChanged.AddListener(delegate
        {
            dropDownItemSelectedFloor(floorDropDown);
            //floorNum = floorDropDown.value;
            updateRoomDrop();
        });

        //Change this to last floor

        floorDropDown.value = 0;

        dropDownItemSelectedFloor(floorDropDown);

        updateRoomDrop();
        updateMaterialDrop();
        updateObjectDrop();
    }

    void updateRoomDrop()
    {

        roomDropDown.interactable = true;
        roomDropDown.ClearOptions();

        for (int i = 0; i < house.floorList[floorDropDown.value].roomList.Count; i++)
        {
            roomDropDown.options.Add(new Dropdown.OptionData() { text = "Room " + (i+1) });
        }

      //  roomNum = roomDropDown.options.Count;

        roomDropDown.onValueChanged.RemoveAllListeners();

        roomDropDown.onValueChanged.AddListener(delegate
        {
            dropDownItemSelectedRoom(roomDropDown);

            updateCamPos();

        });

        roomDropDown.value = 0;

        dropDownItemSelectedRoom(roomDropDown);

        updateCamPos();
    }

    void updateMaterialDrop()
    {
        materialSelectDropDown.interactable = true;
        materialSelectDropDown.ClearOptions();

        for (int i = 0; i < 9; i++)
        {

            switch (i)
            {

                case 0:
                    materialSelectDropDown.options.Add(new Dropdown.OptionData() { text = "None" });
                    break;
                case 1:
                    materialSelectDropDown.options.Add(new Dropdown.OptionData() { text = "Dark Blue" });
                    break;
                case 2:
                    materialSelectDropDown.options.Add(new Dropdown.OptionData() { text = "Teal" });
                    break;
                case 3:
                    materialSelectDropDown.options.Add(new Dropdown.OptionData() { text = "Dark Purple" });
                    break;
                case 4:
                    materialSelectDropDown.options.Add(new Dropdown.OptionData() { text = "Beige" });
                    break;
                case 5:
                    materialSelectDropDown.options.Add(new Dropdown.OptionData() { text = "Light Lime" });
                    break;
                case 6:
                    materialSelectDropDown.options.Add(new Dropdown.OptionData() { text = "Sand Tiles" });
                    break;
                case 7:
                    materialSelectDropDown.options.Add(new Dropdown.OptionData() { text = "Oak Planks" });
                    break;
                case 8:
                    materialSelectDropDown.options.Add(new Dropdown.OptionData() { text = "Brick Wall" });
                    break;
            }

        }


        materialSelectDropDown.onValueChanged.RemoveAllListeners();

        materialSelectDropDown.onValueChanged.AddListener(delegate
        {
            dropDownItemSelectedMaterial(materialSelectDropDown);

            currentMat = getMaterial(materialSelectDropDown.value);

            updateObjectDrop();

        });

        dropDownItemSelectedMaterial(materialSelectDropDown);

        currentMat = getMaterial(materialSelectDropDown.value);

    }

    void updateObjectDrop()
    {
        objectSelectDropDown.interactable = true;

        objectSelectDropDown.interactable = true;
        objectSelectDropDown.ClearOptions();

        for (int i = 0; i < 9; i++)
        {

            switch (i)
            {
                case 0:
                    objectSelectDropDown.options.Add(new Dropdown.OptionData() { text = "None" });
                    break;
                case 1:
                    objectSelectDropDown.options.Add(new Dropdown.OptionData() { text = "Chair" });
                    break;
                case 2:
                    objectSelectDropDown.options.Add(new Dropdown.OptionData() { text = "Plant" });
                    break;
                case 3:
                    objectSelectDropDown.options.Add(new Dropdown.OptionData() { text = "Closet" });
                    break;
                case 4:
                    objectSelectDropDown.options.Add(new Dropdown.OptionData() { text = "Small Table" });
                    break;
                case 5:
                    objectSelectDropDown.options.Add(new Dropdown.OptionData() { text = "Desk" });
                    break;
                case 6:
                    objectSelectDropDown.options.Add(new Dropdown.OptionData() { text = "Stand" });
                    break;
                case 7:
                    objectSelectDropDown.options.Add(new Dropdown.OptionData() { text = "Sofa" });
                    break;
                case 8:
                    objectSelectDropDown.options.Add(new Dropdown.OptionData() { text = "Carpet" });
                    break;
            }

        }


        objectSelectDropDown.onValueChanged.RemoveAllListeners();

        objectSelectDropDown.onValueChanged.AddListener(delegate
        {
            dropDownItemSelectedObject(objectSelectDropDown);
            currentObject = getObject(objectSelectDropDown.value);

            // currentMat = getMaterial(materialSelectDropDown.value);
            updateMaterialDrop();
        });

        dropDownItemSelectedObject(objectSelectDropDown);
        currentObject = getObject(objectSelectDropDown.value);
        // currentMat = getMaterial(materialSelectDropDown.value);
    }

    void dropDownItemSelectedRoom(Dropdown dropdown)
    {
        roomDropDownText.text = dropdown.options[dropdown.value].text;
    }
    void dropDownItemSelectedFloor(Dropdown dropdown)
    {
        floorDropDownText.text = dropdown.options[dropdown.value].text;
    }

    void dropDownItemSelectedMaterial(Dropdown dropdown)
    {
        materialSelectDropDownText.text = dropdown.options[dropdown.value].text;
    }

    void dropDownItemSelectedObject(Dropdown dropdown)
    {
        objectSelectDropDownText.text = dropdown.options[dropdown.value].text;
    }


    void updateHouseStruct()
    {
        // Debug.Log(house.ToString());
        //Debug.Log("Updating House");
        foreach (Transform child in House.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        GameObject houseObj = new GameObject();
        houseObj.name = "HouseEntity";
        houseObj.transform.parent = House.transform;
        houseObj.transform.localPosition = house.housePos;
        genHouse(house, houseObj);

        Vector3 offset = new Vector3(-2, 2, 0);

        spawnRoof("Roof", DNAMath.determineCorner(house.housePos * 2 + offset, house.housePos * 2 + house.houseDim + offset, 5), new Vector3((house.houseDim.x / 4 * 100) * 1.05f, (house.houseDim.z / 4 * 100) * 0.8f, 300), 5);
        //spawnFloor("Roof", DNAMath.determineCorner(roomClass.roomPos + new Vector3(0, 2.7f, 0), roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, floorHeight, roomClass.roomDim.z / 2 * 100), 1, parent, 3);

        for (int i = 0; i < house.floorList.Count; i++)
        {

            //Gen Exterior of House



            // if (floorDropDown.value == i)
            //  {
            GameObject floor = new GameObject();
            floor.name = "Floor " + i;
            floor.transform.parent = houseObj.transform;
            floor.transform.localPosition = Vector3.zero + (new Vector3(0, 3, 0) * i);

            for (int j = 0; j < house.floorList[i].roomList.Count; j++)
            {
                GameObject room = new GameObject();
                room.name = "Room " + j;
                room.transform.parent = floor.transform;
                room.transform.localPosition = Vector3.zero;

                //Debug.Log(floorClasses[i].roomList[j].roomDim);

                // Debug.Log("Spawning room");

                // Debug.Log("I: "+ i);
                //  Debug.Log("J: " + j);

                switch (house.floorList[i].roomList[j].roomType)
                {
                    case "Room":
                        genRoom(house.floorList[i].roomList[j], room);
                        break;
                    case "Hallway":
                        genHallway(house.floorList[i].roomList[j], room);
                        break;
                    case "Stairs":
                        genStairs(house.floorList[i].roomList[j], room);
                        break;
                }

                //  Debug.Log("Room Spawned");


            }
            // }

        }

    }

    public void spawnRoof(string name, Vector3 pos, Vector3 dim, int pointNum)
    {
        GameObject roof = new GameObject();

        roof = GameObject.Instantiate(Resources.Load("Models/newroof")) as GameObject;

        roof.name = name;
        // roof.transform.SetParent(parent.transform);
        roof.transform.localScale = dim;
        roof.transform.localPosition = DNAMath.alignToPoint(pos, dim, pointNum);
        roof.transform.rotation = Quaternion.Euler(-90, 0, 0);
        roof.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;


        // roof.GetComponent<MeshRenderer>().material.color = Color.green;
        roof.AddComponent<MeshCollider>();
    }

    public void spawnWall(string name, Vector3 pos, Vector3 dim, int pointNum, GameObject parent, int matChoice)
    {
        GameObject wall = new GameObject();

        wall = GameObject.Instantiate(Resources.Load("Models/floor_dec_tile")) as GameObject;

        wall.name = name;
        wall.transform.SetParent(parent.transform);
        wall.transform.localScale = dim;
        wall.transform.localPosition = DNAMath.alignToPoint(pos, dim, pointNum);
        wall.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;


        wall.GetComponent<MeshRenderer>().material.color = Color.green;
        wall.AddComponent<MeshCollider>();

        switch (matChoice)
        {
            case 1:
                wall.GetComponent<MeshRenderer>().material = wallMat1;
                break;
            case 2:
                wall.GetComponent<MeshRenderer>().material = exteriorWall;
                break;
        }

    }

    public void spawnFloor(string name, Vector3 pos, Vector3 dim, int pointNum, GameObject parent, int matChoice)
    {
        GameObject floor = new GameObject();

        floor = GameObject.Instantiate(Resources.Load("Models/floor_dec_tile")) as GameObject;
        floor.name = name;
        floor.transform.SetParent(parent.transform);
        floor.transform.localScale = dim;
        floor.transform.localPosition = DNAMath.alignToPoint(pos, dim, pointNum);
        floor.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        floor.AddComponent<MeshCollider>();
        switch (matChoice)
        {
            case 1:
                floor.GetComponent<MeshRenderer>().material = floorMat1;
                break;
            case 2:
                floor.GetComponent<MeshRenderer>().material = floorMat2;
                break;
            case 3:
                floor.GetComponent<MeshRenderer>().material = roofMat;
                break;
        }


    }

    public void spawnStairs(string name, Vector3 pos, Vector3 dim, float spinAngle, int pointNum, GameObject parent, bool horizontal)
    {
        GameObject stairs = new GameObject();

        stairs = GameObject.Instantiate(Resources.Load("Models/stairs")) as GameObject;

        stairs.name = name;
        stairs.transform.SetParent(parent.transform);
        stairs.transform.localScale = dim;

        stairs.AddComponent<MeshCollider>();
        if (horizontal)
        {
            stairs.transform.localPosition = DNAMath.alignToPoint(pos, new Vector3(dim.z, dim.y, dim.x), pointNum);
        }
        else
        {
            stairs.transform.localPosition = DNAMath.alignToPoint(pos, dim, pointNum);
        }


        Quaternion angles = Quaternion.Euler(0, spinAngle, 0);
        stairs.transform.rotation = angles;
        stairs.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        stairs.GetComponent<MeshRenderer>().material = floorMat1;


        // Debug.Log(DNAMath.alignToPoint(pos, dim, pointNum));

    }

    public void genStairs(RoomClass roomClass, GameObject parent)
    {

        //Default orientation is Down
        switch (roomClass.roomDirVec)
        {
            case "Up":
                //   ^
                //   |
                spawnStairs("Stairs", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, 150, roomClass.roomDim.z / 2 * 100), 0, 1, parent, false);

                // Debug.Log(DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1));
                break;
            case "Right":
                //  -->
                spawnStairs("Stairs", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.z / 2 * 100, 150, roomClass.roomDim.x / 2 * 100), 90, 1, parent, true);

                // Debug.Log(DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1));
                break;
            case "Left":
                //  <--
                spawnStairs("Stairs", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.z / 2 * 100, 150, roomClass.roomDim.x / 2 * 100), 270, 1, parent, true);
                break;
            case "Down":
                spawnStairs("Stairs", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, 150, roomClass.roomDim.z / 2 * 100), 180, 1, parent, false);
                break;
            default:

                break;

        }
    }

    public void genHallway(RoomClass roomClass, GameObject parent)
    {
        float floorHeight = 15;
        spawnFloor("Floor", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, floorHeight, roomClass.roomDim.z / 2 * 100), 1, parent, 2);
        spawnFloor("Roof", DNAMath.determineCorner(roomClass.roomPos + new Vector3(0, 2.7f, 0), roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, floorHeight, roomClass.roomDim.z / 2 * 100), 1, parent, 3);
    }

    public void genRoom(RoomClass roomClass, GameObject parent)
    {

        float wallWidth = 15;
        float wallHeight = 150;
        float floorHeight = 15;

        switch (roomClass.roomDirVec)
        {
            case "Up":
                //   ^
                //   |

                spawnWall("Wall 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, wallHeight, wallWidth), 1, parent, 1);
                spawnWall("Wall 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 5), new Vector3(wallWidth, wallHeight, (roomClass.roomDim.z / 2 * 100) - wallWidth * 2), 5, parent, 1);
                spawnWall("Wall 3", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 7), new Vector3(wallWidth, wallHeight, (roomClass.roomDim.z / 2 * 100) - wallWidth * 2), 7, parent, 1);

                spawnFloor("Floor", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, floorHeight, roomClass.roomDim.z / 2 * 100), 1, parent, 1);

                spawnFloor("Roof", DNAMath.determineCorner(roomClass.roomPos + new Vector3(0, 2.7f, 0), roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, floorHeight, roomClass.roomDim.z / 2 * 100), 1, parent, 3);

                //  spawnFloor("Roof", DNAMath.determineCorner(roomClass.roomPos + new Vector3(0, 2.7f, 0), roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, floorHeight, roomClass.roomDim.z / 2 * 100), 1, parent);
                if (roomClass.roomDim.x > 0.8f)
                {
                    float doorwayWidth = (((roomClass.roomDim.x / 2) - 0.4f) / 2) * 100;
                    spawnWall("Door 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 2), new Vector3(doorwayWidth, wallHeight, wallWidth), 2, parent, 1);
                    spawnWall("Door 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 3), new Vector3(doorwayWidth, wallHeight, wallWidth), 3, parent, 1);
                }
                else
                {
                    float doorwayWidth = ((roomClass.roomDim.x / 4) - (roomClass.roomDim.x / 8)) * 100;
                    spawnWall("Door 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 2), new Vector3(doorwayWidth, wallHeight, wallWidth), 2, parent, 1);
                    spawnWall("Door 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 3), new Vector3(doorwayWidth, wallHeight, wallWidth), 3, parent, 1);
                }

                break;
            case "Right":
                //  -->
                spawnWall("Wall 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(wallWidth, wallHeight, roomClass.roomDim.z / 2 * 100), 1, parent, 1);
                spawnWall("Wall 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 6), new Vector3((roomClass.roomDim.x / 2 * 100) - wallWidth * 2, wallHeight, wallWidth), 6, parent, 1);
                spawnWall("Wall 3", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 8), new Vector3((roomClass.roomDim.x / 2 * 100) - wallWidth * 2, wallHeight, wallWidth), 8, parent, 1);

                spawnFloor("Floor", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, floorHeight, roomClass.roomDim.z / 2 * 100), 1, parent, 1);

                spawnFloor("Roof", DNAMath.determineCorner(roomClass.roomPos + new Vector3(0, 2.7f, 0), roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, floorHeight, roomClass.roomDim.z / 2 * 100), 1, parent, 3);
                //  spawnFloor("Roof", DNAMath.determineCorner(roomClass.roomPos + new Vector3(0,2.7f, 0), roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, floorHeight, roomClass.roomDim.z / 2 * 100), 1, parent);

                if (roomClass.roomDim.x > 0.8f)
                {
                    float doorwayWidth = (((roomClass.roomDim.z / 2) - 0.4f) / 2) * 100;
                    spawnWall("Door 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 3), new Vector3(wallWidth, wallHeight, doorwayWidth), 3, parent, 1);
                    spawnWall("Door 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 4), new Vector3(wallWidth, wallHeight, doorwayWidth), 4, parent, 1);
                }
                else
                {
                    float doorwayWidth = ((roomClass.roomDim.z / 4) - (roomClass.roomDim.z / 8)) * 100;
                    spawnWall("Door 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 3), new Vector3(wallWidth, wallHeight, doorwayWidth), 3, parent, 1);
                    spawnWall("Door 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 4), new Vector3(wallWidth, wallHeight, doorwayWidth), 4, parent, 1);
                }

                break;
            case "Left":
                //  <--

                spawnWall("Wall 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 3), new Vector3(wallWidth, wallHeight, roomClass.roomDim.z / 2 * 100), 3, parent, 1);
                spawnWall("Wall 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 6), new Vector3((roomClass.roomDim.x / 2 * 100) - wallWidth * 2, wallHeight, wallWidth), 6, parent, 1);
                spawnWall("Wall 3", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 8), new Vector3((roomClass.roomDim.x / 2 * 100) - wallWidth * 2, wallHeight, wallWidth), 8, parent, 1);

                spawnFloor("Floor", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, floorHeight, roomClass.roomDim.z / 2 * 100), 1, parent, 1);

                spawnFloor("Roof", DNAMath.determineCorner(roomClass.roomPos + new Vector3(0, 2.7f, 0), roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, floorHeight, roomClass.roomDim.z / 2 * 100), 1, parent, 3);

                //  spawnFloor("Roof", DNAMath.determineCorner(roomClass.roomPos + new Vector3(0, 2.7f, 0), roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, floorHeight, roomClass.roomDim.z / 2 * 100), 1, parent);
                if (roomClass.roomDim.x > 0.8f)
                {
                    float doorwayWidth = (((roomClass.roomDim.z / 2) - 0.4f) / 2) * 100;
                    spawnWall("Door 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 2), new Vector3(wallWidth, wallHeight, doorwayWidth), 2, parent, 1);
                    spawnWall("Door 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(wallWidth, wallHeight, doorwayWidth), 1, parent, 1);
                }
                else
                {
                    float doorwayWidth = ((roomClass.roomDim.z / 4) - (roomClass.roomDim.z / 8)) * 100;
                    spawnWall("Door 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 2), new Vector3(wallWidth, wallHeight, doorwayWidth), 2, parent, 1);
                    spawnWall("Door 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(wallWidth, wallHeight, doorwayWidth), 1, parent, 1);
                }

                break;
            case "Down":
                spawnWall("Wall 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 2), new Vector3(roomClass.roomDim.x / 2 * 100, wallHeight, wallWidth), 2, parent, 1);
                spawnWall("Wall 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 5), new Vector3(wallWidth, wallHeight, (roomClass.roomDim.z / 2 * 100) - wallWidth * 2), 5, parent, 1);
                spawnWall("Wall 3", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 7), new Vector3(wallWidth, wallHeight, (roomClass.roomDim.z / 2 * 100) - wallWidth * 2), 7, parent, 1);

                spawnFloor("Floor", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, floorHeight, roomClass.roomDim.z / 2 * 100), 1, parent, 1);

                spawnFloor("Roof", DNAMath.determineCorner(roomClass.roomPos + new Vector3(0, 2.7f, 0), roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, floorHeight, roomClass.roomDim.z / 2 * 100), 1, parent, 3);
                //  spawnFloor("Roof", DNAMath.determineCorner(roomClass.roomPos + new Vector3(0, 2.7f, 0), roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, floorHeight, roomClass.roomDim.z / 2 * 100), 1, parent);
                if (roomClass.roomDim.x > 0.8f)
                {
                    float doorwayWidth = (((roomClass.roomDim.x / 2) - 0.4f) / 2) * 100;
                    spawnWall("Door 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(doorwayWidth, wallHeight, wallWidth), 1, parent, 1);
                    spawnWall("Door 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 4), new Vector3(doorwayWidth, wallHeight, wallWidth), 4, parent, 1);

                }
                else
                {
                    float doorwayWidth = ((roomClass.roomDim.x / 4) - (roomClass.roomDim.x / 8)) * 100;
                    spawnWall("Door 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(doorwayWidth, wallHeight, wallWidth), 1, parent, 1);
                    spawnWall("Door 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 4), new Vector3(doorwayWidth, wallHeight, wallWidth), 4, parent, 1);
                }

                break;
            default:

                break;
        }
    }


    void genHouse(HouseClass house, GameObject parent)
    {
        float wallWidth = 15;
        float wallHeight = 150;
        float floorHeight = 15;
        house.housePos = house.housePos - new Vector3(0.25f, 0, 0.25f);
        house.houseDim = house.houseDim + new Vector3(0.5f, 0, 0.5f);


        spawnFloor("Roof", DNAMath.determineCorner(Vector3.zero + new Vector3(0, 2.7f, 0) + (new Vector3(0, 3, 0) * (house.floorList.Count - 1)), house.houseDim, 9), new Vector3(house.houseDim.x / 2 * 100, floorHeight, house.houseDim.z / 2 * 100), 9, parent, 3);


        for (int i = 0; i < house.floorList.Count; i++)
        {
            if (i == 0)
            {
                //
                //Normally these numbers would be the same
                //                                                                        J                                                                  J
                //But for this we will switch them to trick the system into not clipping certain walls
                //
                // Debug.Log("Gen first floor");
                //Gen First floor with doorway
                spawnWall("Wall 1", DNAMath.determineCorner(Vector3.zero, house.houseDim, 2), new Vector3(house.houseDim.x / 2 * 100, wallHeight, wallWidth), 1, parent, 2);
                spawnWall("Wall 2", DNAMath.determineCorner(Vector3.zero, house.houseDim, 5), new Vector3(wallWidth, wallHeight, (house.houseDim.z / 2 * 100) + wallWidth * 2), 7, parent, 2);
                spawnWall("Wall 3", DNAMath.determineCorner(Vector3.zero, house.houseDim, 7), new Vector3(wallWidth, wallHeight, (house.houseDim.z / 2 * 100) + wallWidth * 2), 5, parent, 2);

                float doorwayWidth = (((house.houseDim.x / 2) - 0.8f) / 2) * 100;
                spawnWall("Door 1", DNAMath.determineCorner(Vector3.zero, house.houseDim, 1), new Vector3(doorwayWidth, wallHeight, wallWidth), 2, parent, 2);
                spawnWall("Door 2", DNAMath.determineCorner(Vector3.zero, house.houseDim, 4), new Vector3(doorwayWidth, wallHeight, wallWidth), 3, parent, 2);

            }
            else
            {
                //Gen room without doorway
                //Debug.Log("Gen Next floor");
                spawnWall("Wall 1", DNAMath.determineCorner(Vector3.zero + new Vector3(0, 3, 0) * i, house.houseDim, 2), new Vector3(house.houseDim.x / 2 * 100, wallHeight, wallWidth), 1, parent, 2);
                spawnWall("Wall 2", DNAMath.determineCorner(Vector3.zero + new Vector3(0, 3, 0) * i, house.houseDim, 5), new Vector3(wallWidth, wallHeight, (house.houseDim.z / 2 * 100) + wallWidth * 2), 7, parent, 2);
                spawnWall("Wall 3", DNAMath.determineCorner(Vector3.zero + new Vector3(0, 3, 0) * i, house.houseDim, 7), new Vector3(wallWidth, wallHeight, (house.houseDim.z / 2 * 100) + wallWidth * 2), 5, parent, 2);
                spawnWall("Wall 4", DNAMath.determineCorner(Vector3.zero + new Vector3(0, 3, 0) * i, house.houseDim, 1), new Vector3(house.houseDim.x / 2 * 100, wallHeight, wallWidth), 2, parent, 2);
            }
        }
    }

    void toggleInside()
    {
        if (inside)
        {
            //Go outside 

            cam.transform.position = outsidePos;
            cam.transform.rotation = Quaternion.Euler(outsideRot.x, outsideRot.y, outsideRot.z);

            inside = false;

            insideTogText.text = "Outside";

        }
        else
        {
            //Go inside
            inside = true;

            //Update Cam pos
            updateCamPos();

            insideTogText.text = "Inside";
        }
    }

    void updateCamPos()
    {
        //Debug.Log("Enter Cam Pos");
        if (inside)
        {
            // Debug.Log("here");
            // insidePos = house.floorList[floorDropDown.value].roomList[roomDropDown.value].roomPos;

            if (house.floorList[floorDropDown.value].roomList[roomDropDown.value].roomType == "Stairs")
            {
                insidePos = DNAMath.determineCorner(house.floorList[floorDropDown.value].roomList[roomDropDown.value].roomPos, house.floorList[floorDropDown.value].roomList[roomDropDown.value].roomPos + house.floorList[floorDropDown.value].roomList[roomDropDown.value].roomDim, 9) + new Vector3(0, 3f, 0) + house.housePos;
            }
            else
            {
                insidePos = DNAMath.determineCorner(house.floorList[floorDropDown.value].roomList[roomDropDown.value].roomPos, house.floorList[floorDropDown.value].roomList[roomDropDown.value].roomPos + house.floorList[floorDropDown.value].roomList[roomDropDown.value].roomDim, 9) + new Vector3(0, 1.5f, 0) + house.housePos + new Vector3(0, 3, 0) * floorDropDown.value;
            }

            cam.transform.position = insidePos;
            cam.transform.rotation = Quaternion.Euler(insideRot.x, insideRot.y, insideRot.z);
        }
    }

    void updateRot()
    {
        if (checkMobile())
        {
            //Mobile
            if (Input.touchCount > 0)
            {
                Vector2 touch = (Input.GetTouch(0).deltaPosition / 10) * -1;

                insideRot.y = touch.x;
                insideRot.x = touch.y;
            }
        }
        else
        {
            //Desktop
            if (mouseDown)
            {
                insideRot.y -= Input.GetAxisRaw("Mouse X") * Time.deltaTime * 240;
                insideRot.x += Input.GetAxisRaw("Mouse Y") * Time.deltaTime * 240;
            }
        }

        insideRot.x = Mathf.Clamp(insideRot.x, -70, 70);

        cam.transform.rotation = Quaternion.Euler(insideRot.x, insideRot.y, insideRot.z);

    }

    public static bool checkMobile()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                return true;
            case RuntimePlatform.IPhonePlayer:
                return true;
            case RuntimePlatform.BlackBerryPlayer:
                return true;
            default:
                return false;
        }
    }

    public void updateInputVals()
    {
        if (objPos == null || objPos == outOfBounds)
        {
            InX.text = "N/A";
            InY.text = "N/A";
            InZ.text = "N/A";
        }
        else
        {
            //Pos
            if (currentLayer == 11)
            {
                objPos.x = DNAMath.snapToUnit(objPos.x, 0.1f);
                objPos.y = DNAMath.snapToUnit(objPos.y, 0.1f);
                objPos.z = DNAMath.snapToUnit(objPos.z, 0.1f);
            }

            InX.text = (objPos.x - house.floorList[floorDropDown.value].roomList[roomDropDown.value].roomPos.x) + "m";
            InY.text = (objPos.y - house.floorList[floorDropDown.value].roomList[roomDropDown.value].roomPos.y) + "m";
            InZ.text = (objPos.z - house.floorList[floorDropDown.value].roomList[roomDropDown.value].roomPos.z) + "m";
        }

        updateCurObjPos();
    }


    public void InputX()
    {

        //Position
        float oldVal = objPos.x;
        float newVal;
        try
        {
            newVal = float.Parse(InX.text);
            objPos.x = newVal + house.floorList[floorDropDown.value].roomList[roomDropDown.value].roomPos.x;
        }
        catch
        {
            objPos.x = DNAMath.getMeasurement(InY.text, oldVal);
        }
        updateInputVals();
    }

    public void InputY()
    {
        float oldVal = objPos.y;
        float newVal;
        try
        {
            newVal = float.Parse(InY.text);


            objPos.y = newVal + house.floorList[floorDropDown.value].roomList[roomDropDown.value].roomPos.y;

        }
        catch
        {
            objPos.y = DNAMath.getMeasurement(InY.text, oldVal);
        }

        updateInputVals();
    }

    public void InputZ()
    {

        float oldVal = objPos.z;
        float newVal;
        try
        {
            newVal = float.Parse(InZ.text);
            objPos.z = newVal + house.floorList[floorDropDown.value].roomList[roomDropDown.value].roomPos.z;
        }
        catch
        {
            objPos.z = DNAMath.getMeasurement(InY.text, oldVal);
        }
        updateInputVals();
    }

    void PlusXBut()
    {
        objPos.x += OneUnit;
        updateInputVals();
    }

    void PlusYBut()
    {
        objPos.y += OneUnit;
        updateInputVals();
    }
    void PlusZBut()
    {
        objPos.z += OneUnit;
        updateInputVals();
    }
    void MinusXBut()
    {
        objPos.x -= OneUnit;
        updateInputVals();
    }
    void MinusYBut()
    {
        objPos.y -= OneUnit;
        updateInputVals();
    }
    void MinusZBut()
    {
        objPos.z -= OneUnit;
        updateInputVals();
    }

    Material getMaterial(int value)
    {
        switch (value)
        {

            case 1:
                validMat = true;
                return wallMat1;
            case 2:
                validMat = true;
                return wallMat2;
            case 3:
                validMat = true;
                return wallMat3;
            case 4:
                validMat = true;
                return wallMat4;
            case 5:
                validMat = true;
                return wallMat5;
            case 6:
                validMat = true;
                return floorMat2;
            case 7:
                validMat = true;
                return floorMat1;
            case 8:
                validMat = true;
                return exteriorWall;
            default:
                validMat = false;
                return wallMat1;
        }
    }

    GameObject getObject(int value)
    {
        switch (value)
        {
            case 1:
                validObject = true;
                return DecorationAssetList.chair(); ;
            case 2:
                validObject = true;
                return DecorationAssetList.plant();
            case 3:
                validObject = true;
                return DecorationAssetList.closet();
            case 4:
                validObject = true;
                return DecorationAssetList.sTable();
            case 5:
                validObject = true;
                return DecorationAssetList.desk();
            case 6:
                validObject = true;
                return DecorationAssetList.stand();
            case 7:
                validObject = true;
                return DecorationAssetList.sofa();
            case 8:
                validObject = true;
                return DecorationAssetList.carpet();
            default:
                validObject = false;
                return DecorationAssetList.chair();
        }
    }

    //
    //Tomorrow add the decoration system, and then it should be it
    //Add a system that makes sure 95% or x% of the floor is filled by area (subtract floor area)
    //


    void modifyToggle()
    {
        if (modify)
        {
            //Turn off 

            modifyTog.gameObject.GetComponent<Image>().color = Color.white;
            // updateMaterialDrop();
            // updateObjectDrop();

            modify = false;
        }
        else
        {
            //Turn on
            modifyTog.gameObject.GetComponent<Image>().color = Color.green;
            modify = true;
            currentLayer = 0;
        }
    }


    void applyMaterial()
    {
        //Debug.Log("Enter");
        //Check in update loop if there was a click
        if (!DNAMath.offScreen(0.75f, true, true))
        {
            //Check if modifiable and material is valid
            //  Debug.Log("On Screen");
            if (modify && validMat)
            {
                // Debug.Log("Modify and Valid Mat");
                Vector2 mousePos;
                if (checkMobile())
                {

                    //Mobile
                    mousePos = Input.GetTouch(0).position;
                }
                else
                {
                    //Desktop
                    mousePos = Input.mousePosition;
                }

                if (mousePos != null)
                {
                    //   Debug.Log("Valid Mouse Pos");
                    Ray ray = Camera.main.ScreenPointToRay(mousePos);
                    RaycastHit hit;


                    if (Physics.Raycast(ray, out hit, 20, 11))
                    {
                        if (hit.collider.gameObject != null)
                        {

                            currentObject = hit.collider.gameObject;
                            //Update Info
                            if (currentObject.GetComponent<MeshRenderer>() != null)
                            {
                                currentObject.GetComponent<MeshRenderer>().material = currentMat;
                            }

                        }
                    }
                }
            }
        }
    }

    void placeObject()
    {
        if (!DNAMath.offScreen(0.75f, true, true))
        {


            Vector2 mousePos;
            if (checkMobile())
            {

                //Mobile
                mousePos = Input.GetTouch(0).position;
            }
            else
            {
                //Desktop
                mousePos = Input.mousePosition;
            }

            if (mousePos != null)
            {
                // Debug.Log("Valid Mouse Pos");
                Ray ray = Camera.main.ScreenPointToRay(mousePos);
                RaycastHit hit;


                if (Physics.Raycast(ray, out hit, 20))
                {
                    if (hit.collider.gameObject != null)
                    {
                        if (modify)
                        {
                            if (validObject)
                            {
                                //Find the parent and parent the object to it

                                GameObject newObj = Instantiate(currentObject);

                                newObj.name = "New Item";
                                newObj.transform.position = hit.point;
                                newObj.AddComponent<BoxCollider>();
                                newObj.GetComponent<BoxCollider>().size = getMeshSize(objectSelectDropDown.value);
                                newObj.transform.parent = decoration.transform;

                                objPos = newObj.transform.position;

                                currentObject = newObj;

                                currentLayer = 11;

                                modifyToggle();

                                updateInputVals();
                            }
                        }
                        else
                        {

                            //Debug.Log("Hitting");

                            // Debug.Log(hit.collider.gameObject);

                            if (hit.collider.gameObject.layer == 11)
                            {
                                validDelete = true;
                                currentObject = hit.collider.gameObject;

                                objPos = currentObject.transform.position;
                                objRot = currentObject.transform.rotation.eulerAngles;

                                currentLayer = currentObject.layer;
                            }
                            else
                            {
                                validDelete = false;
                                currentObject = null;
                                currentLayer = 0;
                                objPos = outOfBounds;
                            }

                            updateInputVals();
                        }
                    }
                }
            }
        }
    }

    void updateCurObjPos()
    {
        if (currentObject == null)
        {
            objPos = outOfBounds;
        }
        else
        {
            currentObject.transform.position = objPos;
        }
    }

    void rotateObj()
    {
        if (currentObject != null)
        {
            switch (objRot.y)
            {
                case 0:
                    objRot.y = 90;
                    break;
                case 90:
                    objRot.y = 180;
                    break;
                case 180:
                    objRot.y = 270;
                    break;
                case 270:
                    objRot.y = 0;
                    break;
            }
        }
        currentObject.transform.rotation = Quaternion.Euler(objRot.x, objRot.y, objRot.z);
    }


    Vector3 getMeshSize(int value)
    {
        switch (value)
        {
            case 1:
                return new Vector3(0.5f, 2, 0.5f);
            case 2:
                return new Vector3(0.5f, 1, 0.5f);
            case 3:
                return new Vector3(1f, 4, 1f);
            case 4:
                return new Vector3(1f, 1.5f, 1f);
            case 5:
                return new Vector3(1, 2, 1);
            case 6:
                return new Vector3(1f, 1.5f, 1f);
            case 7:
                return new Vector3(0.5f, 1, 0.5f);
            case 8:
                return new Vector3(3f, 1, 3f);
            default:
                return new Vector3(3f, 1, 3f);
        }
    }

    void removeObject()
    {
        if (currentObject != null && validDelete)
        {
            Destroy(currentObject);

            updateObjectDrop();

        }
    }

    void completeBuild()
    {
        Debug.Log("Here");
        if (decoration.transform.childCount >= 10)
        {
            Debug.Log("Enter complete");
            //Complete the game
            //Make point panel, achievements and thank you message appear 
            //Background is the camera spinning around in every room

            //  resetCam();

            gameComplete = true;

            showPanel();

            roomPositions.Add(outsidePos);

            for (int i = 0; i < house.floorList.Count; i++)
            {
                for (int j = 0; j < house.floorList[i].roomList.Count; j++)
                {
                    //Check room type

                    if (house.floorList[i].roomList[j].roomType == "Stairs")
                    {
                        roomPositions.Add(DNAMath.determineCorner(house.floorList[i].roomList[j].roomPos, house.floorList[i].roomList[j].roomPos + house.floorList[i].roomList[j].roomDim, 9) + new Vector3(0, 3f, 0) + house.housePos + new Vector3(0, 3, 0) * i);
                    }
                    else
                    {
                        roomPositions.Add(DNAMath.determineCorner(house.floorList[i].roomList[j].roomPos, house.floorList[i].roomList[j].roomPos + house.floorList[i].roomList[j].roomDim, 9) + new Vector3(0, 1.5f, 0) + house.housePos + new Vector3(0, 3, 0) * i);
                    }

                }
            }

            resetCam();
        } else
        {
            Debug.Log("Trigger");
            //The requirements have not been met, complete the requirements
            DialogueTrigger idk = this.gameObject.transform.GetChild(0).GetComponent<DialogueTrigger>();

            idk.TriggerDialogue();
        }
    }

    void resetCam()
    {
        updateFinalCamPos(outsidePos);

        finalCam.transform.rotation = Quaternion.Euler(outsideRot.x, outsideRot.y, outsideRot.z);

        roomCount = 0;

    }

    void updateTimer()
    {

        if (roomCount == 0)
        {
            if (curTime >= firstTimer)
            {
                timerTick();
            }
        }
        else
        {
            updateFinalCamRot();

            if (curTime >= switchTime)
            {
                timerTick();
            }
        }

        curTime += Time.deltaTime;

    }

    void updateFinalCamPos(Vector3 pos)
    {
        finalCam.transform.position = pos;
        finalCam.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void updateFinalCamRot()
    {
        Vector3 rot = finalCam.transform.rotation.eulerAngles + new Vector3(0, (360f/FPS)/switchTime, 0);
        Quaternion newRot = Quaternion.Euler(rot.x, rot.y, rot.z);
        finalCam.transform.rotation = newRot;

    }

    void timerTick()
    {
        curTime = 0;

        roomCount++;

        if (roomCount > roomPositions.Count - 1)
        {
            roomCount = 0;
            resetCam();
        }
        else
        {
            updateFinalCamPos(roomPositions[roomCount]);
        }
    }

    void showPanel()
    {

        Debug.Log("panel");

        cam.gameObject.active = false;

        //Expand stream to full screen, hide all unecessary things, show a panel for score achievement etc. 
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        finalCam.gameObject.active = true;

        stream.sizeDelta = new Vector2(screenWidth, screenHeight);

        stream.gameObject.GetComponent<Image>().material = streamMat2;

        panel.sizeDelta = new Vector2(0, screenHeight);

        panel.gameObject.active = false;


        setFinalPanel();

        setFinalPanelInfo();

    }

    void setFinalPanel()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float panelWidth = screenWidth * 0.8f;
        float panelHeight = screenHeight * 0.8f;

        float everythingWidth = panelWidth * 0.9f;

        float completeTitleH = panelHeight * 0.1f;

        float achievementSecH = panelHeight * 0.25f;
        float pointsSecH = panelHeight * 0.45f;
        float ThankSecH = panelHeight * 0.20f;


        float textHeight = achievementSecH * 0.3f;
        // float achievementTitle = achievementSecH * 0.3f;
        float achievementImages = achievementSecH * 0.7f;

        float achievementSpacing = (everythingWidth - (achievementImages * 5)) / 4;

        AchievementImages.GetComponent<HorizontalLayoutGroup>().spacing = achievementSpacing;

        finalPanel.sizeDelta = new Vector3(panelWidth, panelHeight);


        Title.sizeDelta = new Vector2(everythingWidth, completeTitleH);
        Achievements.sizeDelta = new Vector2(everythingWidth, achievementSecH);
        Points.sizeDelta = new Vector2(everythingWidth, pointsSecH);
        ThankYou.sizeDelta = new Vector2(everythingWidth, ThankSecH);

        //Achievements

        AchievementTitle.sizeDelta = new Vector2(everythingWidth, textHeight);
        AchievementImages.sizeDelta = new Vector2(everythingWidth, achievementImages);

        Achievement1.sizeDelta = new Vector2(achievementImages, achievementImages);
        Achievement2.sizeDelta = new Vector2(achievementImages, achievementImages);
        Achievement3.sizeDelta = new Vector2(achievementImages, achievementImages);
        Achievement4.sizeDelta = new Vector2(achievementImages, achievementImages);
        Achievement5.sizeDelta = new Vector2(achievementImages, achievementImages);

        //Points

        float individualSecH = (pointsSecH - textHeight) * 0.6f;
        float totalSecH = (pointsSecH - textHeight) * 0.4f;


        float pointSecW = (everythingWidth * 0.9f) / 5;
        float pointSecSpace = (everythingWidth - pointSecW * 5) / 4;

        PointsTitle.sizeDelta = new Vector2(everythingWidth, textHeight);
        PointSection.sizeDelta = new Vector2(everythingWidth, individualSecH);
        PointTotal.sizeDelta = new Vector2(everythingWidth, totalSecH);

        PointSection.gameObject.GetComponent<HorizontalLayoutGroup>().spacing = pointSecSpace;

        Section1.sizeDelta = new Vector2(pointSecW, individualSecH);
        Section1Title.sizeDelta = new Vector2(pointSecW, individualSecH * 0.5f);
        Section1Points.sizeDelta = new Vector2(pointSecW, individualSecH * 0.5f);

        Section2.sizeDelta = new Vector2(pointSecW, individualSecH);
        Section2Title.sizeDelta = new Vector2(pointSecW, individualSecH * 0.5f);
        Section2Points.sizeDelta = new Vector2(pointSecW, individualSecH * 0.5f);

        Section3.sizeDelta = new Vector2(pointSecW, individualSecH);
        Section3Title.sizeDelta = new Vector2(pointSecW, individualSecH * 0.5f);
        Section3Points.sizeDelta = new Vector2(pointSecW, individualSecH * 0.5f);

        Section4.sizeDelta = new Vector2(pointSecW, individualSecH);
        Section4Title.sizeDelta = new Vector2(pointSecW, individualSecH * 0.5f);
        Section4Points.sizeDelta = new Vector2(pointSecW, individualSecH * 0.5f);

        Section5.sizeDelta = new Vector2(pointSecW, individualSecH);
        Section5Title.sizeDelta = new Vector2(pointSecW, individualSecH * 0.5f);
        Section5Points.sizeDelta = new Vector2(pointSecW, individualSecH * 0.5f);


        PointTotalTitle.sizeDelta = new Vector2(everythingWidth, totalSecH - textHeight);
        PointTotalpnt.sizeDelta = new Vector2(everythingWidth, textHeight);

        ThankYouSec.sizeDelta = new Vector2(everythingWidth, ThankSecH);
        ThankYouText.sizeDelta = new Vector2(everythingWidth * 0.8f, ThankSecH * 0.8f);

    }

    void setFinalPanelInfo()
    {
        float screenHeight = Screen.height;
        float panelHeight = screenHeight * 0.8f;
        float achievementSecH = panelHeight * 0.25f;
        float imageDim = achievementSecH * 0.7f;



        AchievementImage1 = Achievement1.gameObject.GetComponent<Image>();
        AchievementImage2 = Achievement2.gameObject.GetComponent<Image>();
        AchievementImage3 = Achievement3.gameObject.GetComponent<Image>();
        AchievementImage4 = Achievement4.gameObject.GetComponent<Image>();
        AchievementImage5 = Achievement5.gameObject.GetComponent<Image>();

        Texture2D imageAch1G = Resources.Load("Achievements/1") as Texture2D;
        Texture2D imageAch2G = Resources.Load("Achievements/2") as Texture2D;
        Texture2D imageAch3G = Resources.Load("Achievements/3") as Texture2D;
        Texture2D imageAch4G = Resources.Load("Achievements/4") as Texture2D;
        Texture2D imageAch5G = Resources.Load("Achievements/5") as Texture2D;

        Texture2D imageAch4B = Resources.Load("Achievements/4-1") as Texture2D;
        Texture2D imageAch5B = Resources.Load("Achievements/5-1") as Texture2D;


        //Achievement 1 - 3 is free

        // Achievement 4 unlocked if object number exceeds 60

        //Achievement 5 unlocked if object number exceeds 100

        AchievementImage1.sprite = Sprite.Create(imageAch1G, new Rect(0.0f, 0.0f, imageAch1G.width, imageAch1G.height), new Vector2(0.5f, 0.5f), 100.0f);
        AchievementImage2.sprite = Sprite.Create(imageAch2G, new Rect(0.0f, 0.0f, imageAch2G.width, imageAch2G.height), new Vector2(0.5f, 0.5f), 100.0f);
        AchievementImage3.sprite = Sprite.Create(imageAch3G, new Rect(0.0f, 0.0f, imageAch3G.width, imageAch3G.height), new Vector2(0.5f, 0.5f), 100.0f);

        int count = 0;
        for (int i = 0; i < house.floorList.Count; i++)
        {
            for (int j = 0; j < house.floorList[i].roomList.Count; j++)
            {
                for (int g = 0; g < house.floorList[i].roomList[j].objList.Count; g++)
                {
                    count++;
                }
            }
        }

        count = count + decoration.transform.childCount;

        Debug.Log(count);


        if (count >= 60)
        {
            AchievementImage4.sprite = Sprite.Create(imageAch4G, new Rect(0.0f, 0.0f, imageAch4G.width, imageAch4G.height), new Vector2(0.5f, 0.5f), 100.0f);
        }
        else
        {
            AchievementImage4.sprite = Sprite.Create(imageAch4B, new Rect(0.0f, 0.0f, imageAch4B.width, imageAch4B.height), new Vector2(0.5f, 0.5f), 100.0f);
        }

        if (count >= 100)
        {
            AchievementImage5.sprite = Sprite.Create(imageAch5G, new Rect(0.0f, 0.0f, imageAch5G.width, imageAch5G.height), new Vector2(0.5f, 0.5f), 100.0f);
        }
        else
        {
            AchievementImage5.sprite = Sprite.Create(imageAch5B, new Rect(0.0f, 0.0f, imageAch5B.width, imageAch5B.height), new Vector2(0.5f, 0.5f), 100.0f);
        }

        //Points Section
       


        //Floor Number
        floorNum = house.floorList.Count;

        //Room Number 
        roomNum = 0;
        for (int i = 0; i < house.floorList.Count; i++)
        {
            roomNum = roomNum + house.floorList[i].roomList.Count;
        }


        //Object Number
        objNum = 0;
        for (int i = 0; i < house.floorList.Count; i ++)
        {
            for (int j = 0; j < house.floorList[i].roomList.Count; j++)
            {
                objNum = objNum + house.floorList[i].roomList[j].objList.Count;
            }
        }
        objNum = objNum + 10;

        //Decoration
        decNum = decoration.transform.childCount;

        //Time
        timer.count = false;
        timeElap = timer.currentTime;

        timer.gameObject.active = false;


        //Return point values

        int floorMulti = 1000;
        int roomMulti = 500;
        int objMulti = 50;
        int decMulti = 100;
        int timeMulti = 100;
        

        floorPoints = floorNum * floorMulti;
        roomPoints = roomNum * roomMulti;
        objPoints = objNum * objMulti;
        decPoints = decNum * decMulti;

        //20 mins
        if (timeElap > 1200)
        {
            //Use function
            timePoints = DNAMath.timeExp(timeElap - 1200);
        } else
        {
            timePoints = 5000;
        }


       // timePoints = timeElap * timeMulti;

        totalPoints = floorPoints + roomPoints + objPoints + decPoints + timePoints;
       

        cleanSlate();

    }

    void pointController ()
    {
        
        if (pointSysStart)
        {
            string floorTextT = "Floor" + "\n"
            + "Number" + "\n";
            string floorTextP = "Points" + "\n";

            string roomTextT = "Room" + "\n"
           + "Number" + "\n";
            string roomTextP = "Points" + "\n";

            string objTextT = "Object" + "\n"
          + "Number" + "\n";
            string objTextP = "Points" + "\n";

            string decoTextT = "Decoration" + "\n"
          + "Number" + "\n";
            string decoTextP = "Points" + "\n";

            string timeTextT = "Time" + "\n"
          + "Elapsed" + "\n";
            string timeTextP = "Points" + "\n";

           // string TotalTextP = "Points" + "\n";

            //Remove later
           // int fac = 2;

            //Create classes
            floorTMan = new PointSystem(floorNum, PointSec1T, (3 * pointTimer) / 4, floorTextT);
            floorPMan = new PointSystem(floorPoints, PointSec1P, pointTimer , floorTextP);

            Debug.Log(roomNum);

            roomTMan = new PointSystem(roomNum, PointSec2T, (3 * pointTimer) / 4, roomTextT);
            roomPMan = new PointSystem(roomPoints, PointSec2P, pointTimer , roomTextP);

            objectTMan = new PointSystem(objNum, PointSec3T, (3 * pointTimer) / 4, objTextT);
            objectPMan = new PointSystem(objPoints, PointSec3P, pointTimer , objTextP);
            
            decoTMan = new PointSystem(decNum, PointSec4T, (3 * pointTimer) / 4, decoTextT);
            decoPMan = new PointSystem(decPoints, PointSec4P, pointTimer , decoTextP);

            TimeTMan = new PointSystem(timeElap, PointSec5T, (3*pointTimer) / 4, timeTextT);
            TimePMan = new PointSystem(timePoints, PointSec5P, pointTimer, timeTextP);

            TimeTMan.numberBase = false;

            TotalPointsMan = new PointSystem(totalPoints, PointFinalP, pointTimer, "");

            

            pointSysStart = false;
        }
       

        //Controller Classes should have a passthrough for (points, textfield thing, )


        switch (pointType)
        {
            case 0:
                //Floor
                floorTMan.updateDispPoints(curPointTimer);
                floorPMan.updateDispPoints(curPointTimer);

                nextPointType();

                if (floorPMan.done)
                {
                    curPointTimer = 0;
                    pointType++;
                }

                break;
            case 1:
                //Room
                Debug.Log("RoomTitle");
                roomTMan.updateDispPoints(curPointTimer);
                roomPMan.updateDispPoints(curPointTimer);

                if (roomPMan.done)
                {
                    curPointTimer = 0;
                    pointType++;
                }

                nextPointType();
                // Debug.Log("Next Thing");
                break;
            case 2:
                //Obj Num
                objectTMan.updateDispPoints(curPointTimer);
                objectPMan.updateDispPoints(curPointTimer);

                if (objectPMan.done)
                {
                    curPointTimer = 0;
                    pointType++;
                }

                nextPointType();
               
                break;
            case 3:
                //Decoration Num
                decoTMan.updateDispPoints(curPointTimer);
                decoPMan.updateDispPoints(curPointTimer);

                if (decoPMan.done)
                {
                    curPointTimer = 0;
                    pointType++;
                }

                nextPointType();
               
                break;
            case 4:
                //Time

                TimeTMan.updateDispPoints(curPointTimer);
                TimePMan.updateDispPoints(curPointTimer);

                if (TimePMan.done)
                {
                    curPointTimer = 0;
                    pointType++;
                }

                nextPointType();
                break;
            case 5:
                //Total points 
                TotalPointsMan.updateDispPoints(curPointTimer);
               

                if (TotalPointsMan.done)
                {
                    curPointTimer = 0;
                    pointType++;
                }

                nextPointType();

                break;
            case 6:
                //Reset loop?
                if (Input.GetKeyDown(KeyCode.Mouse0) || Input.touchCount > 0)
                {
                    cleanSlate();
                    pointSysStart = true;
                    pointType = 0;
                }
              
                break;
        }
        curPointTimer = curPointTimer + (1f / FPS);

        //Debug.Log((1f / 60f));
        //Debug.Log(Time.deltaTime);

    }

    void cleanSlate ()
    {
        //Set Default Text

        PointSec1T = Section1Title.gameObject.GetComponent<Text>();
        PointSec1P = Section1Points.gameObject.GetComponent<Text>();

        PointSec2T = Section2Title.gameObject.GetComponent<Text>();
        PointSec2P = Section2Points.gameObject.GetComponent<Text>();

        PointSec3T = Section3Title.gameObject.GetComponent<Text>();
        PointSec3P = Section3Points.gameObject.GetComponent<Text>();

        PointSec4T = Section4Title.gameObject.GetComponent<Text>();
        PointSec4P = Section4Points.gameObject.GetComponent<Text>();

        PointSec5T = Section5Title.gameObject.GetComponent<Text>();
        PointSec5P = Section5Points.gameObject.GetComponent<Text>();

        PointFinalT = PointTotalTitle.gameObject.GetComponent<Text>();
        PointFinalP = PointTotalpnt.gameObject.GetComponent<Text>();


        //Floor
        PointSec1T.text = "Floor" + "\n"
            + "Number" + "\n"
            + "0"
            ;

        PointSec1P.text = "Points" + "\n"
           + "0"
           ;

        //Room
        PointSec2T.text = "Room" + "\n"
            + "Number" + "\n"
            + "0"
            ;

        PointSec2P.text = "Points" + "\n"
           + "0"
           ;

        //Object
        PointSec3T.text = "Object" + "\n"
            + "Number" + "\n"
            + "0"
            ;

        PointSec3P.text = "Points" + "\n"
           + "0"
           ;

        //Decoration
        PointSec4T.text = "Decoration" + "\n"
            + "Number" + "\n"
            + "0"
            ;

        PointSec4P.text = "Points" + "\n"
           + "0"
           ;

        //Time
        PointSec5T.text = "Time" + "\n"
            + "Elapsed" + "\n"
            + "0"
            ;

        PointSec5P.text = "Points" + "\n"
           + "0"
           ;

        //Time
        PointFinalT.text = "Total Points";

        PointFinalP.text = "0";
    }

    void nextPointType ()
    {
        if (curPointTimer >= pointTimer)
        {
            curPointTimer = 0;
            pointType++;
        }
    }






}
