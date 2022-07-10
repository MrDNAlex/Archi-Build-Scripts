using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModObjClass
{

    public GameObject gameObj;

    public Vector3 iDim;

    public Vector3 Dim;

    public float Width;
    public float Height;
    public float Length;

    public Vector3 SizeReq;

    public ModObjClass(GameObject gameObj,Vector3 req)
    {
        this.gameObj = gameObj;
        this.SizeReq = req;

        iDim = gameObj.transform.localScale;
        Dim = gameObj.transform.localScale;

        gameObj.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    }

    public void UpdateVals()
    {
        Dim = gameObj.transform.localScale;
    }

    public float GetCellSize()
    {
        // return GetSmallestUnit()*2/100;
        return 0.1f * 2; //*2 because of the nature of size units
    }

    public float GetSmallestUnit()
    {
        float num1 = Mathf.Min(iDim.x, iDim.y, iDim.z);
        //float num2 = Mathf.Min(num1, dimZ);
        return num1;
    }

    public float GetBiggestUnit()
    {
        float num1 = Mathf.Min(iDim.x, iDim.y, iDim.z);
        return num1;
    }

    public void SetPositiontoOrigin()
    {
        gameObj.transform.position = AligntoOrigin();
    }

    public Vector3 AligntoOrigin()
    {
        UpdateVals();

        return new Vector3(Dim.x / 100, Dim.y / 100, Dim.z / 100); 
    }

    public void UpdateSizeVals(Vector3 vals)
    {
        Dim = vals;
    }

    public float GetMaxDimension()
    {
        return Mathf.Max(Dim.x, Dim.y, Dim.z);
    }

    public float GetMaxVisDim (int VNum)
    {
        switch (VNum)
        {
            case 1:
                //Front
                return Mathf.Max( Dim.y, Dim.z);
                
            case 2:
                //Side
                return Mathf.Max(Dim.x, Dim.y);
                
            case 3:
                //Top
                return Mathf.Max(Dim.x, Dim.z);
                
            case 4:
                //Persp
                return Mathf.Max(Dim.x, Dim.y, Dim.z);
                
                default:
                return Mathf.Max(Dim.x, Dim.y, Dim.z);
                
        }
    }

    public bool CheckReq ()
    {
        Vector3 dim = new Vector3(Dim.x, Dim.y, Dim.z);

        Debug.Log(SizeReq);
        Debug.Log(dim);

        if (dim == SizeReq)
        {
            Debug.Log("Good");
            return true;
        } else
        {
            Debug.Log("Wrong");
            return false;
        }
    }
}
