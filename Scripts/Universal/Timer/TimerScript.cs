using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SaveLoadSystem;

public class TimerScript : MonoBehaviour
{
    Text timerText;

    public float currentTime; //Seconds

    //[SerializeField] Vector2 position;

    [SerializeField] bool objectList;

    public bool count = true;
    [SerializeField] bool reset;


    // Start is called before the first frame update
    void Start()
    {
        timerText = this.gameObject.transform.Find("TimerText").GetComponent<Text>();

        if (reset)
        {
            currentTime = 0;
        } else
        {
            SaveSystemManager.Load();

            currentTime = SaveSystemManager.CurrentSaveData.timeElapsed;
        }


      

        if (objectList)
        {
            this.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(-Screen.width, 0, 0);
            Debug.Log("Here");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (count)
        {
            currentTime = currentTime + Time.deltaTime;

            timerText.text = DNAMath.convertToTimeFormat(currentTime);
        } else
        {
            saveStuff();
        }
       
    }

    private void OnDestroy()
    {
        saveStuff();
    }


    private void saveStuff ()
    {
        SaveSystemManager.CurrentSaveData.timeElapsed = currentTime;
        SaveSystemManager.Save();
    }
}
