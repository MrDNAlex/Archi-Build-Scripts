using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SaveLoadSystem;

public class HouseDimensionScript : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] GameObject landmark1;
    [SerializeField] GameObject landmark2;
    [SerializeField] GameObject target;
    [SerializeField] GameObject cursor;
    [SerializeField] GameObject HouseParent;


    [SerializeField] RectTransform panel;
    [SerializeField] RectTransform stream;
    [SerializeField] Texture streamTexture;

    //CameraButtons
    [Header("Sections")]
    [SerializeField] RectTransform CameraButtons;
    [SerializeField] RectTransform ModifyButtons;
    [SerializeField] RectTransform Description;

    //CameraButtons
    [Header("CameraButtons")]
    [SerializeField] float CamWeight;
    // [SerializeField] RectTransform CameraText;
    [SerializeField] RectTransform CamButtons;

    //ControllerButtons
    [Header("ControllerButtons")]
    [SerializeField] float ContWeight;
    [SerializeField] RectTransform ModifyTitle;
    [SerializeField] RectTransform XTitle;
    [SerializeField] RectTransform XController;
    [SerializeField] RectTransform YTitle;
    [SerializeField] RectTransform YController;
    // [SerializeField] RectTransform ZTitle;
    //  [SerializeField] RectTransform ZController;

    //Description Area
    [Header("ModifyArea")]
    [SerializeField] float DescWeight;
    [SerializeField] RectTransform DescTitle;
    [SerializeField] RectTransform DescText;
    //[SerializeField] RectTransform HouseTog;
    [SerializeField] RectTransform SaveButton;

    [SerializeField] DialogueManager DMan;



    Vector3 cursorHide = new Vector3(1000, 1000, 1000);


    Vector3 perspViewPos = new Vector3(9.93f, 10, -30);
    Vector3 skyView = new Vector3(9.94f, 10, 2.244f);

    Vector3 CamVec = new Vector3(0, 0, 1);

    Vector3 mouseInit;
    Vector3 mousePos;

    Vector3 aboveGround = Vector3.zero;


    Vector3 initCorner;


    Button camToggle;
    Text camToggleText;
    Button setDim;
    Button DimPosTogBtn;
    Text DimPosTogText;
   
    //Button Sky;


    //Mod Controllers
    Button PlusX;
    Button PlusY;
    //  Button PlusZ;
    Button MinusX;
    Button MinusY;
    //  Button MinusZ;

    InputField InX;
    InputField InY;
    //  InputField InZ;

    Text InXText;
    Text InYText;

    Text InXTitle;
    Text InYTitle;

    Text reqText;
    //  Text InZText;

    Button Save;
   // Button HouseTogBtn;
  //  Text HouseTogText;

   // CameraClass camClass;


    bool perspView;
    bool setDimensions;

    bool houseTog = false;

    float valWidth;
    float valLength;

    float offsetX;
    float offsetY;

    //10 cm, will have to multiply on this screen probably to meet to the design requirements
    float OneUnit = 0.1f;


    float houseWidthReq;
    float houseLengthReq;

    bool nextPage;

    bool mouseDown;
    bool mouseClick;

    float globalUnit = 1f;

    bool drawing;

    bool DimOrPos; //true = Dim , false = Pos

    int[] idk = new int[3];

    BlueprintControls blueControls;


    // Start is called before the first frame update
    void Start()
    {
        SaveSystemManager.Load();
    
        SaveSystemManager.CurrentSaveData.currentLevel = 1;
      
        DimOrPos = true;
        
        //Indestructable.instance.prevScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        houseWidthReq = 25;
        houseLengthReq = 30;

        setCamVectors();
     
        setUpUI();

        getButtons();
        setUpButtons();
      
        perspView = false;

        cameraToggle();

        setRequirement();

        aboveGround.y = 0.01f;

        DialogueTrigger idk = this.gameObject.GetComponent<DialogueTrigger>();

        idk.TriggerDialogue();

    }

    // Update is called once per frame
    void Update()
    {

       mouseControls();

        if (setDimensions)
        {
            cursor.transform.position = getMousePos(globalUnit);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            nextPage = true;
        }
        else
        {
            nextPage = false;
        }

        listenControls();

       mouseTrack();
    }
    void setUpUI()
    {
        //Values
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float panelWidth = screenWidth * 0.25f;
        float horPad = panel.gameObject.GetComponent<VerticalLayoutGroup>().padding.horizontal;
        float vertPad = panel.gameObject.GetComponent<VerticalLayoutGroup>().padding.vertical;
        float spacing = panel.gameObject.GetComponent<VerticalLayoutGroup>().spacing * 2;

        float width = panelWidth;

        float textheight = ((screenHeight * ContWeight) - (vertPad + spacing)) / 6;
        float buttonSize = ((screenHeight * ContWeight) - (vertPad + spacing)) / 6;

        float descText = (screenHeight * DescWeight) - (vertPad + spacing + textheight + (buttonSize * 2));

        streamTexture.width = (int)Mathf.Floor(screenWidth * 0.75f);
        streamTexture.height = (int)Mathf.Floor(screenHeight);


        panel.sizeDelta = new Vector2(panelWidth, screenHeight);
        stream.sizeDelta = new Vector2(screenWidth * 0.75f, screenHeight);


        //Area Layout
        CameraButtons.sizeDelta = new Vector2(width, (screenHeight * CamWeight) - (vertPad + spacing));
        ModifyButtons.sizeDelta = new Vector2(width, screenHeight * ContWeight);
        Description.sizeDelta = new Vector2(width, screenHeight * DescWeight);

        //Camera Buttons

        CamButtons.sizeDelta = new Vector2(width - horPad, (screenHeight * CamWeight) - (vertPad + spacing));

        //ControllerButtons
        ModifyTitle.sizeDelta = new Vector2(width - horPad, textheight);
        XTitle.sizeDelta = new Vector2(width - horPad, textheight);
        YTitle.sizeDelta = new Vector2(width - horPad, textheight);
        // ZTitle.sizeDelta = new Vector2(width - horPad, TextSize);
        XController.sizeDelta = new Vector2(width - horPad, buttonSize);
        YController.sizeDelta = new Vector2(width - horPad, buttonSize);
        // ZController.sizeDelta = new Vector2(width - horPad, ButtonSize);

        //DescriptionObjects
        DescTitle.sizeDelta = new Vector2(width - horPad, textheight);
        DescText.sizeDelta = new Vector2(width - horPad, descText);
        SaveButton.sizeDelta = new Vector2(width - horPad, buttonSize);
       // HouseTog.sizeDelta = new Vector2(width - horPad, buttonSize);
    }

    void getButtons()
    {

        camToggle = CamButtons.transform.Find("CameraToggle").GetComponent<Button>();
        camToggleText = camToggle.transform.Find("CamTogText").GetComponent<Text>();
        setDim = CamButtons.transform.Find("SetDimensions").GetComponent<Button>();
        DimPosTogBtn = CamButtons.transform.Find("DimensionPositionTog").GetComponent<Button>();
        DimPosTogText = DimPosTogBtn.transform.Find("DimPosTogText").GetComponent<Text>();

        PlusX = XController.transform.Find("PlusX").GetComponent<Button>();
        PlusY = YController.transform.Find("PlusY").GetComponent<Button>();
        MinusX = XController.transform.Find("MinusX").GetComponent<Button>();
        MinusY = YController.transform.Find("MinusY").GetComponent<Button>();

        InX = XController.transform.Find("SizeX").GetComponent<InputField>();
        InY = YController.transform.Find("SizeY").GetComponent<InputField>();


        InXTitle = ModifyButtons.transform.Find("X").GetComponent<Text>();
        InYTitle = ModifyButtons.transform.Find("Y").GetComponent<Text>();


        InXText = InX.textComponent;
        InYText = InY.textComponent;

        InXText.horizontalOverflow = HorizontalWrapMode.Wrap;
        InYText.horizontalOverflow = HorizontalWrapMode.Wrap;

        reqText = DescText.GetComponent<Text>();

        Save = SaveButton.gameObject.GetComponent<Button>();
       // HouseTogBtn = HouseTog.gameObject.GetComponent<Button>();
       // HouseTogText = HouseTog.transform.Find("HouseTogText").GetComponent<Text>();

    }

    void setUpButtons()
    {
        camToggle.onClick.AddListener(cameraToggle);
        setDim.onClick.AddListener(dimensionToggle);
        DimPosTogBtn.onClick.AddListener(dimOrPosToggle);

        PlusX.onClick.AddListener(PlusXBut);
        PlusY.onClick.AddListener(PlusYBut);
        MinusX.onClick.AddListener(MinusXBut);
        MinusY.onClick.AddListener(MinusYBut);

        InX.onEndEdit.AddListener(delegate { InputX(); });
        InY.onEndEdit.AddListener(delegate { InputY(); });

        Save.onClick.AddListener(saveBut);
       // HouseTogBtn.onClick.AddListener(togHouse);

        // Debug.Log("Here");
    }

   

    void cameraToggle()
    {
        if (perspView)
        {
            //2D

            cam.transform.position = skyView;
            Quaternion Rot = new Quaternion(0, 0, 0, 0);
            Rot.eulerAngles = DNAMath.CalcEulerAngleRot(skyView, target.transform.position, CamVec);
            cam.transform.rotation = Rot;
            camToggleText.text = "2D";
            cam.orthographic = true;
            cam.orthographicSize = 15;
            perspView = false;
           // Debug.Log(target.transform.position - cam.transform.position);

        }
        else
        {
            //3D
            cam.transform.position = perspViewPos;
            Quaternion Rot = new Quaternion(0, 0, 0, 0);
            Rot.eulerAngles = DNAMath.CalcEulerAngleRot(perspViewPos, target.transform.position, CamVec);
            cam.transform.rotation = Rot;
            camToggleText.text = "3D";
            perspView = true;
            cam.orthographic = false;
           // Debug.Log(target.transform.position - cam.transform.position);
        }

    }

    void setCamVectors()
    {
        skyView = target.transform.position;
        skyView.y = 10;

        perspViewPos.x = target.transform.position.x;
    }

    void setRequirement()
    {
        string req = "Width : " + houseWidthReq + "m" + "\n"
            + "Height : " + houseLengthReq + "m";
        reqText.text = req;

    }

    void listenControls()
    {
        //if (Input.GetKeyDown(KeyCode.Escape))
      //  {
      //      UnityEngine.SceneManagement.SceneManager.LoadScene(5);
      //  }

    }


    void saveBut ()
    {
        if (checkRequirements())
        {
            //Save dimensions of house into file 
            Vector3 initDim = new Vector3 (valWidth, 0, valLength);
           // Debug.Log(valWidth);
           // Debug.Log(valLength);

            HouseClass house = new HouseClass(new Vector3(initCorner.x - landmark1.transform.position.x, 0, initCorner.z - landmark1.transform.position.z), initDim);

            SaveSystemManager.Load();

            SaveSystemManager.SaveLevel(1, house);
            SaveSystemManager.CurrentSaveData.currentHouse = house;
            

            SaveSystemManager.Save();

          //  Debug.Log("Saving");


            UnityEngine.SceneManagement.SceneManager.LoadScene(3);

           
        } else
        {

            //Message, the requirements are not met

            DialogueTrigger idk = this.gameObject.transform.GetChild(0).GetComponent<DialogueTrigger>();

            idk.TriggerDialogue();

        }
    }

    bool checkRequirements ()
    {
        if (houseWidthReq == valWidth && houseLengthReq == valLength)
        {
            return true;
        } 
        return false;
    }


    public void PlusXBut()
    {
        if (DimOrPos)
        {
            //Dim
            if (checkPossible(initCorner, valWidth + OneUnit, valLength, true, landmark2.transform.position))
            {
                valWidth += OneUnit;
            }

        }
        else
        {

            //Pos
            if (checkPossible(new Vector3(initCorner.x + OneUnit, initCorner.y, initCorner.z), valWidth, valLength, false, landmark2.transform.position))
            {
                initCorner.x += OneUnit;
            }
        }
        // setDimensions = true;

        updateInputVals();

    }

    public void PlusYBut()
    {
        if (DimOrPos)
        {
            //Dim
            if (checkPossible(initCorner, valWidth, valLength + OneUnit, true, landmark2.transform.position))
            {
                valLength += OneUnit;
            }

        }
        else
        {
            //Pos
            if (checkPossible(new Vector3(initCorner.x, initCorner.y, initCorner.z + OneUnit), valWidth, valLength, false, landmark2.transform.position))
            {
                initCorner.z += OneUnit;
            }

        }

        // setDimensions = true;

        updateInputVals();

    }

    public void MinusXBut()
    {
        if (DimOrPos)
        {
            //Dim
            if (valWidth - OneUnit > 0)
            {
                valWidth -= OneUnit;
            }



        }
        else
        {
            //Pos
            if ((initCorner.x - landmark1.transform.position.x) - OneUnit > 0)
            {
                initCorner.x -= OneUnit;
            }

        }

        //  setDimensions = true;
        // valWidth -= OneUnit;
        updateInputVals();

    }
    public void MinusYBut()
    {
        if (DimOrPos)
        {
            //Dim
            if (valLength - OneUnit > 0)
            {
                valLength -= OneUnit;
            }

        }
        else
        {
            //Pos
            if ((initCorner.z - landmark1.transform.position.z) - OneUnit > 0)
            //if (checkPossible(new Vector3(initCorner.x, initCorner.y, initCorner.z - OneUnit), valWidth, valLength, false))
            {
                initCorner.z -= OneUnit;
            }
            //initCorner.z -= OneUnit;
        }

        // setDimensions = true;
        // valLength -= OneUnit;
        updateInputVals();

    }

    public void InputX()
    {
        if (DimOrPos)
        {
            float oldVal = valWidth;
            float newVal;
            try
            {

                newVal = float.Parse(InX.text);
                if (checkPossible(initCorner, newVal, valLength, true, landmark2.transform.position))
                {
                    valWidth = newVal;

                }
                else
                {
                    valWidth = oldVal;
                }


                updateInputVals();
            }
            catch
            {

                valWidth = DNAMath.getMeasurement(InX.text, oldVal);

                /*
                 else if (num + "cm" == input || num + "cM" == input || num + "Cm" == input || num + "CM" == input) 
                {
                    //CM
                    valWidth = num;
                }
                 */

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

                if (checkPossible(initCorner, valWidth, valLength, false, landmark2.transform.position))
                {
                    initCorner.x = newVal + landmark1.transform.position.x;
                }
                else
                {
                    initCorner.x = oldVal;
                }
                //initCorner.x = newVal;
                updateInputVals();
            }
            catch
            {

                initCorner.x = DNAMath.getMeasurement(InX.text, oldVal);

                if (initCorner.x == oldVal)
                {

                } else
                {
                    initCorner.x = initCorner.x + landmark1.transform.position.x;
                }

                updateInputVals();
            }
        }


    }

    public void InputY()
    {
        if (DimOrPos)
        {
            float oldVal = valLength;
            float newVal;
            try
            {
                newVal = float.Parse(InY.text);

                if (checkPossible(initCorner, valWidth, newVal, true, landmark2.transform.position))
                {
                    valLength = newVal;
                }
                else
                {
                    valLength = oldVal;
                }


                //  valLength = newVal;

            }
            catch
            {
                valLength = DNAMath.getMeasurement(InY.text, oldVal);
            }
        }
        else
        {
            float oldVal = initCorner.z;
            float newVal;
            try
            {
                newVal = float.Parse(InY.text);

                if (checkPossible(initCorner, valWidth, newVal, false, landmark2.transform.position))
                {
                    initCorner.z = newVal + landmark1.transform.position.z;
                }
                else
                {
                    initCorner.z = oldVal;
                }

                // initCorner.z = newVal - landmark1.transform.position.z;

            }
            catch
            {
                initCorner.y = DNAMath.getMeasurement(InY.text, oldVal);

                if (initCorner.y == oldVal)
                {

                }
                else
                {
                    initCorner.y = initCorner.y + landmark1.transform.position.z;
                }
            }
        }
        updateInputVals();

    }

    public void updateInputVals()
    {
        if (DimOrPos)
        {
            DimPosTogText.text = "Dimension";



            InXTitle.text = "Width";
            InYTitle.text = "Length";

            //Clean the values 
            valWidth = DNAMath.snapToUnit(valWidth, 0.1f);
            valLength = DNAMath.snapToUnit(valLength, 0.1f);


            //Update the values
            InX.text = valWidth.ToString() + "m";
            InY.text = valLength.ToString() + "m";
        }
        else
        {
            DimPosTogText.text = "Position";

            InXTitle.text = "Position X";
            InYTitle.text = "Position Y";

            //Pos
            initCorner.x = DNAMath.snapToUnit(initCorner.x, 0.1f);
            initCorner.z = DNAMath.snapToUnit(initCorner.z, 0.1f);

            InX.text = (initCorner.x - landmark1.transform.position.x) + "m";
            InY.text = (initCorner.z - landmark1.transform.position.z) + "m";
        }

        //Update Square
        foreach (Transform child in HouseParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        DrawSquare(initCorner, DNAMath.makeCorner(initCorner, new Vector3(valWidth, 0, valLength)));

    }

    public void dimensionToggle()
    {
        if (setDimensions)
        {
            //turn off
            setDim.GetComponent<Image>().color = Color.white;
            setDimensions = false;

            cursor.transform.position = cursorHide;
        }
        else
        {
            //turn on 
            setDim.GetComponent<Image>().color = Color.green;
            setDimensions = true;
        }
    }

    public Vector3 getMousePos(float unit)
    {
        Vector3 pos = Vector3.one;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            if (checkInPlay(raycastHit.point, landmark1.transform.position, landmark2.transform.position))
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
        return new Vector3(DNAMath.snapToUnit(pos.x, unit), pos.y, DNAMath.snapToUnit(pos.z, unit));

    }


    public void mouseTrack()
    {

        if (setDimensions)
        {
            if (mouseClick)
            {
                mouseInit = getMousePos(globalUnit) + aboveGround;
                HouseParent.transform.position = mouseInit;


                drawing = true;
            }

            if (mouseDown)
            {
                mousePos = getMousePos(globalUnit) + aboveGround;

                foreach (Transform child in HouseParent.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }

                initCorner = DNAMath.determineCorner(mouseInit, mousePos, 1);
                valWidth = DNAMath.houseSize(mouseInit, mousePos).x;
                valLength = DNAMath.houseSize(mouseInit, mousePos).z;

                DrawSquare(initCorner, DNAMath.makeCorner(initCorner, new Vector3(valWidth, 0, valLength)));

                updateInputVals();

                drawing = true;
            }
            else
            {
                if (drawing)
                {
                    setDimensions = true;
                    dimensionToggle();

                    drawing = false;
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
    }

    public void DrawSquare(Vector3 start, Vector3 end)
    {
        //Corner 2                   Corner 3

        //Corner 1                   Corner 4
        Vector3 Corner1 = start;
        Vector3 Corner2 = start;
        Corner2.z = end.z;
        Vector3 Corner3 = end;
        Vector3 Corner4 = end;
        Corner4.z = Corner1.z;

        DrawLine(Corner1, Corner2, Color.red, HouseParent);
        DrawLine(Corner2, Corner3, Color.red, HouseParent);
        DrawLine(Corner3, Corner4, Color.red, HouseParent);
        DrawLine(Corner4, Corner1, Color.red, HouseParent);

        //Make a system here that checks for which vector is the lowest in both dimensions (equivalent to the start pos where the house will be) (Maybe make a function for that) 
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

    public void updateCamera(Vector3 topDown)
    {

        if (perspView)
        {

        }
        else
        {

            //Deltaposition for mobile devices

            //if (Application.platform == RuntimePlatform.)
            if (checkMobile())
            {


                Vector2 touch = (Input.GetTouch(0).deltaPosition/10)*-1;


                offsetX += touch.x;
                offsetY += touch.y;
            }
            else
            {
                offsetX -= Input.GetAxisRaw("Mouse X") * Time.deltaTime * 100;
                offsetY -= Input.GetAxisRaw("Mouse Y") * Time.deltaTime * 100;

            }


            offsetX = Mathf.Clamp(offsetX, -10, 10);
            offsetY = Mathf.Clamp(offsetY, -15, 15);

            cam.transform.position = new Vector3(topDown.x + offsetX, topDown.y, topDown.z + offsetY);
        }

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

    public static bool checkPossible(Vector3 vec, float val1, float val2, bool Dim, Vector3 refMax)
    {
        float widthleft = refMax.x - vec.x;
        float heightleft = refMax.z - vec.z;

        if (Dim)
        {
            if (val1 > widthleft)
            {
                return false;
            }
            else
            {
                if (val2 > heightleft)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        else
        {
            if (vec.x + val1 > refMax.x)
            {
                return false;
            }
            else
            {
                if (vec.z + val2 > refMax.z)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
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

    public void mouseControls()
    {
        if (DNAMath.offScreen(0.75f, true, true))
        {
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

    void passStuff ()
    {

        blueControls.cam = cam;

        blueControls.camToggle = camToggle;
        blueControls.camToggleText = camToggleText;
        blueControls.setDim = setDim;
        blueControls.DimPosTogBtn = DimPosTogBtn;
        blueControls.DimPosTogText = DimPosTogText;

        blueControls.PlusX = PlusX;
        blueControls.PlusY = PlusY;
        blueControls.MinusX = MinusX;
        blueControls.MinusY = MinusY;

        blueControls.InX = InX;
        blueControls.InY = InY;

        blueControls.InXText = InXText;
        blueControls.InYText = InYText;
        blueControls.InXTitle = InXTitle;
        blueControls.InYTitle = InYTitle;

        blueControls.reqText = reqText;
        blueControls.Save = Save;

    }

  

   // //Assets/Resources/Materials and Textures/Texture Packs/Suburban Structure Kit/Prefabs/FullPrefabs/SuburbanHouseVer2.prefab

}