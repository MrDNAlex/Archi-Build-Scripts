using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SaveLoadSystem;
public class ItemUIControllerClass
{

    VerticalLayoutGroup holderLayout;

    RectTransform holder;
    RectTransform image;
    RectTransform name;
    RectTransform floor;
    RectTransform room;
    RectTransform requirement;
    RectTransform reqX;
    RectTransform reqY;
    RectTransform reqZ;
    RectTransform completed;
    RectTransform saveButton;


    Image objectImage;
    Text nameText;
    Text floorText;
    Text roomText;
    Text reqXText;
    Text reqYText;
    Text reqZText;
    Text completedText;
    Text saveButtonText;
    Button saveButtonBtn;

    ObjectInfoClass info;
    ObjectInfoClass lastInfo;

    Vector2 dimension;

    public bool allCompleted;

    public ItemUIControllerClass(Vector2 dim)
    {
        this.dimension = dim;

        getUIComponents();
        setUIComponents();

        if (info == null)
        {
            hideAllUIComponents();
        }
    }


    public void getUIComponents()
    {
        holder = GameObject.Find("Holder").GetComponent<RectTransform>();
        image = holder.transform.Find("ObjectImage").GetComponent<RectTransform>();
        name = holder.transform.Find("Name").GetComponent<RectTransform>();
        floor = holder.transform.Find("Floor").GetComponent<RectTransform>();
        room = holder.transform.Find("Room").GetComponent<RectTransform>();
        requirement = holder.transform.Find("Requirements").GetComponent<RectTransform>();
        reqX = holder.transform.Find("RequirementX").GetComponent<RectTransform>();
        reqY = holder.transform.Find("RequirementY").GetComponent<RectTransform>();
        reqZ = holder.transform.Find("RequirementZ").GetComponent<RectTransform>();
        completed = holder.transform.Find("Completed").GetComponent<RectTransform>();
        saveButton = holder.transform.Find("SaveButton").GetComponent<RectTransform>();
        saveButtonText = saveButton.transform.Find("SaveButtonText").GetComponent<Text>();

        objectImage = image.gameObject.GetComponent<Image>();
        nameText = name.gameObject.GetComponent<Text>();
        floorText = floor.gameObject.GetComponent<Text>();
        roomText = room.gameObject.GetComponent<Text>();
        reqXText = reqX.gameObject.GetComponent<Text>();
        reqYText = reqY.gameObject.GetComponent<Text>();
        reqZText = reqZ.gameObject.GetComponent<Text>();
        completedText = completed.gameObject.GetComponent<Text>();

        saveButtonBtn = saveButton.gameObject.GetComponent<Button>();
        holderLayout = holder.gameObject.GetComponent<VerticalLayoutGroup>();

    }


    void setUIComponents()
    {

        Vector2 dim = dimension;
        float horPad = holderLayout.padding.horizontal;
        float vertPad = holderLayout.padding.vertical;
        float spacingNum = holder.transform.childCount - 1;
        float spacing = holderLayout.spacing * spacingNum;

        float objWidth = (dim.x - horPad);
        float buttonHeight = (dim.y - (vertPad + spacing)) * 0.05f;
        float textHeight = (dim.y - (objWidth + buttonHeight + vertPad + spacing)) / 8;

        image.sizeDelta = new Vector2(objWidth, objWidth);
        name.sizeDelta = new Vector2(objWidth, textHeight);
        floor.sizeDelta = new Vector2(objWidth, textHeight);
        room.sizeDelta = new Vector2(objWidth, textHeight);
        requirement.sizeDelta = new Vector2(objWidth, textHeight);
        reqX.sizeDelta = new Vector2(objWidth, textHeight);
        reqY.sizeDelta = new Vector2(objWidth, textHeight);
        reqZ.sizeDelta = new Vector2(objWidth, textHeight);
        completed.sizeDelta = new Vector2(objWidth, textHeight);
        saveButton.sizeDelta = new Vector2(objWidth, buttonHeight);
    }


    public void transferInfo(ObjectInfoClass inf)
    {
        lastInfo = info;
        info = inf;
       

        if (checkIfSame())
        {

            //Same object clicked

            if (allCompleted)
            {
                showCompletedButton();
                info = null;
            }
            else
            {
                hideAllUIComponents();
                info = null;
            }

        } else
        {
            setInformation();
        }

    }

    public void setInformation()
    {
        showAllUIComponents();
        

        if (info.completed)
        {
            updateUI();
        }
        else
        {
            setUIComponents();
        }


        Texture2D image = Resources.Load(info.ImageIconPath) as Texture2D;
        objectImage.sprite = Sprite.Create(image, new Rect(0.0f, 0.0f, image.width, image.height), new Vector2(0.5f, 0.5f), 100.0f);

       // objectImage.sprite = info.ImageIcon;
        nameText.text = "Name : " + info.Name;
        floorText.text = "Floor : " + info.FloorNum;
        roomText.text = "Room : " + info.RoomNum;
        reqXText.text = "X : " + 2*info.DimR.x/100 + "m";
        reqYText.text = "Y : " + 2*info.DimR.y/100 + "m";
        reqZText.text = "Z : " + 2*info.DimR.z/100 + "m";

        if (info.completed)
        {
            completedText.text = "Completed : " + "Yes";
        }
        else
        {
            completedText.text = "Completed : " + "No";
        }


        if (info.completed == true)
        {
            saveButton.gameObject.SetActive(false);
        }
        else
        {
            if (saveButton.gameObject.activeSelf == false)
            {
                saveButton.gameObject.SetActive(true);
            }
            saveButtonText.text = "Edit";
            saveButtonBtn.onClick.RemoveAllListeners();
            saveButtonBtn.onClick.AddListener(goToEditScene);
        }

    }

