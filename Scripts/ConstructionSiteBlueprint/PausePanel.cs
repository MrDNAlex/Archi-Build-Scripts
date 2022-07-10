using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveLoadSystem;

public class PausePanel : MonoBehaviour
{
    public static bool PauseStatus = true;

    public GameObject pausePanelUI;


    void Start()
    {
        //pausePanelUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
          //  Resume();
        }
    }

    public void Resume ()
    {
        pausePanelUI.SetActive(false);
        PauseStatus = false;
        //UnityEngine.SceneManagement.SceneManager.LoadScene(Indestructable.instance.prevScene);
    }

    public void SaveFile()
    {
        SaveSystemManager.NewGame("Test");
        SaveSystemManager.AdvanceLevel();
        SaveSystemManager.SaveLevel(Indestructable.instance.CurrentLevel, new HouseClass(new Vector3(0,0,0), new Vector3(0,0,0)));
        SaveSystemManager.Save();
    }

    public void LoadFile()
    {
        SaveSystemManager.LoadLevel(Indestructable.instance.CurrentLevel);
        SaveSystemManager.Load();
    }

    public void VolumePanel()
    {
       // UnityEngine.SceneManagement.SceneManager.LoadScene(6);
    }

    public void ControlPanel()
    {
      //  UnityEngine.SceneManagement.SceneManager.LoadScene(7);
    }


    public void BackToMenu()
    {
      //  UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void BackToPausePanel()
    {
        pausePanelUI.SetActive(true);
    }
}
