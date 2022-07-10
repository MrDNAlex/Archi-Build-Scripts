using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SaveLoadSystem;

public class SceneScript : MonoBehaviour
{
    private float ScreenWidth;
    private float ScreenHeight;

    float ViewWidth;
    float ViewHeight;

    [Header("Random")]
    [SerializeField] RectTransform ObjectView;
    [SerializeField] RectTransform ButtonPanel;
    [SerializeField] Material ScreenMaterial;
    [SerializeField] Texture GridView;
    [SerializeField] Slider zoom;
    [SerializeField] Light light;
    [SerializeField] GameObject Background;

    [Header("UI Regions")]
    [SerializeField] RectTransform CameraButtons;
    [SerializeField] RectTransform ModifyButtons;
    [SerializeField] RectTransform Description;

    //CameraButtons
    [Header("CameraButtons")]
    [SerializeField] float CamWeight;
    [SerializeField] RectTransform CameraText;
    [SerializeField] RectTransform CamButtons;

    //ControllerButtons
    [Header("ControllerButtons")]
    [SerializeField] float ContWeight;
    [SerializeField] RectTransform ModifyTitle;
    [SerializeField] RectTransform XTitle;
    [SerializeField] RectTransform XController;
    [SerializeField] RectTransform YTitle;
    [SerializeField] RectTransform YController;
    [SerializeField] RectTransform ZTitle;
    [SerializeField] RectTransform ZController;

    //Description Area
    [Header("ModifyArea")]
    [SerializeField] float DescWeight;
    [SerializeField] RectTransform DescTitle;
    [SerializeField] RectTransform DescText;
    [SerializeField] RectTransform SaveButton;

    [Header("Other")]
    [SerializeField] Camera GridCam;
    [SerializeField] Text Requirements;

    [SerializeField] GameObject GridParent;



    float ContSpacing;
    float CamSpacing;
    //  float DescSpacing;
    float TextSize;
    float ButtonSize;

    //Camera GridCam;

    GameObject newObject;

    ObjectInfoClass info;
    ObjectScript objScript;
    GridClass grid;
    CameraClass cam;
    ControllerClass cont;
    int maxW = 2000;

    Vector3 SizeReq; 

    string unit = "m";
    string length = "2";


    // Start is called before the first frame update
    void Start()
    {
       // Indestructable.instance.prevScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        ScreenWidth = Screen.width;
        ScreenHeight = Screen.height;
        ViewWidth = ScreenWidth * 0.8f;
        ViewHeight = ScreenHeight;

      
        LoadObjectIntoScene();

        //Values
        ContSpacing = 14f;
        CamSpacing = 10f;
        //  DescSpacing = 6f;
        TextSize = ((ScreenHeight * ContWeight * 0.3f) - ContSpacing) / 4;
        ButtonSize = ((ScreenHeight * ContWeight * 0.7f) - ContSpacing) / 3;


        //Set Sizes of Objects
        SetUpUI();

        //Generate Background
        GenerateBackground();


        InitializeClasses();

        SetRequirementText();

    }


    // Update is called once per frame
    void Update()
    {
        cam.UpdateCamView();

        cam.SetCamZoom(zoom.maxValue / zoom.value);

       // if (Input.GetKeyDown(KeyCode.Escape))
       // {
       //     UnityEngine.SceneManagement.SceneManager.LoadScene(5);
       // }

    }

    void SetRequirementText()
    {
        //Actual Values will be passedthrough using somesort of 
        //Requirements.text = "";
        string DimX = "X : " + 2* info.DimR.x/100 + unit + "\n";
        string DimY = "Y : " + 2*info.DimR.y / 100 + unit + "\n";
        string DimZ = "Z : " + 2*info.DimR.z / 100 + unit + "\n";
        Requirements.text = DimX + DimY + DimZ;
        
    }