    void updateUI()
    {
        Vector2 dim = dimension;
        float horPad = holderLayout.padding.horizontal;
        float vertPad = holderLayout.padding.vertical;
        float spacingNum = holder.transform.childCount - 1;
        float spacing = holderLayout.spacing * spacingNum;

        float objWidth = (dim.x - horPad);
        float buttonHeight = (dim.y - (vertPad + spacing)) * 0.05f;
        float textHeight = (dim.y - (objWidth + vertPad + spacing)) / 8;

        image.sizeDelta = new Vector2(objWidth, objWidth);
        name.sizeDelta = new Vector2(objWidth, textHeight);
        floor.sizeDelta = new Vector2(objWidth, textHeight);
        room.sizeDelta = new Vector2(objWidth, textHeight);
        requirement.sizeDelta = new Vector2(objWidth, textHeight);
        reqX.sizeDelta = new Vector2(objWidth, textHeight);
        reqY.sizeDelta = new Vector2(objWidth, textHeight);
        reqZ.sizeDelta = new Vector2(objWidth, textHeight);
        completed.sizeDelta = new Vector2(objWidth, textHeight);
        // saveButton.sizeDelta = new Vector2(objWidth, buttonHeight);
    }

    public void showCompletedButton()
    {
        setUIComponents();
        hideAllUIComponents();
        showBtn();
        
        saveButtonBtn.onClick.RemoveAllListeners();
        saveButtonBtn.onClick.AddListener(nextScene);
        saveButtonText.text = "Continue";
    }

    public void hideAllUIComponents()
    {

        // holder.gameObject.SetActive(false);
        image.gameObject.SetActive(false);
        name.gameObject.SetActive(false);
        floor.gameObject.SetActive(false);
        room.gameObject.SetActive(false);
        requirement.gameObject.SetActive(false);
        reqX.gameObject.SetActive(false);
        reqY.gameObject.SetActive(false);
        reqZ.gameObject.SetActive(false);
        completed.gameObject.SetActive(false);
        saveButton.gameObject.SetActive(false);

    }

    public void showAllUIComponents()
    {

        // holder.gameObject.SetActive(true);
        image.gameObject.SetActive(true);
        name.gameObject.SetActive(true);
        floor.gameObject.SetActive(true);
        room.gameObject.SetActive(true);
        requirement.gameObject.SetActive(true);
        reqX.gameObject.SetActive(true);
        reqY.gameObject.SetActive(true);
        reqZ.gameObject.SetActive(true);
        completed.gameObject.SetActive(true);
        saveButton.gameObject.SetActive(true);

    }


    public void showBtn()
    {
        saveButton.gameObject.SetActive(true);
    }

    public void nextScene()
    {
        Debug.Log("Going to the next scene");

        UnityEngine.SceneManagement.SceneManager.LoadScene(6);

    }

    public bool checkIfSame()
    {
        bool same = true;
        /*
        if (info.Name == lastInfo.Name)
        {
            same = true;
        }
        else
        {
            same = false;
        }
        if (info.FloorNum == lastInfo.FloorNum)
        {
            same = true;
        }
        else
        {
            same = false;
        }
        if (info.RoomNum == lastInfo.RoomNum)
        {
            same = true;
        }
        else
        {
            same = false;
        }
        if (info.DimXR == lastInfo.DimXR)
        {
            same = true;
        }
        else
        {
            same = false;
        }
        if (info.DimYR == lastInfo.DimYR)
        {
            same = true;
        }
        else
        {
            same = false;
        }
        if (info.DimZR == lastInfo.DimZR)
        {
            same = true;
        }
        else
        {
            same = false;
        }
        */
        if (info == null || lastInfo == null)
        {
            same = false;
        } else
        {
            if (info.objID == lastInfo.objID)
            {
                same = true;
            }
            else
            {
                same = false;
            }
        }
       
        return same;
    }

    void goToEditScene ()
    {
        SaveSystemManager.Load();

        SaveSystemManager.CurrentSaveData.editObject = info;

        SaveSystemManager.Save();


       // SaveDataClass.saveData(info, Application.persistentDataPath + "/" + "EditObject.txt");
        //string data = JsonUtility.ToJson(info);
       // Debug.Log(data);

       // Debug.Log("Going to edit");

        UnityEngine.SceneManagement.SceneManager.LoadScene(5);
    }

}





