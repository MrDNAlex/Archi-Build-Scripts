using UnityEngine;
using System.Collections;

public class Indestructable : MonoBehaviour {

    public static Indestructable instance = null;

    public int prevScene;
    public int CurrentLevel = 1;

    void Awake() {
        if( !instance )
            instance = this;
        else {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}