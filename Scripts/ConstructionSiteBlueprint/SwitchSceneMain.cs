using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSceneMain : MonoBehaviour
{
    void Start() {
        Indestructable.instance.prevScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        Indestructable.instance.CurrentLevel = 1;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
           // UnityEngine.SceneManagement.SceneManager.LoadScene(6);
            // above is for test only, the one below is the correct screen flow
            // UnityEngine.SceneManagement.SceneManager.LoadScene(10);
        }
    }
}
