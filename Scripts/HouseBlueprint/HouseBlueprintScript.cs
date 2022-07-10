using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SaveLoadSystem;
//using UnityEngine.CoreModule;

public class HouseBlueprintScript : MonoBehaviour
{
    [Header("Parents")]
    [SerializeField] RectTransform Stream;
    [SerializeField] RectTransform panelUI;
    [SerializeField] RectTransform zoomRect;

    [Header("LayoutGroups")]
    [SerializeField] RectTransform floorRoomBtns;
    [SerializeField] RectTransform controllerBtns;
    [SerializeField] RectTransform description;

    [Header("Floor And Room Btns")]
    [SerializeField] float floorBtnFac;
    [SerializeField] RectTransform floorController;
    [SerializeField] RectTransform roomController;
    [SerializeField] RectTransform roomTypeController;
    [SerializeField] RectTransform minusFloor;
    [SerializeField] RectTransform floorDrop;
    [SerializeField] RectTransform plusFloor;
    [SerializeField] RectTransform minusRoom;
    [SerializeField] RectTransform roomDrop;
    [SerializeField] RectTransform plusRoom;
    //[SerializeField] RectTransform rotateLeft;
    [SerializeField] RectTransform roomTypeDrop;
    [SerializeField] RectTransform roomDirDrop;
    //[SerializeField] RectTransform rotateRight;

    [SerializeField] RectTransform dimPosTog;

    [Header("Controller Btns")]
    [SerializeField] float contBtnFac;
    [SerializeField] RectTransform modifyTitle;
    [SerializeField] RectTransform XTitle;
    [SerializeField] RectTransform XController;
    [SerializeField] RectTransform YTitle;
    [SerializeField] RectTransform YController;
    [SerializeField] RectTransform XMinus;
    [SerializeField] RectTransform XSize;
    [SerializeField] RectTransform XPlus;
    [SerializeField] RectTransform YMinus;
    [SerializeField] RectTransform YSize;
    [SerializeField] RectTransform YPlus;

    [Header("Description")]
    [SerializeField] float descFac;
    [SerializeField] RectTransform descTitle;
    [SerializeField] RectTransform requirements;
    [SerializeField] RectTransform wireFrameTog;
    [SerializeField] RectTransform Save;

    [Header("Other")]
    [SerializeField] Texture streamTexture;
    // [SerializeField] GameObject HouseParent;
    [SerializeField] GameObject cursor;
    [SerializeField] Camera cam;
    [SerializeField] GameObject houseOutline;
    [SerializeField] GameObject House;
    [SerializeField] GameObject House3D;

    Slider zoomSlider;

    Dropdown floorDropDown;
    Dropdown roomDropDown;
    Dropdown roomTypeDropDown;
    Dropdown roomDirDropDown;

    Text floorDropDownText;
    Text roomDropDownText;
    Text roomTypeDropDownText;
    Text roomDirDropDownText;


    Button floorPlus;
    Button floorMinus;
    Button roomPlus;
    Button roomMinus;
    // Button rotateRoomLeft;
    //  Button rotateRoomRight;

    Button DimPosTogBtn;
    Text DimPosTogText;

    Button XMinusBtn;
    Button XPlusBtn;
    Button YMinusBtn;
    Button YPlusBtn;

    InputField InX;
    InputField InY;
    Text InXText;
    Text InYText;

    Text InXTitle;
    Text InYTitle;

    Text requirementText;
    Text wireFrameTogText;

    Button wireFrameTogBtn;
    Button SaveButton;


    //GameObject currentFloor;
    //GameObject currentRoom;

    Transform currentFloorTrans;
    Transform currentRoomTrans;

    //Actual Variables

    Vector3 cursorHide = new Vector3(1000, 1000, 1000);

    Vector3 perspViewPos = new Vector3(9.93f, 10, -30);
    // Vector3 skyView = new Vector3(9.94f, 10, 2.244f);
    Vector3 skyView;

    Vector3 CamVec = new Vector3(0, 0, 1);

    Vector3 mouseInit;
    Vector3 mousePos;

    Vector3 aboveGround = Vector3.zero;


    Vector3 initCorner;

    Vector3 maxDim = new Vector3(40, 0, 40);
    Vector3 origin = new Vector3(0, 0, 0);

    Vector3 roomDim;

    Vector3 globalCorner1;
    Vector3 globalCorner2;

    Vector3 nullVec;


    Vector3 abovePlatform = new Vector3(0, 1, 0);
    // bool perspView;

    bool setRoom;

    bool setDimensions;

    bool createNewRoom;

    bool nextPage;

    bool mouseDown;
    bool mouseClick;

    bool drawing;

    bool DimOrPos = true; //true = Dim , false = Pos

    float offsetX;
    float offsetY;

    //10 cm, will have to multiply on this screen probably to meet to the design requirements
    float OneUnit = 0.1f;


    float houseWidthReq;
    float houseLengthReq;

    float globalUnit = 0.1f;

    int floorNum;
    int roomNum;

    List<FloorClass> floorClasses;

    HouseClass house;

    bool GoodtoDraw = false;

    bool wireFrameMode; //True = regular lines, false = actual 3D Models

    GameObject roomObject;

    List<List<Vector3>> floorReq = new List<List<Vector3>>();

    // Start is called before the first frame update

    void createReq()
    {
        Vector3 r1 = new Vector3(7, 0, 15);
        Vector3 r2 = new Vector3(5, 0, 5);
        Vector3 r3 = new Vector3(8, 0, 11);
        Vector3 r4 = new Vector3(6, 0, 8);

        Vector3 r5 = new Vector3(9, 0, 12);
        Vector3 r6 = new Vector3(7, 0, 8);
        Vector3 r7 = new Vector3(6, 0, 9);

        // Vector3 r4 = new Vector3(1, 0, 1);
        // Vector3 r5 = new Vector3(5, 0, 2);
        // Vector3 r6 = new Vector3(7, 0, 7);


        List<Vector3> floor1 = new List<Vector3>();
        floor1.Add(r1);
        floor1.Add(r2);
        floor1.Add(r3);
        floor1.Add(r4);

        List<Vector3> floor2 = new List<Vector3>();
        floor2.Add(r5);
        floor2.Add(r6);
        floor2.Add(r7);


        // List<Vector3> floor2 = new List<Vector3>();
        // floor2.Add(r4);
        // floor2.Add(r5);
        // floor2.Add(r6);

        floorReq.Add(floor1);
        floorReq.Add(floor2);

        //2 Floors


        string req = "Floor 1 : " + "\n"
            + "Room 1 : " + "Width: " + r1.x + "m  Length: " + r1.z + "m" + "\n"
            + "Room 2 : " + "Width: " + r2.x + "m  Length: " + r2.z + "m" + "\n"
            + "Room 3 : " + "Width: " + r3.x + "m  Length: " + r3.z + "m" + "\n"
             + "Room 4 : " + "Width: " + r4.x + "m  Length: " + r4.z + "m" + "\n"
            + "Floor 2 : " + "\n"
              + "Room 1 : " + "Width: " + r5.x + "m  Length: " + r5.z + "m" + "\n"
              + "Room 2 : " + "Width: " + r6.x + "m  Length: " + r6.z + "m" + "\n"
              + "Room 3 : " + "Width: " + r7.x + "m  Length: " + r7.z + "m" + "\n"
              + "95% of floor area filled";

        requirementText.text = req;

    }

    void Start()
    {
        loadInfo();

        //industructable();

        floorClasses = new List<FloorClass>();
        Debug.Log("New page");
        Debug.Log(house.houseDim);
        Debug.Log(origin);

        FloorClass floor1 = new FloorClass(0, origin, house.houseDim);


        Debug.Log(floor1.floorDim);

        floor1.createNewRoom(0, origin, origin + new Vector3(1, 0, 1) * 2);

        floorClasses.Add(floor1);

        floorNum = floorClasses.Count;

        setUI();
        getButtons();

        createReq();

        addBtnListeners();

        maxDim = house.houseDim;

        DrawSquare(origin + abovePlatform, maxDim + abovePlatform, houseOutline, Color.red);

        skyView = (origin + maxDim / 2) + new Vector3(0, 10, 0);

        updateCamera(skyView);

        updateFloorDrop();

        DialogueTrigger idk = this.gameObject.GetComponent<DialogueTrigger>();

        idk.TriggerDialogue();

    }

    // Update is called once per frame
    void Update()
    {

        mouseControls();

        //listenControls();

        mouseTrack();

        if (setDimensions)
        {
            cursor.transform.position = getMousePos(globalUnit);
        }

    }

