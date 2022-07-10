using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUIClass
{
    GameObject ItemUI;

   // GameObject newobj;
    //  GameObject parent;
    // Transform parent;
    HorizontalLayoutGroup backgroundLayout;

    RectTransform background;
    RectTransform image;
    RectTransform textSection;
    RectTransform name;
    RectTransform floor;
    RectTransform room;

    Image imageImage;
    Text nameText;
    Text floorText;
    Text roomText;


    Button backgroundButton;

    ObjectInfoClass info;

    ItemUIControllerClass controller;


    // string ObjName = "ItemUI";
    Button.ButtonClickedEvent onClickListener = new Button.ButtonClickedEvent();

    Vector2 itemSize;


    public ItemUIClass(Vector2 cellSize, GameObject obj, GameObject parent, ObjectInfoClass info) //Add an info class
    {

        this.info = info;
        
        ItemUI = GameObject.Instantiate(obj, parent.transform);

        getUIComponents();

        itemSize = cellSize;

        setUIComponents();

        setItemInfo();

        backgroundButton.onClick = onClickListener;

       
    }

    public void addController (ItemUIControllerClass controller)
    {
        this.controller = controller;
        onClickListener.AddListener(buttonClick);
      //  backgroundButton.onClick = onClickListener;
    }

    void getUIComponents()
    {
        backgroundLayout = ItemUI.transform.Find("Background").GetComponent<HorizontalLayoutGroup>();
        background = ItemUI.transform.Find("Background").GetComponent<RectTransform>();
        backgroundButton = ItemUI.transform.Find("Background").GetComponent<Button>();

        textSection = ItemUI.transform.Find("Background/TextSection").GetComponent<RectTransform>();


        image = ItemUI.transform.Find("Background/Image").GetComponent<RectTransform>();
        name = ItemUI.transform.Find("Background/TextSection/Name").GetComponent<RectTransform>();
        floor = ItemUI.transform.Find("Background/TextSection/Floor").GetComponent<RectTransform>();
        room = ItemUI.transform.Find("Background/TextSection/Room").GetComponent<RectTransform>();


        imageImage = image.gameObject.GetComponent<Image>();
        nameText = name.gameObject.GetComponent<Text>();
        floorText = floor.gameObject.GetComponent<Text>();
        roomText = room.gameObject.GetComponent<Text>();



    }

    void setUIComponents()
    {
        //4 x 1 aspect ratio 4 width, 1 height

        float verticalDim = itemSize.y - backgroundLayout.padding.vertical;
        float textHeight = verticalDim / 3;
        float textWidth = itemSize.x - (verticalDim + backgroundLayout.padding.horizontal + backgroundLayout.spacing);
        Vector2 textDim = new Vector2(textWidth, textHeight);

        image.sizeDelta = new Vector2(verticalDim, verticalDim);

        textSection.sizeDelta = new Vector2(textWidth, verticalDim);
        name.sizeDelta = textDim;
        floor.sizeDelta = textDim;
        room.sizeDelta = textDim;
    }

    void setItemInfo()
    {
        nameText.text = "Name : " + info.Name;
        floorText.text = "Floor : " + info.FloorNum;
        roomText.text = "Room : " + info.RoomNum;

       
        Texture2D image = Resources.Load(info.ImageIconPath) as Texture2D;
        imageImage.sprite = Sprite.Create(image, new Rect(0.0f, 0.0f, image.width, image.height), new Vector2(0.5f, 0.5f), 100.0f);

    }

    void buttonClick ()
    {
        controller.transferInfo(info);
    }

    public GameObject getItem ()
    {
        return info.gameObj;
    }

    public Button.ButtonClickedEvent getListener ()
    {
        return onClickListener;
    }

}
