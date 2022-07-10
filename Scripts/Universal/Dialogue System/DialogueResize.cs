using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueResize : MonoBehaviour
{
    [SerializeField] RectTransform DiaBox;
    [SerializeField] RectTransform Title;
    [SerializeField] RectTransform Dialogue;
    [SerializeField] RectTransform Continue;
    [SerializeField] VerticalLayoutGroup layoutG;

    // [SerializeField] float posX;
    //[SerializeField] float posY;

    //Add a system to manipulate animation location


    // Start is called before the first frame update
    void Start()
    {

        

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;


        float boxW = screenWidth * 0.6f;
        float boxH = screenHeight * 0.4f;


        float everythingHeight = boxH * 0.90f;
        float everythingWidth = boxW * 0.9f;
        float titleHeight = everythingHeight * 0.3f;
        float diaHeight = everythingHeight * 0.55f;
        float contH = everythingHeight * 0.15f;
        float contW = everythingWidth * 0.85f;

        float spacing = boxH - everythingHeight;


        layoutG.padding.top = (int)spacing / 2;
        layoutG.padding.bottom = (int)spacing / 2;


        DiaBox.sizeDelta = new Vector2(boxW, boxH);

        Title.sizeDelta = new Vector2(everythingWidth, titleHeight);
        Dialogue.sizeDelta = new Vector2(everythingWidth, diaHeight);
        Continue.sizeDelta = new Vector2(contW, contH);

    }

    // Update is called once per frame
    void Update()
    {

    }
}



