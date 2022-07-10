using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using SaveLoadSystem;

public class ObjectLists : MonoBehaviour
{
    [SerializeField] GameObject GameTile;


    [Header("Panels")]
    [SerializeField] RectTransform InCompletePanel;
    [SerializeField] RectTransform CompletePanel;
    [SerializeField] RectTransform InfoPanel; //Also buttons panel

    [Header("Incomplete Section")]
    [SerializeField] float WidthFactorIncomp;
    [SerializeField] RectTransform IncompleteTitle;
    [SerializeField] RectTransform IncompleteScroll;
    [SerializeField] GameObject IncompleteContent;
    GridLayoutGroup incompleteContentLayout;

    [Header("Complete Section")]
    [SerializeField] float WidthFactorComp;
    [SerializeField] RectTransform CompleteTitle;
    [SerializeField] RectTransform CompleteScroll;
    [SerializeField] GameObject CompleteContent;
    GridLayoutGroup completeContentLayout;


    [Header("Info Section")]
    [SerializeField] float WidthFactorInfo;

    [Header("UIItems")]
    [SerializeField] GameObject child;
    [SerializeField] GameObject parent;
    [SerializeField] int Num;


    [Header("Other")]
    [SerializeField] GameObject test;
    [SerializeField] Sprite icon;
    [SerializeField] string name;
    [SerializeField] int floor;
    [SerializeField] int room;

    [SerializeField] RectTransform idk;


    ItemUIControllerClass controller;

    //Incomplete Section
    float IncompletePaddingW;
    float IncompletePaddingH;



    //Complete Section
    float CompletePaddingW;
    float CompletePaddingH;


    //Info Section
    float ScreenHeight;
    float ScreenWidth;

    float TextHeight;

    bool allCompleted;

    bool nextPage = false;
    // Start is called before the first frame update

    HouseClass house;


    void Start()
    {
       // Indestructable.instance.prevScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        ScreenWidth = Screen.width;
        ScreenHeight = Screen.height;

        TextHeight = ScreenHeight * 0.05f;

        getUIComponents();

        SetUIDesign();

        controller = new ItemUIControllerClass(InfoPanel.sizeDelta);

        Populate();
        //Workingpopulate();

        /*
        ObjectInfoClass tile = new ObjectInfoClass(GameTile);


        tile.DimXR = 300;
        tile.DimYR = 100;
        tile.DimZR = 300;

        SaveDataClass data = new SaveDataClass("Testing", true);

        data.SaveClass(tile, "hi");
        */

        // Directory.CreateDirectory(Application.persistentDataPath + "/" + "idk" + "/" + "cantsay");


        // Debug.Log(idk.floorList[0].roomList[0].objList);


       


    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            nextPage = true;
        }
        else
        {
            nextPage = false;
        }