    void loadInfo()
    {
        SaveSystemManager.Load();
        house = SaveSystemManager.LoadLevel(1);
        house = SaveSystemManager.CurrentSaveData.currentHouse;
    }


    void industructable()
    {
        // while (Indestructable.instance == null)
        //{
        //  Indestructable.instance.prevScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        // }
    }

    void setUI()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float streamWidth = Screen.width * 0.75f;
        float panelWidth = Screen.width * 0.25f;

        float horPad = panelUI.gameObject.GetComponent<VerticalLayoutGroup>().padding.horizontal;
        float vertPad = panelUI.gameObject.GetComponent<VerticalLayoutGroup>().padding.vertical;
        float spacing = panelUI.gameObject.GetComponent<VerticalLayoutGroup>().spacing * 2;

        float panelHeight = screenHeight - (vertPad + spacing);

        float everythingWidth = panelWidth - horPad;

        float floorAndRoom = panelHeight * floorBtnFac;
        float controller = panelHeight * contBtnFac;
        float desc = panelHeight * descFac;

        float BtnHeight = floorAndRoom / 4;
        float btnDim = BtnHeight;

        float textHeight = (controller - (BtnHeight * 2)) / 3;

        float requriementTextHeight = desc - (BtnHeight * 2 + textHeight);

        streamTexture.width = (int)Mathf.Floor(streamWidth);
        streamTexture.height = (int)Mathf.Floor(screenHeight);

        Stream.sizeDelta = new Vector2(streamWidth, screenHeight);
        panelUI.sizeDelta = new Vector2(panelWidth, screenHeight);
        zoomRect.sizeDelta = new Vector2(streamWidth * 0.95f, screenHeight * 0.05f);

        floorRoomBtns.sizeDelta = new Vector2(everythingWidth, floorAndRoom);
        controllerBtns.sizeDelta = new Vector2(everythingWidth, controller);
        description.sizeDelta = new Vector2(everythingWidth, desc);

        floorController.sizeDelta = new Vector2(everythingWidth, BtnHeight);
        roomController.sizeDelta = new Vector2(everythingWidth, BtnHeight);
        roomTypeController.sizeDelta = new Vector2(everythingWidth, BtnHeight);
        dimPosTog.sizeDelta = new Vector2(everythingWidth, BtnHeight);

        minusFloor.sizeDelta = new Vector2(btnDim, btnDim);
        plusFloor.sizeDelta = new Vector2(btnDim, btnDim);
        floorDrop.sizeDelta = new Vector2(everythingWidth - (btnDim * 2), BtnHeight);

        minusRoom.sizeDelta = new Vector2(btnDim, btnDim);
        plusRoom.sizeDelta = new Vector2(btnDim, btnDim);
        roomDrop.sizeDelta = new Vector2(everythingWidth - (btnDim * 2), BtnHeight);

        roomDirDrop.sizeDelta = new Vector2(everythingWidth / 2, BtnHeight);
        roomTypeDrop.sizeDelta = new Vector2(everythingWidth / 2, BtnHeight);

        modifyTitle.sizeDelta = new Vector2(everythingWidth, textHeight);
        XTitle.sizeDelta = new Vector2(everythingWidth, textHeight);
        YTitle.sizeDelta = new Vector2(everythingWidth, textHeight);
        XController.sizeDelta = new Vector2(everythingWidth, BtnHeight);
        YController.sizeDelta = new Vector2(everythingWidth, BtnHeight);

