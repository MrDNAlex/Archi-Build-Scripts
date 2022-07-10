using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{

    public ModObjClass ObjectMod;

    public Vector3 req;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        this.transform.localScale = new Vector3(ObjectMod.Dim.x, ObjectMod.Dim.y, ObjectMod.Dim.z);

        ObjectMod.SetPositiontoOrigin();
    }

    public void CreateNewModClass()
    {
        ObjectMod = new ModObjClass(this.gameObject, req);
    }

}

