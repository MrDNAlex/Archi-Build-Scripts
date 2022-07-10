using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BlueprintControls 
{
   public Camera cam;
    public GameObject landmark1;
    public GameObject landmark2;
    public GameObject target;
    public GameObject cursor;
    public GameObject HouseParent;

    public Vector3 CamVec = new Vector3(0, 0, 1);

    public Vector3 cursorHide = new Vector3(1000, 1000, 1000);

    Vector3 aboveGround = Vector3.zero;


    public Vector3 skyView;

    public Vector3 mouseInit;
    public Vector3 mousePos;

    public Vector3 initCorner;



    public bool perspView;
    public bool setDimensions;


    public float valWidth;
    public float valLength;

    public float offsetX;
    public float offsetY;

    //10 cm, will have to multiply on this screen probably to meet to the design requirements
    public float OneUnit = 0.1f;


    public float houseWidthReq;
    public float houseLengthReq;

    public bool nextPage;

    public bool mouseDown;
    public bool mouseClick;

    public float globalUnit = 1f;

    public bool drawing;

    public bool DimOrPos; //true = Dim , false = Pos

    public Button camToggle;
    public Text camToggleText;
    public Button setDim;
    public Button DimPosTogBtn;
    public Text DimPosTogText;

    //Button Sky;


    //Mod Controllers
    public Button PlusX;
    public Button PlusY;
    //  Button PlusZ;
    public Button MinusX;
    public Button MinusY;
    //  Button MinusZ;

    public InputField InX;
    public InputField InY;
    //  InputField InZ;

    public Text InXText;
    public Text InYText;

    public Text InXTitle;
    public Text InYTitle;

    public Text reqText;
    //  Text InZText;

    public Button Save;

    //CameraClass camClass;


  

}