        descTitle.sizeDelta = new Vector2(everythingWidth, textHeight);
        wireFrameTog.sizeDelta = new Vector2(everythingWidth, BtnHeight);
        Save.sizeDelta = new Vector2(everythingWidth, BtnHeight);
        requirements.sizeDelta = new Vector2(everythingWidth, requriementTextHeight);
    }

    void getButtons()
    {
        zoomSlider = zoomRect.gameObject.GetComponent<Slider>();

        floorDropDown = floorDrop.gameObject.GetComponent<Dropdown>();
        roomDropDown = roomDrop.gameObject.GetComponent<Dropdown>();
        roomTypeDropDown = roomTypeDrop.gameObject.GetComponent<Dropdown>();
        roomDirDropDown = roomDirDrop.gameObject.GetComponent<Dropdown>();

        floorDropDownText = floorDrop.gameObject.transform.Find("Label").GetComponent<Text>();
        roomDropDownText = roomDrop.gameObject.transform.Find("Label").GetComponent<Text>();
        roomTypeDropDownText = roomTypeDrop.gameObject.transform.Find("Label").GetComponent<Text>();
        roomDirDropDownText = roomDirDrop.gameObject.transform.Find("Label").GetComponent<Text>();

        floorPlus = plusFloor.gameObject.GetComponent<Button>();
        floorMinus = minusFloor.gameObject.GetComponent<Button>();
        roomPlus = plusRoom.gameObject.GetComponent<Button>();
        roomMinus = minusRoom.gameObject.GetComponent<Button>();

        DimPosTogBtn = dimPosTog.gameObject.GetComponent<Button>();
        DimPosTogText = dimPosTog.transform.Find("DimPosTogText").GetComponent<Text>();

        XPlusBtn = XPlus.gameObject.GetComponent<Button>();
        YPlusBtn = YPlus.gameObject.GetComponent<Button>();
        XMinusBtn = XMinus.gameObject.GetComponent<Button>();
        YMinusBtn = YMinus.gameObject.GetComponent<Button>();

        InX = XSize.gameObject.GetComponent<InputField>();
        InY = YSize.gameObject.GetComponent<InputField>();
        InXText = XSize.gameObject.GetComponent<Text>();
        InYText = YSize.gameObject.GetComponent<Text>();

        InXTitle = XTitle.gameObject.GetComponent<Text>();
        InYTitle = YTitle.gameObject.GetComponent<Text>();

        requirementText = requirements.gameObject.GetComponent<Text>();
        wireFrameTogText = wireFrameTog.gameObject.transform.Find("WireFrameTogText").GetComponent<Text>();

        wireFrameTogBtn = wireFrameTog.gameObject.GetComponent<Button>();
        SaveButton = Save.gameObject.GetComponent<Button>();
    }

    void addBtnListeners()
    {

        floorPlus.onClick.AddListener(addFloor);
        floorMinus.onClick.AddListener(removeFloor);
        roomPlus.onClick.AddListener(addRoom);
        roomMinus.onClick.AddListener(removeRoom);

        DimPosTogBtn.onClick.AddListener(dimOrPosToggle);

        XPlusBtn.onClick.AddListener(PlusXBut);
        YPlusBtn.onClick.AddListener(PlusYBut);
        XMinusBtn.onClick.AddListener(MinusXBut);
        YMinusBtn.onClick.AddListener(MinusYBut);

        InX.onEndEdit.AddListener(delegate { InputX(); });
        InY.onEndEdit.AddListener(delegate { InputY(); });

        wireFrameTogBtn.onClick.AddListener(toggleWireFrame);
        SaveButton.onClick.AddListener(saveBut);
    }

    public void PlusXBut()
    {
        updateTransforms();
        if (DimOrPos)
        {
            //Dim
            // Debug.Log("New Turn PDX");
            if (checkIfRoomPossible(initCorner, new Vector3(roomDim.x + OneUnit, roomDim.y, roomDim.z), maxDim, true))
            {
                roomDim.x += OneUnit;
            }
        }
        else
        {
            //Pos
            // Debug.Log("New Turn PPX");
            if (checkIfRoomPossible(new Vector3(initCorner.x + OneUnit, initCorner.y, initCorner.z), roomDim, maxDim, false))
            {
                initCorner.x += OneUnit;
            }
        }

        updateInputVals();
    }

    public void PlusYBut()
    {
        updateTransforms();
        if (DimOrPos)
        {
            //Dim
            // Debug.Log("New Turn PDY");
            if (checkIfRoomPossible(initCorner, new Vector3(roomDim.x, roomDim.y, roomDim.z + OneUnit), maxDim, true))
            {
                roomDim.z += OneUnit;
            }
        }
        else
        {
            //Pos
            // Debug.Log("New Turn PPY");
            if (checkIfRoomPossible(new Vector3(initCorner.x, initCorner.y, initCorner.z + OneUnit), roomDim, maxDim, false))
            {
                initCorner.z += OneUnit;
            }
        }

        updateInputVals();
    }

    public void MinusXBut()
    {
        updateTransforms();
        if (DimOrPos)
        {
            //Dim
            // Debug.Log("New Turn MDX");
            if (roomDim.x - OneUnit >= 0 && checkIfRoomPossible(initCorner, new Vector3(roomDim.x - OneUnit, roomDim.y, roomDim.z), maxDim, true))
            {
                roomDim.x -= OneUnit;
            }
        }
        else
        {
            //Pos
            //  Debug.Log("New Turn MPX");
            if ((initCorner.x - origin.x) - OneUnit >= 0 && checkIfRoomPossible(new Vector3(initCorner.x - OneUnit, initCorner.y, initCorner.z), roomDim, maxDim, false))
            {
                initCorner.x -= OneUnit;
            }
        }
        updateInputVals();
    }
    public void MinusYBut()
    {
        updateTransforms();
        if (DimOrPos)
        {
            //Dim
            //Debug.Log("New Turn MDY");
            if (roomDim.z - OneUnit >= 0 && checkIfRoomPossible(initCorner, new Vector3(roomDim.x, roomDim.y, roomDim.z - OneUnit), maxDim, true))
            {
                roomDim.z -= OneUnit;
            }
        }
        else
        {
            //Pos
            //Debug.Log("New Turn MPY");
            if ((initCorner.z - origin.z) - OneUnit >= 0 && checkIfRoomPossible(new Vector3(initCorner.x, initCorner.y, initCorner.z - OneUnit), roomDim, maxDim, false))
            {
                initCorner.z -= OneUnit;
            }
        }
        updateInputVals();
    }

    public void InputX()
    {
        updateTransforms();
        if (DimOrPos)
        {
            float oldVal = roomDim.x;
            float newVal;
            try
            {
                newVal = float.Parse(InX.text);
                if (checkIfRoomPossible(initCorner, new Vector3(newVal, roomDim.y, roomDim.z), maxDim, true))
                {
                    roomDim.x = newVal;
                }
                else
                {
                    roomDim.x = oldVal;
                }
                updateInputVals();
            }
            catch
            {
                roomDim.x = DNAMath.getMeasurement(InX.text, oldVal);

                updateInputVals();
            }
        }
        else
        {
            //Position
            float oldVal = initCorner.x;
            float newVal;
            try
            {
                newVal = float.Parse(InX.text);

                if (checkIfRoomPossible(new Vector3(origin.x + newVal, initCorner.y, initCorner.z), roomDim, maxDim, false))
                {
                    initCorner.x = newVal + origin.x;
                }
                else
                {
                    initCorner.x = oldVal;
                }
                updateInputVals();
            }
            catch
            {

                initCorner.x = DNAMath.getMeasurement(InX.text, oldVal);

                if (initCorner.x == oldVal)
                {

                }
                else
                {
                    initCorner.x = initCorner.x + origin.x;
                }


                updateInputVals();
            }
        }
    }

    public void InputY()
    {
        updateTransforms();
        if (DimOrPos)
        {
            float oldVal = roomDim.z;
            float newVal;
            try
            {
                newVal = float.Parse(InY.text);

                if (checkIfRoomPossible(initCorner, new Vector3(roomDim.x, roomDim.y, newVal), maxDim, true))
                {
                    roomDim.z = newVal;
                }
                else
                {
                    roomDim.z = oldVal;
                }
            }
            catch
            {
                roomDim.z = DNAMath.getMeasurement(InY.text, oldVal);
            }
        }
        else
        {
            float oldVal = initCorner.z;
            float newVal;
            try
            {
                newVal = float.Parse(InY.text);

                if (checkIfRoomPossible(new Vector3(initCorner.x, initCorner.y, origin.z + newVal), roomDim, maxDim, false))
                {
                    initCorner.z = newVal + origin.z;
                }
                else
                {
                    initCorner.z = oldVal;
                }
            }
            catch
            {
                initCorner.z = DNAMath.getMeasurement(InY.text, oldVal);

                if (initCorner.z == oldVal)
                {

                }
                else
                {
                    initCorner.z = initCorner.z + origin.z;
                }
            }
        }
        updateInputVals();
    }

    public void updateInputVals()
    {

        // Debug.Log("Update Input Val");
        updateTransforms();
        if (DimOrPos)
        {
            DimPosTogText.text = "Dimension";

            InXTitle.text = "Width";
            InYTitle.text = "Length";

            //Update the values
            if (roomDim == null)
            {
                InX.text = "N/A";
                InY.text = "N/A";
            }
            else
            {
                //Clean the values 
                roomDim.x = DNAMath.snapToUnit(roomDim.x, 0.1f);
                roomDim.z = DNAMath.snapToUnit(roomDim.z, 0.1f);

                InX.text = roomDim.x.ToString() + "m";
                InY.text = roomDim.z.ToString() + "m";
            }
        }
        else
        {
            DimPosTogText.text = "Position";

            InXTitle.text = "Position X";
            InYTitle.text = "Position Y";

            if (initCorner == null)
            {
                InX.text = "N/A";
                InY.text = "N/A";
            }
            else
            {
                //Pos
                initCorner.x = DNAMath.snapToUnit(initCorner.x, 0.1f);
                initCorner.z = DNAMath.snapToUnit(initCorner.z, 0.1f);

                InX.text = (initCorner.x - origin.x) + "m";
                InY.text = (initCorner.z - origin.z) + "m";
            }
        }

        if (roomDropDown.options.Count > 0)
        {
            //Update Square
            foreach (Transform child in currentRoomTrans.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            drawRoomType(initCorner + abovePlatform, DNAMath.makeCorner(initCorner, roomDim) + abovePlatform, currentRoomTrans.gameObject, Color.green, floorClasses[floorDropDown.value].roomList[roomDropDown.value].roomType, floorClasses[floorDropDown.value].roomList[roomDropDown.value].roomDirVec);
        }
        updateRoomInfo();
    }

    public void dimOrPosToggle()
    {

        if (DimOrPos)
        {
            //Go to Pos
            DimOrPos = false;
        }
        else
        {
            //Go to Dim
            DimOrPos = true;
        }

        updateInputVals();
    }

    public void DrawLine(Vector3 start, Vector3 end, Color color, GameObject parent)
    {
        GameObject line = new GameObject("ThinLine");
        line.transform.localPosition = start;
        line.AddComponent<LineRenderer>();
        line.transform.parent = parent.transform;
        LineRenderer lr = line.GetComponent<LineRenderer>();
        lr.material.color = color;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

    }

    public void DrawSquare(Vector3 start, Vector3 end, GameObject parent, Color color)
    {
        //Corner 2                   Corner 3



        //Corner 1                   Corner 4

        Vector3 Corner1 = start;
        Vector3 Corner2 = start;
        Corner2.z = end.z;
        Vector3 Corner3 = end;
        Vector3 Corner4 = end;
        Corner4.z = Corner1.z;

        DrawLine(Corner1, Corner2, color, parent);
        DrawLine(Corner2, Corner3, color, parent);
        DrawLine(Corner3, Corner4, color, parent);
        DrawLine(Corner4, Corner1, color, parent);


    }


    void saveBut()
    {
        Debug.Log("Saving");
        if (checkRequirements())
        {

            if (checkAreaRequirement())
            {
                HouseClass newHouse = new HouseClass(SaveSystemManager.CurrentSaveData.currentHouse.housePos, SaveSystemManager.CurrentSaveData.currentHouse.houseDim);

                newHouse.createBlueprint(floorClasses);

                SaveSystemManager.CurrentSaveData.currentHouse = newHouse;

                SaveSystemManager.Save();

                UnityEngine.SceneManagement.SceneManager.LoadScene(4);
            }
            else
            {
                Debug.Log("95% not met");
                DialogueTrigger idk = this.gameObject.transform.GetChild(0).GetChild(1).GetComponent<DialogueTrigger>();

                idk.TriggerDialogue();
            }

        }
        else
        {
            //Add another section for calculating the area and a message box for that one too
            //The requirements have not been met, complete the requirements


            DialogueTrigger idk = this.gameObject.transform.GetChild(0).GetComponent<DialogueTrigger>();

            idk.TriggerDialogue();

        }
    }

    bool checkRequirements()
    {
        //Check if right number of floors
        if (floorReq.Count == floorClasses.Count)
        {
            for (int i = 0; i < floorClasses.Count; i++)
            {

                //  if (floorReq[i].Count == floorClasses[i].roomList.Count)
                //{

                //Look if any all required rooms are present

                for (int j = 0; j < floorReq[i].Count; j++)
                {
                    bool verdict = false;
                    for (int g = 0; g < floorClasses[i].roomList.Count; g++)
                    {
                        if (floorReq[i][j] == floorClasses[i].roomList[g].roomDim)
                        {
                            verdict = true;
                        }
                    }
                    if (!verdict)
                    {
                        //None of the rooms had the right dimension
                        return false;
                    }
                }
                // }
                // else
                // {
                //    return false;
                // }
            }

            return true;
        }
        else
        {
            return false;
        }



    }

    bool roomContained(List<Vector3> reqs, List<RoomClass> rooms)
    {
        bool verdict = false;
        for (int i = 0; i < rooms.Count; i++)
        {
            //  if ()



        }



        return true;
    }

    public void mouseControls()
    {
        //Debug.Log(Input.mousePosition);

        if (DNAMath.offScreen(0.75f, true, true) || DNAMath.offScreen(0.1f, false, false))
        {
            //Debug.Log("OffScreen");
            updateZoom();
        }
        else
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                mouseDown = true;
            }
            else
            {
                mouseDown = false;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                mouseClick = true;
            }
            else
            {
                mouseClick = false;
            }
        }
    }

    public void mouseTrack()
    {

        //bool goodToDraw = false;
        if (setDimensions)
        {
            updateTransforms();
            if (mouseClick)
            {

                if (checkIfRoomPossible(getMousePos(globalUnit) + aboveGround, Vector3.one, maxDim, true))
                {
                    mouseInit = getMousePos(globalUnit) + aboveGround;

                    //Change these to room object

                    currentRoomTrans.transform.position = mouseInit;

                    drawing = true;

                    GoodtoDraw = true;
                }
                else
                {
                    GoodtoDraw = false;
                }
            }

            if (mouseDown && GoodtoDraw)
            {
                updateTransforms();

                mousePos = getMousePos(globalUnit);

                foreach (Transform child in currentRoomTrans.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }

                if (checkIfRoomPossible(DNAMath.determineCorner(mouseInit, mousePos, 1), new Vector3(DNAMath.houseSize(mouseInit, mousePos).x, 0, DNAMath.houseSize(mouseInit, mousePos).z), maxDim, true))
                {
                    initCorner = DNAMath.determineCorner(mouseInit, mousePos, 1);

                    roomDim = new Vector3(DNAMath.houseSize(mouseInit, mousePos).x, 0, DNAMath.houseSize(mouseInit, mousePos).z);
                }
                else
                {

                }

                drawRoomType(initCorner + abovePlatform, DNAMath.makeCorner(initCorner, roomDim) + abovePlatform, currentRoomTrans.gameObject, Color.green, floorClasses[floorDropDown.value].roomList[roomDropDown.value].roomType, floorClasses[floorDropDown.value].roomList[roomDropDown.value].roomDirVec);

                updateInputVals();

                drawing = true;

            }
            else
            {
                if (drawing)
                {
                    drawing = false;
                    setDimensions = false;

                    roomPlus.GetComponent<Image>().color = Color.white;
                }
            }
        }
        else
        {
            //Move Camera
            if (mouseDown)
            {
                updateCamera(skyView);
            }
        }
    }

    public Vector3 getMousePos(float unit)
    {
        Vector3 pos = Vector3.one;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            if (checkInPlay(raycastHit.point, origin, maxDim))
            {
                pos = raycastHit.point;

            }
            else
            {
                pos = cursor.transform.position;
            }
        }
        else
        {
            pos = cursor.transform.position;
        }
        pos.y = 0.1f;
        return new Vector3(DNAMath.snapToUnit(pos.x, unit), pos.y, DNAMath.snapToUnit(pos.z, unit));

    }

    public void updateCamera(Vector3 topDown)
    {
        //  debug.text = "platform";
        if (checkMobile())
        {
            if (Input.touchCount > 0)
            {

                Vector2 touch = (Input.GetTouch(0).deltaPosition / 10) * -1;

                offsetX += touch.x;
                offsetY += touch.y;
            }


        }
        else
        {

            offsetX -= Input.GetAxisRaw("Mouse X") * Time.deltaTime * 120;
            offsetY -= Input.GetAxisRaw("Mouse Y") * Time.deltaTime * 120;
        }

        //  Debug.Log(zoomSlider);



        updateZoom();

        //Change this clamp system



        offsetX = Mathf.Clamp(offsetX, -5 * zoomSlider.value, 5 * zoomSlider.value);
        offsetY = Mathf.Clamp(offsetY, -5 * zoomSlider.value, 5 * zoomSlider.value);

        cam.transform.position = new Vector3(Mathf.Clamp(topDown.x + offsetX, 0, maxDim.x), topDown.y, Mathf.Clamp(topDown.z + offsetY, 0, maxDim.z));
        //  }

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

    public static bool checkInPlay(Vector3 pos, Vector3 corner1, Vector3 corner2)
    {
        if (pos.x > corner1.x && pos.x < corner2.x)
        {
            if (pos.z > corner1.z && pos.z < corner2.z)
            {
                return true;
            }
        }
        return false;
    }

    void updateFloorDrop()
    {
        // Debug.Log("Update Floor Drop");
        int lastFloor = floorDropDown.value;

        floorDropDown.ClearOptions();

        //  debug.text = "cleared";

        for (int i = 0; i < floorClasses.Count; i++)
        {
            //Replace the number with i probably?
            floorDropDown.options.Add(new Dropdown.OptionData() { text = "Floor " + (i + 1) });
        }

        floorNum = floorDropDown.options.Count;

        floorDropDown.onValueChanged.RemoveAllListeners();

        // debug.text = "cleared 2";

        floorDropDown.onValueChanged.AddListener(delegate
        {
            dropDownItemSelectedFloor(floorDropDown);
            updateRoomDrop();
        });

        //Change this to last floor
        if (lastFloor < floorClasses.Count)
        {
            floorDropDown.value = lastFloor;
        }
        else
        {
            floorDropDown.value = 0;
        }

        dropDownItemSelectedFloor(floorDropDown);

        updateRoomDrop();
    }

    void updateRoomDrop()
    {
        //Debug.Log("Update Room Drop enter");
        //Room Drop down
        if (floorClasses[floorDropDown.value].roomList.Count == 0)
        {
            roomDropDown.ClearOptions();
            roomDropDownText.text = "No Rooms";
            roomDropDown.interactable = false;
            roomNum = roomDropDown.options.Count;

            roomTypeDropDown.ClearOptions();
            roomTypeDropDownText.text = "Type N/A";
            roomTypeDropDown.interactable = false;

            roomDirDropDown.ClearOptions();
            roomDirDropDownText.text = "Type N/A";
            roomDirDropDown.interactable = false;
        }
        else
        {
            roomDropDown.interactable = true;
            roomDropDown.ClearOptions();

            for (int i = 0; i < floorClasses[floorDropDown.value].roomList.Count; i++)
            {
                roomDropDown.options.Add(new Dropdown.OptionData() { text = "Room " + (i + 1) });
            }

            roomNum = roomDropDown.options.Count;

            roomDropDown.onValueChanged.RemoveAllListeners();

            roomDropDown.onValueChanged.AddListener(delegate
            {
                dropDownItemSelectedRoom(roomDropDown);
                updateTransforms();
                updateEditVals();

                dropDownItemSelectedRoomType(roomTypeDropDown);
                updateRoomType();
                updateRoomDirVector();


                updateHouseStruct();

            });

            if (setDimensions)
            {
                roomDropDown.value = roomDropDown.options.Count - 1;
            }
            else
            {
                roomDropDown.value = 0;
            }

            dropDownItemSelectedRoom(roomDropDown);

            updateRoomType();

            updateRoomDirVector();

        }

        updateHouseStruct();
        // Debug.Log("Transform Enter");
        updateTransforms();

        updateEditVals();
    }



    void updateRoomType()
    {
        // Debug.Log("Update Room Type Drop");

        //
        //Room type dropdown
        //

        roomTypeDropDown.interactable = true;
        roomTypeDropDown.ClearOptions();

        roomTypeDropDown.options.Add(new Dropdown.OptionData() { text = "Room" });
        roomTypeDropDown.options.Add(new Dropdown.OptionData() { text = "Hallway" });
        roomTypeDropDown.options.Add(new Dropdown.OptionData() { text = "Stairs" });

        roomTypeDropDown.onValueChanged.RemoveAllListeners();

        roomTypeDropDown.onValueChanged.AddListener(delegate
        {
            dropDownItemSelectedRoomType(roomTypeDropDown);
            // updateRoomType();

            floorClasses[floorDropDown.value].roomList[roomDropDown.value].roomType = roomTypeDropDown.options[roomTypeDropDown.value].text;

            updateHouseStruct();

        });

        if (roomDropDown.options.Count > 0)
        {
            if (floorClasses[floorDropDown.value].roomList[roomDropDown.value].roomType == null)
            {
                //  Debug.Log("Drop here");
                roomTypeDropDown.value = 0;

                floorClasses[floorDropDown.value].roomList[roomDropDown.value].roomType = roomTypeDropDown.options[roomTypeDropDown.value].text;

            }
            else
            {
                //  Debug.Log("Drop here 2");
                switch (floorClasses[floorDropDown.value].roomList[roomDropDown.value].roomType)
                {
                    case "Room":
                        roomTypeDropDown.value = 0;
                        break;
                    case "Hallway":
                        roomTypeDropDown.value = 1;
                        break;
                    case "Stairs":
                        roomTypeDropDown.value = 2;
                        break;
                }

            }
            dropDownItemSelectedRoomType(roomTypeDropDown);
        }

    }

    void updateRoomDirVector()
    {
        // Debug.Log("Update Dir Vec Drop");
        roomDirDropDown.interactable = true;
        roomDirDropDown.ClearOptions();

        roomDirDropDown.options.Add(new Dropdown.OptionData() { text = "Up" });
        roomDirDropDown.options.Add(new Dropdown.OptionData() { text = "Right" });
        roomDirDropDown.options.Add(new Dropdown.OptionData() { text = "Left" });
        roomDirDropDown.options.Add(new Dropdown.OptionData() { text = "Down" });

        roomDirDropDown.onValueChanged.RemoveAllListeners();

        roomDirDropDown.onValueChanged.AddListener(delegate
        {
            dropDownItemSelectedRoomDirVec(roomDirDropDown);

            floorClasses[floorDropDown.value].roomList[roomDropDown.value].roomDirVec = roomDirDropDown.options[roomDirDropDown.value].text;

            updateHouseStruct();
        });

        // Debug.Log(floorClasses[floorDropDown.value].roomList[roomDropDown.value].roomDirVec);

        if (roomDropDown.options.Count > 0)
        {
            if (floorClasses[floorDropDown.value].roomList[roomDropDown.value].roomDirVec == null)
            {
                // Debug.Log("Give a dir");
                roomDirDropDown.value = 0;
                floorClasses[floorDropDown.value].roomList[roomDropDown.value].roomDirVec = roomDirDropDown.options[roomDirDropDown.value].text;
            }
            else
            {
                switch (floorClasses[floorDropDown.value].roomList[roomDropDown.value].roomDirVec)
                {
                    case "Up":
                        roomDirDropDown.value = 0;
                        break;
                    case "Right":
                        roomDirDropDown.value = 1;
                        break;
                    case "Left":
                        roomDirDropDown.value = 2;
                        break;
                    case "Down":
                        roomDirDropDown.value = 3;
                        break;
                }
            }
            dropDownItemSelectedRoomDirVec(roomDirDropDown);
        }
    }

    void dropDownItemSelectedRoomDirVec(Dropdown dropdown)
    {
        roomDirDropDownText.text = dropdown.options[dropdown.value].text;
    }

    void dropDownItemSelectedRoomType(Dropdown dropdown)
    {
        roomTypeDropDownText.text = dropdown.options[dropdown.value].text;
    }

    void dropDownItemSelectedFloor(Dropdown dropdown)
    {
        floorDropDownText.text = dropdown.options[dropdown.value].text;

    }

    void dropDownItemSelectedRoom(Dropdown dropdown)
    {
        roomDropDownText.text = dropdown.options[dropdown.value].text;
    }

    void addFloor()
    {
        FloorClass newFloor = new FloorClass(floorNum, origin, maxDim);

        GameObject floor = new GameObject();
        floor.name = "Floor " + floorNum;
        floor.transform.parent = House.transform;

        //floorObjects.Add(floor);

        floorClasses.Add(newFloor);

        updateFloorDrop();
        floorNum = floorDropDown.options.Count;
    }

    void removeFloor()
    {
        floorClasses.RemoveAt(floorDropDown.value);
        updateFloorDrop();

    }

    void addRoom()
    {
        if (setDimensions)
        {
            //turn off
            roomPlus.GetComponent<Image>().color = Color.white;
            setDimensions = false;

        }
        else
        {
            updateTransforms();
            //turn on 
            roomPlus.GetComponent<Image>().color = Color.green;
            setDimensions = true;

            RoomClass roomClass = new RoomClass(roomNum, floorNum, Vector3.zero, Vector3.zero);
            roomClass.roomDirVec = "Up";
            roomClass.roomType = "Room";


            GameObject room = new GameObject();
            room.name = "Room " + roomNum;
            room.transform.parent = currentFloorTrans.transform;
            room.transform.position = Vector3.zero;

            currentRoomTrans = currentFloorTrans.transform.GetChild(currentFloorTrans.transform.childCount - 1);

            floorClasses[floorDropDown.value].roomList.Add(roomClass);


            // Debug.Log(floorClasses[floorDropDown.value].roomList.Count);
            roomNum++;

            // Debug.Log("Update Room Drop");
            updateRoomDrop();
        }
    }

    void updateHouseStruct()
    {
        //Debug.Log("Updating House");
        foreach (Transform child in House.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < floorClasses.Count + 1; i++)
        {

            if (floorDropDown.value == i)
            {
                GameObject floor = new GameObject();
                floor.name = "Floor " + i;
                floor.transform.parent = House.transform;
                floor.transform.localPosition = Vector3.zero;

                for (int j = 0; j < floorClasses[i].roomList.Count; j++)
                {
                    GameObject room = new GameObject();
                    room.name = "Room " + j;
                    room.transform.parent = floor.transform;
                    room.transform.localPosition = floorClasses[i].roomList[j].roomPos;

                    //Debug.Log(floorClasses[i].roomList[j].roomDim);

                    if (roomDropDown.value == j)
                    {
                        drawRoomType(floorClasses[i].roomList[j].roomPos + abovePlatform, floorClasses[i].roomList[j].roomPos + floorClasses[i].roomList[j].roomDim + abovePlatform, room, Color.green, floorClasses[i].roomList[j].roomType, floorClasses[i].roomList[j].roomDirVec);
                    }
                    else
                    {
                        drawRoomType(floorClasses[i].roomList[j].roomPos + abovePlatform, floorClasses[i].roomList[j].roomPos + floorClasses[i].roomList[j].roomDim + abovePlatform, room, Color.red, floorClasses[i].roomList[j].roomType, floorClasses[i].roomList[j].roomDirVec);
                    }
                }
            }
        }
    }

    void removeRoom()
    {

        floorClasses[floorDropDown.value].roomList.RemoveAt(roomDropDown.value);

        updateRoomDrop();

    }

    void updateTransforms()
    {
        try
        {
            if (House.transform.childCount > 0)
            {
                // Debug.Log("Getting floor");
                //If your going to change something about the system this is important
                currentFloorTrans = House.transform.GetChild(0);

                if (currentFloorTrans.transform.childCount > 0)
                {
                    //Debug.Log("Getting room");
                    // Debug.Log(roomDropDown.value);

                    currentRoomTrans = currentFloorTrans.transform.GetChild(roomDropDown.value);
                    roomObject = currentRoomTrans.gameObject;
                }
            }
        }
        catch
        {
            Debug.Log("Error");
        }
    }

    void updateRoomInfo()
    {
        if (floorClasses[floorDropDown.value].roomList.Count > 0)
        {
            floorClasses[floorDropDown.value].roomList[roomDropDown.value].roomPos = initCorner;
            floorClasses[floorDropDown.value].roomList[roomDropDown.value].roomDim = roomDim;
        }
    }

    bool checkIfRoomPossible(Vector3 pos, Vector3 dim, Vector3 houseDim, bool Dim)
    {
        //Need to shorten the checkPossible

        //Check if all corners are not inside another rooms region
        bool verdict = true;


        if (Dim)
        {
            float widthLeft = houseDim.x - pos.x;
            float heightLeft = houseDim.z - pos.z;
            if (widthLeft >= dim.x)
            {
                if (heightLeft >= dim.z)
                {
                    verdict = true;
                }
                else
                {
                    // Debug.Log("Not Possible 1");
                    return false;
                }
            }
            else
            {
                //  Debug.Log("Not Possible 2");
                return false;
            }
        }
        else
        {
            if (houseDim.x >= DNAMath.makeCorner(pos, dim).x)
            {
                if (houseDim.z >= DNAMath.makeCorner(pos, dim).z)
                {
                    verdict = true;
                }
                else
                {
                    // Debug.Log("Not Possible 3");
                    return false;
                }
            }
            else
            {
                //  Debug.Log("Not Possible 4");
                return false;
            }
        }

        //Check if any of the rooms corners are inside another room

        for (int i = 0; i < floorClasses[floorDropDown.value].roomList.Count; i++)
        {
            if (roomDropDown.value == i)
            {
                //Skip if looking at the same
            }
            else
            {
                if (insideRoom(pos, dim, floorClasses[floorDropDown.value].roomList[i]))
                {
                    return false;
                }
            }
        }

        return verdict;
    }

    bool insideRoom(Vector3 pos, Vector3 dim, RoomClass otherRoom)
    {
        // Debug.Log("Room pos: " + pos);
        // Debug.Log("Room Dim: " + dim);

        Vector3 corner1 = DNAMath.determineCorner(otherRoom.roomPos, otherRoom.roomPos + otherRoom.roomDim, 1);
        Vector3 corner2 = DNAMath.determineCorner(otherRoom.roomPos, otherRoom.roomPos + otherRoom.roomDim, 2);
        Vector3 corner3 = DNAMath.determineCorner(otherRoom.roomPos, otherRoom.roomPos + otherRoom.roomDim, 3);
        Vector3 corner4 = DNAMath.determineCorner(otherRoom.roomPos, otherRoom.roomPos + otherRoom.roomDim, 4);

        globalCorner1 = corner1;
        globalCorner2 = corner3;

        //Check if inside other room
        for (int i = 1; i <= 4; i++)
        {
            Vector3 corner = DNAMath.determineCorner(pos, pos + dim, i);

            if (corner.x > corner1.x && corner.x < corner3.x && corner.z > corner1.z && corner.z < corner3.z) ///Maybe remove = 
            {
                //Inside the room 
                // Debug.Log("Corner inside room");
                return true;
            }
        }

        //Check if other room is inside the room being edited
        Vector3 editRoomCorner1 = DNAMath.determineCorner(pos, pos + dim, 1);
        Vector3 editRoomCorner3 = DNAMath.determineCorner(pos, pos + dim, 3);

        for (int i = 1; i <= 4; i++)
        {
            Vector3 corner = DNAMath.determineCorner(otherRoom.roomPos, otherRoom.roomPos + otherRoom.roomDim, i);

            if (corner.x > editRoomCorner1.x && corner.x < editRoomCorner3.x && corner.z > editRoomCorner1.z && corner.z < editRoomCorner3.z) ///Maybe remove = 
            {
                //Inside the room 
                // Debug.Log("Other Corner inside room");
                return true;
            }
        }


        //Get perpendicular diagonal intersection

        if (DNAMath.intersectVector(pos, dim, corner2, corner4))
        {
            // Debug.Log("New Solution");
            return true;

        }

        return false;
    }

    void updateEditVals()
    {
        // Debug.Log("Update Edit");

        if (roomDropDown.options.Count > 0)
        {
            initCorner = floorClasses[floorDropDown.value].roomList[roomDropDown.value].roomPos;
            roomDim = floorClasses[floorDropDown.value].roomList[roomDropDown.value].roomDim;
        }
        else
        {
            initCorner = nullVec;
            roomDim = nullVec;
        }

        // Debug.Log("About to Input Val");
        updateInputVals();
    }


    bool checkBetweenCorners(Vector2 origin, Vector2 dir, float s, Vector3 corner1, Vector3 corner2)
    {
        Vector2 newCorner1 = new Vector2(corner1.x, corner1.z);
        Vector2 newCorner2 = new Vector2(corner2.x, corner2.z);

        Vector2 point = origin + (dir * s);

        if (point == newCorner1 || point == newCorner2)
        {
            return false;
        }

        if ((point.x > globalCorner1.x && point.x < globalCorner2.x) && (point.y > globalCorner1.z && point.y < globalCorner2.z))
        {
            //Inside the square
            return true;
        }
        else
        {
            return false;
        }

    }


    void drawRoomType(Vector3 start, Vector3 end, GameObject parent, Color color, string roomType, string roomDir)
    {
        Vector3 Corner1 = start;
        Vector3 Corner2 = start;
        Corner2.z = end.z;
        Vector3 Corner3 = end;
        Vector3 Corner4 = end;
        Corner4.z = Corner1.z;

        Vector3 centerPos = getCenterOfRoom(Corner1, Corner3);

        switch (roomType)
        {
            case "Room":

                switch (roomDir)
                {
                    case "Up":
                        //Doorway is orientation sensitive
                        //Walls
                        DrawLine(Corner1, Corner2, color, parent);
                        DrawLine(Corner3, Corner4, color, parent);
                        DrawLine(Corner4, Corner1, color, parent);
                        //Doorway
                        drawDoorWay(Corner2, Corner3, color, parent);
                        break;
                    case "Right":
                        //Walls
                        DrawLine(Corner1, Corner2, color, parent);
                        DrawLine(Corner2, Corner3, color, parent);
                        DrawLine(Corner4, Corner1, color, parent);
                        //Doorway
                        drawDoorWay(Corner4, Corner3, color, parent);
                        break;
                    case "Left":
                        //Walls
                        DrawLine(Corner2, Corner3, color, parent);
                        DrawLine(Corner3, Corner4, color, parent);
                        DrawLine(Corner4, Corner1, color, parent);
                        //Doorway
                        drawDoorWay(Corner1, Corner2, color, parent);
                        break;
                    case "Down":
                        //Walls
                        DrawLine(Corner2, Corner3, color, parent);
                        DrawLine(Corner3, Corner4, color, parent);
                        DrawLine(Corner1, Corner2, color, parent);
                        //Doorway
                        drawDoorWay(Corner1, Corner4, color, parent);
                        break;
                }
                break;
            case "Hallway":
                //Walls
                DrawLine(Corner1, Corner2, color, parent);
                DrawLine(Corner2, Corner3, color, parent);
                DrawLine(Corner4, Corner3, color, parent);
                DrawLine(Corner1, Corner4, color, parent);
                //X Shape
                DrawLine(Corner1, Corner3, color, parent);
                DrawLine(Corner2, Corner4, color, parent);
                break;
            case "Stairs":

                //Walls
                DrawLine(Corner1, Corner2, color, parent);
                DrawLine(Corner2, Corner3, color, parent);
                DrawLine(Corner4, Corner3, color, parent);
                DrawLine(Corner1, Corner4, color, parent);

                switch (roomDir)
                {
                    case "Up":
                        drawStairCase(Corner1, Corner2, Corner4, color, parent);
                        break;
                    case "Right":
                        drawStairCase(Corner2, Corner3, Corner1, color, parent);
                        break;
                    case "Left":
                        drawStairCase(Corner4, Corner1, Corner3, color, parent);
                        break;
                    case "Down":
                        drawStairCase(Corner2, Corner1, Corner3, color, parent);
                        break;
                }

                break;
        }

        drawArrow(centerPos, roomDir, parent);

    }


    void drawStairCase(Vector3 start, Vector3 end, Vector3 width, Color color, GameObject parent)
    {
        //Draw 8 or 10 steps 

        Vector3 stepDirVec = (end - start) / 10;

        for (int i = 0; i <= 10; i++)
        {
            DrawLine(start + stepDirVec * i, width + stepDirVec * i, color, parent);
        }


    }

    void drawDoorWay(Vector3 start, Vector3 end, Color color, GameObject parent)
    {
        //All doors will be 0.8 meters wide and 3 meters tall

        //  Debug.Log("Start " + start);
        //  Debug.Log("End " + end);

        Vector3 width = end - start;
        float wallWidth;
        wallWidth = Mathf.Max(Mathf.Abs(width.x), Mathf.Abs(width.z));
        Vector3 halfPoint = width / 2;
        //  Vector3 guessVec = new Vector3(halfPoint.x - 0.25f, halfPoint.y, halfPoint.z);

        if (wallWidth > 0.8f)
        {
            //  Debug.Log("More than 0.8m");
            if (width.x == wallWidth)
            {
                //   Debug.Log("X Axis");
                //On the right axis
                DrawLine(start, start + new Vector3(halfPoint.x - 0.4f, 0, halfPoint.z), color, parent);

                DrawLine(start + new Vector3(halfPoint.x + 0.4f, 0, halfPoint.z), end, color, parent);
            }
            else
            {
                //  Debug.Log("Z Axis");
                //Go on Z axis
                DrawLine(start, start + new Vector3(halfPoint.x, 0, halfPoint.z - 0.4f), color, parent);

                DrawLine(start + new Vector3(halfPoint.x, 0, halfPoint.z + 0.4f), end, color, parent);
            }
        }
        else
        {
            // Debug.Log("Not More than 0.8m");
            //We will use a door that is 80 or 90 % the width of the wall 
            if (width.x == wallWidth)
            {
                //   Debug.Log("X Axis");
                //On the right axis
                DrawLine(start, start + new Vector3(halfPoint.x - halfPoint.x * 0.5f, 0, halfPoint.z), color, parent);

                DrawLine(start + new Vector3(halfPoint.x + halfPoint.x * 0.5f, 0, halfPoint.z), end, color, parent);
            }
            else
            {
                //  Debug.Log("Z Axis");
                //Go on Z axis
                DrawLine(start, start + new Vector3(halfPoint.x, 0, halfPoint.z - halfPoint.x * 0.5f), color, parent);

                DrawLine(start + new Vector3(halfPoint.x, 0, halfPoint.z + halfPoint.x * 0.5f), end, color, parent);
            }
        }
    }

    void drawArrow(Vector3 pos, string dir, GameObject parent)
    {
        GameObject arrow = new GameObject();
        arrow.name = "Arrow";
        arrow.transform.position = pos;
        arrow.transform.parent = parent.transform;

        pos = pos + new Vector3(0, 0.5f, 0);

        Vector3 bottom = Vector3.zero;
        Vector3 top = Vector3.zero;
        Vector3 right = Vector3.zero;
        Vector3 left = Vector3.zero;
        Vector3 tip = Vector3.zero;

        switch (dir)
        {
            case "Up":
                //   ^
                //   |
                bottom = new Vector3(pos.x, pos.y, pos.z - 0.3f);
                top = new Vector3(pos.x, pos.y, pos.z + 0.3f);
                right = new Vector3(pos.x + 0.3f, pos.y, pos.z + 0.3f);
                left = new Vector3(pos.x - 0.3f, pos.y, pos.z + 0.3f);
                tip = new Vector3(pos.x, pos.y, pos.z + 0.6f);

                break;
            case "Right":
                //  -->
                bottom = new Vector3(pos.x - 0.3f, pos.y, pos.z);
                top = new Vector3(pos.x + 0.3f, pos.y, pos.z);
                right = new Vector3(pos.x + 0.3f, pos.y, pos.z - 0.3f);
                left = new Vector3(pos.x + 0.3f, pos.y, pos.z + 0.3f);
                tip = new Vector3(pos.x + 0.6f, pos.y, pos.z);

                break;
            case "Left":
                //  <--
                bottom = new Vector3(pos.x + 0.3f, pos.y, pos.z);
                top = new Vector3(pos.x - 0.3f, pos.y, pos.z);
                right = new Vector3(pos.x - 0.3f, pos.y, pos.z - 0.3f);
                left = new Vector3(pos.x - 0.3f, pos.y, pos.z + 0.3f);
                tip = new Vector3(pos.x - 0.6f, pos.y, pos.z);
                break;
            case "Down":
                bottom = new Vector3(pos.x, pos.y, pos.z + 0.3f);
                top = new Vector3(pos.x, pos.y, pos.z - 0.3f);
                right = new Vector3(pos.x + 0.3f, pos.y, pos.z - 0.3f);
                left = new Vector3(pos.x - 0.3f, pos.y, pos.z - 0.3f);
                tip = new Vector3(pos.x, pos.y, pos.z - 0.6f);

                break;
            default:

                bottom = new Vector3(pos.x, pos.y, pos.z - 0.3f);
                top = new Vector3(pos.x, pos.y, pos.z + 0.3f);
                right = new Vector3(pos.x + 0.3f, pos.y, pos.z + 0.3f);
                left = new Vector3(pos.x - 0.3f, pos.y, pos.z + 0.3f);
                tip = new Vector3(pos.x, pos.y, pos.z + 0.6f);
                break;
        }

        //Frame
        DrawLine(bottom, top, Color.magenta, arrow);
        DrawLine(left, right, Color.magenta, arrow);
        DrawLine(left, tip, Color.magenta, arrow);
        DrawLine(right, tip, Color.magenta, arrow);

    }

    Vector3 getCenterOfRoom(Vector3 corner1, Vector3 corner3)
    {
        Vector3 cross = (corner3 - corner1) / 2;
        return corner1 + cross;
    }

    void updateZoom()
    {
        cam.orthographicSize = 5 * (zoomSlider.maxValue / zoomSlider.value);
    }

    void toggleWireFrame()
    {

        Debug.Log("TogWire");
        if (wireFrameMode)
        {
            //Go to 3D models
            wireFrameMode = false;

            foreach (Transform child in House3D.transform)
            {
                GameObject.Destroy(child.gameObject);

                wireFrameTogText.text = "Wireframe";
            }

        }
        else
        {
            wireFrameTogText.text = "3D";
            Debug.Log("Draw Wall");
            //Come back to wireframe
            wireFrameMode = true;
            updateHouseStruct();

            update3DObjects();
            //Debug.Log(newWall);

        }

    }

    void update3DObjects()
    {
        updateTransforms();

        foreach (Transform child in House3D.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < floorClasses[floorDropDown.value].roomList.Count; i++)
        {
            GameObject room = new GameObject("Room " + i);
            room.transform.position = Vector3.zero;
            room.transform.SetParent(House3D.transform);
            switch (floorClasses[floorDropDown.value].roomList[i].roomType)
            {
                case "Room":
                    genRoom(floorClasses[floorDropDown.value].roomList[i], room);
                    break;
                case "Hallway":
                    genHallway(floorClasses[floorDropDown.value].roomList[i], room);
                    break;
                case "Stairs":
                    genStairs(floorClasses[floorDropDown.value].roomList[i], room);
                    break;
            }
        }
    }

    public static void spawnWall(string name, Vector3 pos, Vector3 dim, int pointNum, GameObject parent)
    {
        GameObject wall = new GameObject();

        wall = GameObject.Instantiate(Resources.Load("Models/floor_dec_tile")) as GameObject;

        wall.name = name;
        wall.transform.SetParent(parent.transform);
        wall.transform.localScale = dim;
        wall.transform.localPosition = DNAMath.alignToPoint(pos, dim, pointNum);
        wall.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        wall.GetComponent<MeshRenderer>().material.color = Color.green;

    }

    public static void spawnFloor(string name, Vector3 pos, Vector3 dim, int pointNum, GameObject parent)
    {
        GameObject floor = new GameObject();

        floor = GameObject.Instantiate(Resources.Load("Models/floor_dec_tile")) as GameObject;
        floor.name = name;
        floor.transform.SetParent(parent.transform);
        floor.transform.localScale = dim;
        floor.transform.localPosition = DNAMath.alignToPoint(pos, dim, pointNum);
        floor.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        floor.GetComponent<MeshRenderer>().material.color = Color.blue;

    }

    public static void spawnStairs(string name, Vector3 pos, Vector3 dim, float spinAngle, int pointNum, GameObject parent, bool horizontal)
    {
        GameObject stairs = new GameObject();

        stairs = GameObject.Instantiate(Resources.Load("Models/stairs")) as GameObject;

        stairs.name = name;
        stairs.transform.SetParent(parent.transform);
        stairs.transform.localScale = dim;

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

        stairs.GetComponent<MeshRenderer>().material.color = Color.magenta;


        Debug.Log(DNAMath.alignToPoint(pos, dim, pointNum));

    }

    public static void genStairs(RoomClass roomClass, GameObject parent)
    {

        //Default orientation is Down
        switch (roomClass.roomDirVec)
        {
            case "Up":
                //   ^
                //   |
                spawnStairs("Stairs", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, 150, roomClass.roomDim.z / 2 * 100), 0, 1, parent, false);

                Debug.Log(DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1));
                break;
            case "Right":
                //  -->
                spawnStairs("Stairs", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.z / 2 * 100, 150, roomClass.roomDim.x / 2 * 100), 90, 1, parent, true);

                Debug.Log(DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1));
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

    public static void genHallway(RoomClass roomClass, GameObject parent)
    {
        float floorHeight = 15;
        spawnFloor("Floor", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, floorHeight, roomClass.roomDim.z / 2 * 100), 1, parent);
    }

    public static void genRoom(RoomClass roomClass, GameObject parent)
    {

        float wallWidth = 15;
        float wallHeight = 150;
        float floorHeight = 15;

        switch (roomClass.roomDirVec)
        {
            case "Up":
                //   ^
                //   |

                spawnWall("Wall 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, wallHeight, wallWidth), 1, parent);
                spawnWall("Wall 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 5), new Vector3(wallWidth, wallHeight, (roomClass.roomDim.z / 2 * 100) - wallWidth * 2), 5, parent);
                spawnWall("Wall 3", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 7), new Vector3(wallWidth, wallHeight, (roomClass.roomDim.z / 2 * 100) - wallWidth * 2), 7, parent);

                spawnFloor("Floor", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, floorHeight, roomClass.roomDim.z / 2 * 100), 1, parent);

                //  spawnFloor("Roof", DNAMath.determineCorner(roomClass.roomPos + new Vector3(0, 2.7f, 0), roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, floorHeight, roomClass.roomDim.z / 2 * 100), 1, parent);
                if (roomClass.roomDim.x > 0.8f)
                {
                    float doorwayWidth = (((roomClass.roomDim.x / 2) - 0.4f) / 2) * 100;
                    spawnWall("Door 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 2), new Vector3(doorwayWidth, wallHeight, wallWidth), 2, parent);
                    spawnWall("Door 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 3), new Vector3(doorwayWidth, wallHeight, wallWidth), 3, parent);
                }
                else
                {
                    float doorwayWidth = ((roomClass.roomDim.x / 4) - (roomClass.roomDim.x / 8)) * 100;
                    spawnWall("Door 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 2), new Vector3(doorwayWidth, wallHeight, wallWidth), 2, parent);
                    spawnWall("Door 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 3), new Vector3(doorwayWidth, wallHeight, wallWidth), 3, parent);
                }

                break;
            case "Right":
                //  -->
                spawnWall("Wall 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(wallWidth, wallHeight, roomClass.roomDim.z / 2 * 100), 1, parent);
                spawnWall("Wall 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 6), new Vector3((roomClass.roomDim.x / 2 * 100) - wallWidth * 2, wallHeight, wallWidth), 6, parent);
                spawnWall("Wall 3", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 8), new Vector3((roomClass.roomDim.x / 2 * 100) - wallWidth * 2, wallHeight, wallWidth), 8, parent);

                spawnFloor("Floor", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, floorHeight, roomClass.roomDim.z / 2 * 100), 1, parent);

                //  spawnFloor("Roof", DNAMath.determineCorner(roomClass.roomPos + new Vector3(0,2.7f, 0), roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, floorHeight, roomClass.roomDim.z / 2 * 100), 1, parent);

                if (roomClass.roomDim.x > 0.8f)
                {
                    float doorwayWidth = (((roomClass.roomDim.z / 2) - 0.4f) / 2) * 100;
                    spawnWall("Door 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 3), new Vector3(wallWidth, wallHeight, doorwayWidth), 3, parent);
                    spawnWall("Door 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 4), new Vector3(wallWidth, wallHeight, doorwayWidth), 4, parent);
                }
                else
                {
                    float doorwayWidth = ((roomClass.roomDim.z / 4) - (roomClass.roomDim.z / 8)) * 100;
                    spawnWall("Door 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 3), new Vector3(wallWidth, wallHeight, doorwayWidth), 3, parent);
                    spawnWall("Door 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 4), new Vector3(wallWidth, wallHeight, doorwayWidth), 4, parent);
                }

                break;
            case "Left":
                //  <--

                spawnWall("Wall 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 3), new Vector3(wallWidth, wallHeight, roomClass.roomDim.z / 2 * 100), 3, parent);
                spawnWall("Wall 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 6), new Vector3((roomClass.roomDim.x / 2 * 100) - wallWidth * 2, wallHeight, wallWidth), 6, parent);
                spawnWall("Wall 3", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 8), new Vector3((roomClass.roomDim.x / 2 * 100) - wallWidth * 2, wallHeight, wallWidth), 8, parent);

                spawnFloor("Floor", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, floorHeight, roomClass.roomDim.z / 2 * 100), 1, parent);

                //  spawnFloor("Roof", DNAMath.determineCorner(roomClass.roomPos + new Vector3(0, 2.7f, 0), roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, floorHeight, roomClass.roomDim.z / 2 * 100), 1, parent);
                if (roomClass.roomDim.x > 0.8f)
                {
                    float doorwayWidth = (((roomClass.roomDim.z / 2) - 0.4f) / 2) * 100;
                    spawnWall("Door 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 2), new Vector3(wallWidth, wallHeight, doorwayWidth), 2, parent);
                    spawnWall("Door 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(wallWidth, wallHeight, doorwayWidth), 1, parent);
                }
                else
                {
                    float doorwayWidth = ((roomClass.roomDim.z / 4) - (roomClass.roomDim.z / 8)) * 100;
                    spawnWall("Door 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 2), new Vector3(wallWidth, wallHeight, doorwayWidth), 2, parent);
                    spawnWall("Door 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(wallWidth, wallHeight, doorwayWidth), 1, parent);
                }

                break;
            case "Down":
                spawnWall("Wall 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 2), new Vector3(roomClass.roomDim.x / 2 * 100, wallHeight, wallWidth), 2, parent);
                spawnWall("Wall 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 5), new Vector3(wallWidth, wallHeight, (roomClass.roomDim.z / 2 * 100) - wallWidth * 2), 5, parent);
                spawnWall("Wall 3", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 7), new Vector3(wallWidth, wallHeight, (roomClass.roomDim.z / 2 * 100) - wallWidth * 2), 7, parent);

                spawnFloor("Floor", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, floorHeight, roomClass.roomDim.z / 2 * 100), 1, parent);

                //  spawnFloor("Roof", DNAMath.determineCorner(roomClass.roomPos + new Vector3(0, 2.7f, 0), roomClass.roomPos + roomClass.roomDim, 1), new Vector3(roomClass.roomDim.x / 2 * 100, floorHeight, roomClass.roomDim.z / 2 * 100), 1, parent);
                if (roomClass.roomDim.x > 0.8f)
                {
                    float doorwayWidth = (((roomClass.roomDim.x / 2) - 0.4f) / 2) * 100;
                    spawnWall("Door 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(doorwayWidth, wallHeight, wallWidth), 1, parent);
                    spawnWall("Door 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 4), new Vector3(doorwayWidth, wallHeight, wallWidth), 4, parent);

                }
                else
                {
                    float doorwayWidth = ((roomClass.roomDim.x / 4) - (roomClass.roomDim.x / 8)) * 100;
                    spawnWall("Door 1", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 1), new Vector3(doorwayWidth, wallHeight, wallWidth), 1, parent);
                    spawnWall("Door 2", DNAMath.determineCorner(roomClass.roomPos, roomClass.roomPos + roomClass.roomDim, 4), new Vector3(doorwayWidth, wallHeight, wallWidth), 4, parent);
                }

                break;
            default:

                break;
        }

    }


    public bool checkAreaRequirement()
    {
        //Must meet 95% area requirement

        bool verdict = true;

        List<float> areas = new List<float>();

        List<float> areaReq = new List<float>();


        for (int i = 0; i < floorClasses.Count; i++)
        {
            areas.Add(0);
            areaReq.Add(floorClasses[i].floorDim.x * floorClasses[i].floorDim.z);
           // Debug.Log("X Building: " + floorClasses[i].floorDim.x);
           // Debug.Log("Z Building: " + floorClasses[i].floorDim.z);

            for (int j = 0; j < floorClasses[i].roomList.Count; j++)
            {
                //Add area of each room on the floor

               // Debug.Log("X: " + floorClasses[i].roomList[j].roomDim.x);
               // Debug.Log("Z: " + floorClasses[i].roomList[j].roomDim.z);
              //  Debug.Log("Area of room: " + (floorClasses[i].roomList[j].roomDim.x * floorClasses[i].roomList[j].roomDim.z));

                areas[i] = areas[i] + (floorClasses[i].roomList[j].roomDim.x * floorClasses[i].roomList[j].roomDim.z);
            }

            //Check and remove the area from previous floor 

            //Check if the floor under is possible
            if (i - 1 >= 0)
            {
                for (int j = 0; j < floorClasses[i - 1].roomList.Count; j++)
                {
                    if (floorClasses[i - 1].roomList[j].roomType == "Stairs")
                    {
                       // Debug.Log("Area of stairs: " + (floorClasses[i - 1].roomList[j].roomDim.x * floorClasses[i - 1].roomList[j].roomDim.z));
                        areas[i] = areas[i] - (floorClasses[i - 1].roomList[j].roomDim.x * floorClasses[i - 1].roomList[j].roomDim.z);
                        areaReq[i] = areaReq[i] - (floorClasses[i - 1].roomList[j].roomDim.x * floorClasses[i - 1].roomList[j].roomDim.z);
                    }
                }
            }

         //   Debug.Log("Area: " + areas[i]);
         //   Debug.Log("Req Area: " + areaReq[i]);
         //   Debug.Log("95% Req Area: " + areaReq[i] * 0.95f);


            if (areas[i] < areaReq[i] * 0.95f)
            {
                verdict = false;
                i = floorClasses.Count;
            }

        }

        return verdict;
    }

}