    void SetUpUI()
    {
        //Set Sizes of Objects
        ObjectView.sizeDelta = new Vector2(ViewWidth, ViewHeight);
        GridView.width = (int)Mathf.Floor(ViewWidth);
        GridView.height = (int)Mathf.Floor(ViewHeight);
        ButtonPanel.sizeDelta = new Vector2(ScreenWidth - ViewWidth, ScreenHeight);


        //Area Layout
        CameraButtons.sizeDelta = new Vector2(ScreenWidth - ViewWidth, ScreenHeight * CamWeight);
        ModifyButtons.sizeDelta = new Vector2(ScreenWidth - ViewWidth, ScreenHeight * ContWeight);
        Description.sizeDelta = new Vector2(ScreenWidth - ViewWidth, ScreenHeight * DescWeight);

        //Camera Buttons
        CameraText.sizeDelta = new Vector2(ViewWidth - 20, TextSize);
        CamButtons.sizeDelta = new Vector2(ViewWidth - 20, (ScreenHeight * CamWeight) - (TextSize + CamSpacing));

        //ControllerButtons
        ModifyTitle.sizeDelta = new Vector2(ScreenWidth - 20, TextSize);
        XTitle.sizeDelta = new Vector2(ScreenWidth - 20, TextSize);
        YTitle.sizeDelta = new Vector2(ScreenWidth - 20, TextSize);
        ZTitle.sizeDelta = new Vector2(ScreenWidth - 20, TextSize);
        XController.sizeDelta = new Vector2(ScreenWidth - 20, ButtonSize);
        YController.sizeDelta = new Vector2(ScreenWidth - 20, ButtonSize);
        ZController.sizeDelta = new Vector2(ScreenWidth - 20, ButtonSize);

        //DescriptionObjects
        DescTitle.sizeDelta = new Vector2(ScreenWidth - 20, TextSize);
        DescText.sizeDelta = new Vector2(ScreenWidth - 20, (ScreenHeight * DescWeight * 0.4f));
        SaveButton.sizeDelta = new Vector2(ScreenWidth - 20, (ScreenHeight * DescWeight * 0.4f));
    }

    void GenerateBackground()
    {
        //Generate Background
        GameObject Floor = Instantiate(Background);
        GameObject WallZ = Instantiate(Background);
        GameObject WallX = Instantiate(Background);

        Floor.transform.position = new Vector3(0, -0.01f, 0);
        WallZ.transform.position = new Vector3(-0.01f, 0, 0);
        WallX.transform.position = new Vector3(0, 0, -0.01f);

        Floor.transform.rotation = Quaternion.Euler(0, 0, 0);
        WallZ.transform.rotation = Quaternion.Euler(0, 0, -90);
        WallX.transform.rotation = Quaternion.Euler(90, 0, 0);
    }


    void LoadObjectIntoScene () {
        //SaveDataClass data = new SaveDataClass("Testing", false);

       // info = (ObjectInfoClass)SaveDataClass.loadData(Application.persistentDataPath + "/" + "EditObject.txt", "ObjectInfoClass");

        SaveSystemManager.Load();
        info = SaveSystemManager.CurrentSaveData.editObject;

        // info = (ObjectInfoClass)data.GetClass("ObjInfo");
        info.gameObj.transform.localScale = new Vector3(30, 10, 30);

        newObject = Instantiate(info.gameObj, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        newObject.AddComponent<ObjectScript>();

        objScript = newObject.GetComponent<ObjectScript>();

        SizeReq = new Vector3(info.DimR.x, info.DimR.y, info.DimR.z) *2;

        objScript.req = SizeReq;

        Debug.Log(SaveSystemManager.CurrentSaveData.firstNarratorObjectMod);

        if (SaveSystemManager.CurrentSaveData.firstNarratorObjectMod)
        {
            SaveSystemManager.CurrentSaveData.firstNarratorObjectMod = false;

            DialogueTrigger idk = this.gameObject.GetComponent<DialogueTrigger>();

            idk.TriggerDialogue();

            Debug.Log("Entered");

            SaveSystemManager.Save();

        }

    }
    void InitializeClasses ()
    {

        while (objScript.ObjectMod == null)
        {
            objScript.CreateNewModClass();
        }


        grid = new GridClass(maxW, 0.2f, GridParent);
        cam = new CameraClass(GridCam);
        cam.addExtraInfo(grid, zoom, objScript.gameObject, objScript.ObjectMod, light);
        cont = new ControllerClass(cam, objScript.ObjectMod);
    }

}