        if (nextPage)
        {
            //Go to the next scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
            nextPage = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(5);
        }

    }


    public void SetUIDesign()
    {

        //Get Padding Values
        CompletePaddingW = CompletePanel.GetComponent<VerticalLayoutGroup>().padding.horizontal + completeContentLayout.padding.horizontal;
        CompletePaddingH = CompletePanel.GetComponent<VerticalLayoutGroup>().padding.vertical + completeContentLayout.padding.vertical;

        IncompletePaddingW = InCompletePanel.GetComponent<VerticalLayoutGroup>().padding.horizontal + incompleteContentLayout.padding.horizontal;
        IncompletePaddingH = InCompletePanel.GetComponent<VerticalLayoutGroup>().padding.vertical + incompleteContentLayout.padding.vertical;



        InCompletePanel.sizeDelta = new Vector2(ScreenWidth * WidthFactorIncomp, ScreenHeight);
        CompletePanel.sizeDelta = new Vector2(ScreenWidth * WidthFactorComp, ScreenHeight);
        InfoPanel.sizeDelta = new Vector2(ScreenWidth * WidthFactorInfo, ScreenHeight);

        IncompleteTitle.sizeDelta = new Vector2((ScreenWidth * WidthFactorIncomp) - IncompletePaddingW, TextHeight);
        IncompleteScroll.sizeDelta = new Vector2((ScreenWidth * WidthFactorIncomp) - IncompletePaddingW, ScreenHeight - (TextHeight + IncompletePaddingH));
        incompleteContentLayout.cellSize = new Vector2((ScreenWidth * WidthFactorIncomp) - (IncompletePaddingW + incompleteContentLayout.padding.horizontal), ((ScreenWidth * WidthFactorIncomp) - IncompletePaddingW) / 4);

        CompleteTitle.sizeDelta = new Vector2((ScreenWidth * WidthFactorComp) - CompletePaddingW, TextHeight);
        CompleteScroll.sizeDelta = new Vector2((ScreenWidth * WidthFactorComp) - CompletePaddingW, ScreenHeight - (TextHeight + CompletePaddingH));
        completeContentLayout.cellSize = new Vector2((ScreenWidth * WidthFactorComp) - (CompletePaddingW + completeContentLayout.padding.horizontal), ((ScreenWidth * WidthFactorComp) - CompletePaddingW) / 4);


    }

    public void Populate()
    {
        List<ObjectInfoClass> info = new List<ObjectInfoClass>();

        //   string basicPath = Application.persistentDataPath;
        //   string houseFilePath = basicPath + "/" + "House" + "/" + "House.txt";

        // house = (HouseClass)SaveDataClass.loadData(houseFilePath, "HouseClass");

        SaveSystemManager.Load();
       // house = SaveSystemManager.LoadLevel(1);

        house = SaveSystemManager.CurrentSaveData.currentHouse;
      
        for (int i = 0; i < house.floorList.Count; i++)
        {
            for (int j = 0; j < house.floorList[i].roomList.Count; j++)
            {
                for (int g = 0; g < house.floorList[i].roomList[j].objList.Count; g++)
                {
                    info.Add(house.floorList[i].roomList[j].objList[g]);
                }
            }
        }

        Debug.Log("length : "+ info.Count);

        int completeCount = 0;

        allCompleted = true;
        for (int i = 0; i < info.Count; i++)
        {
            // Debug.Log (i);
            if (info[i].completed == true)
            {
                if (allCompleted == false)
                {
                    //do nothing
                }
                else
                {
                    allCompleted = true;
                }
                completeCount++;
                ItemUIClass newUi = new ItemUIClass(completeContentLayout.cellSize, child, CompleteContent, info[i]);
                newUi.addController(controller);
            }
            else
            {
                allCompleted = false;
                ItemUIClass newUi = new ItemUIClass(incompleteContentLayout.cellSize, child, IncompleteContent, info[i]);
                newUi.addController(controller);
            }
        }


        if (completeCount <= 0)
        {
            DialogueTrigger idk = this.gameObject.GetComponent<DialogueTrigger>();

            idk.TriggerDialogue();

            SaveSystemManager.CurrentSaveData.firstNarratorObjectMod = true;

            SaveSystemManager.Save();
        }

        if (allCompleted)
        {
            controller.allCompleted = true;
            controller.showCompletedButton();
        } 
    }

    void getUIComponents()
    {
        incompleteContentLayout = IncompleteContent.GetComponent<GridLayoutGroup>();
        completeContentLayout = CompleteContent.GetComponent<GridLayoutGroup>();

    }


    void getClassInfo()
    {
    }




    void Workingpopulate()
    {
        List<ObjectInfoClass> info = new List<ObjectInfoClass>();
        house = new HouseClass(new Vector3(0, 0, 0), new Vector3(0, 0, 0));

        for (int i = 0; i < house.floorList.Count; i++)
        {
            for (int j = 0; j < house.floorList[i].roomList.Count; j++)
            {
                for (int g = 0; g < house.floorList[i].roomList[j].objList.Count; g++)
                {
                    info.Add(house.floorList[i].roomList[j].objList[g]);
                }
            }
        }

        allCompleted = true;
        for (int i = 0; i < info.Count; i++)
        {
            // Debug.Log (i);
            if (info[i].completed == true)
            {
                if (allCompleted == false)
                {
                    //do nothing
                }
                else
                {
                    allCompleted = true;
                }
                ItemUIClass newUi = new ItemUIClass(completeContentLayout.cellSize, child, CompleteContent, info[i]);
                newUi.addController(controller);
            }
            else
            {
                allCompleted = false;
                ItemUIClass newUi = new ItemUIClass(incompleteContentLayout.cellSize, child, IncompleteContent, info[i]);
                newUi.addController(controller);
            }
        }


        if (allCompleted)
        {
            controller.allCompleted = true;
            controller.showCompletedButton();
        }




    }
}
