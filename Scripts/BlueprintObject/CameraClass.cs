using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraClass
{
    Slider Zoom;
    Light l;

    GameObject obj;
    Camera cam;
    GridClass grid;
    ModObjClass modify;
    float FOV; //60 degree

    public bool persp; //True = perspective
    float Maxdim;

    //View Number
    public int VNum;

    public CameraClass(Camera cam)
    {
        this.cam = cam;
       

        this.persp = false;

        this.Maxdim = 100;
      

        VNum = 1;

        //UpdateCamView();

    }

    public void addExtraInfo ( GridClass grid, Slider zoom, GameObject gameobj, ModObjClass modObj, Light light)
    {
        this.grid = grid;
        this.Zoom = zoom;
        this.obj = gameobj;
        this.modify = modObj;
        this.l = light;
    }





    public void FrontView()
    {
        Vector3 CamVec = new Vector3(0, 0, 1);
        Vector3 Pos = CalcCamPos(VNum);
        Quaternion Rot = new Quaternion(0, 0, 0, 0);
        Rot.eulerAngles = DNAMath.CalcEulerAngleRot(Pos, obj.transform.position, CamVec);
        cam.transform.position = Pos;
        cam.transform.rotation = Rot;
        UpdateLight(Pos);
        //cam.orthographicSize = Maxdim / Zoom.value;
    }

    public void SideView()
    {
        Vector3 CamVec = new Vector3(0, 0, 1);
        Vector3 Pos = CalcCamPos(VNum);
        Quaternion Rot = new Quaternion(0, 0, 0, 0);
        Rot.eulerAngles = DNAMath.CalcEulerAngleRot(Pos, obj.transform.position, CamVec);
        cam.transform.position = Pos;
        cam.transform.rotation = Rot;
        UpdateLight(Pos);
    }

    public void TopdownView()
    {
        // Debug.Log("Top");
        Vector3 CamVec = new Vector3(0, 0, 1);
        Vector3 Pos = CalcCamPos(VNum);
        Quaternion Rot = new Quaternion(0, 0, 0, 0);
        Rot.eulerAngles = DNAMath.CalcEulerAngleRot(Pos, obj.transform.position, CamVec);
        cam.transform.position = Pos;
        cam.transform.rotation = Rot;
       UpdateLight(Pos);
    }


    public void PerspView()
    {
        Vector3 CamVec = new Vector3(0, 0, 1);
        Vector3 Pos = CalcCamPos(VNum);
        Quaternion Rot = new Quaternion(0, 0, 0, 0);
        Rot.eulerAngles = DNAMath.CalcEulerAngleRot(Pos, obj.transform.position, CamVec);
        cam.transform.position = Pos;
        cam.transform.rotation = Rot;
        UpdateLight(Pos);
    }

    Vector3 CalcCenterofObj()
    {
        return obj.transform.position;
    }


    Vector3 CalcCamPos(int viewNum)
    {
        //Determine which view we are using
        //1 = front, 2 = side, 3 = topdown
        Vector3 Center;
        switch (viewNum)
        {
            case 1:
                Center = CalcCenterofObj();
                Center.x = Maxdim;
                return Center;
            case 2:
                Center = CalcCenterofObj();
                Center.z = Maxdim;
                return Center;

            case 3:
                Center = CalcCenterofObj();
                Center.y = Maxdim;
                return Center;

            case 4:
                //Perspective
                Vector3 Pos = DeterminePerspPos() * (Zoom.maxValue / Zoom.value);
                return Pos;
            default:
                Center = CalcCenterofObj();
                Center.z = Maxdim;
                return Center;
        }

    }

    public void UpdateCamView()
    {
        switch (VNum)
        {
            case 1:
                //Forcemake camera ortho
                //VNum = 1;
                cam.orthographic = true;
                FrontView();
                break;
            case 2:
                cam.orthographic = true;
                SideView();
                break;
            case 3:
                cam.orthographic = true;
                TopdownView();
                break;
            case 4:
                //Perspective
                cam.orthographic = false;
                PerspView();
                break;
            default:
                cam.orthographic = true;
                SideView();
                break;
        }
    }


    //
    //Camera Math Be careful to not mess with this code (I worked hard for this)
    //


  



    /*
    Vector3 CalcAngleBetweenVectors(Vector3 vec1, Vector3 vec2)
    {
        Vector3 Angles = new Vector3(0, 0, 0);
        Vector3 up = Vector3.up;

        //Angle between 2 vectors

        Vector3 projUpCam = Projection(up, vec1);
        Vector3 perpCam = PerpendiculartoProjection(vec1, projUpCam); ;


        Vector3 projUpObj = Projection(up, vec2);
        Vector3 perpObj = PerpendiculartoProjection(vec2, projUpObj);


        Angles.x = CalcXRot(perpCam, perpObj, vec1, vec2);
        Angles.y = CalcYRot(perpCam, perpObj, vec2);

        return Angles;
    }
    */


    public void SetCamZoom(float zoom)
    {
        float size = modify.GetMaxVisDim(VNum);

        if (persp)
        {

        }
        else
        {
            cam.orthographicSize = (size / 100) * 1.2f * zoom;
            grid.zoom = zoom;
        }
    }

    Vector3 DeterminePerspPos()
    {
        Vector3 Axis = new Vector3(1, 0.5f, 1);
        Vector3 Pos = Axis * (modify.GetMaxDimension() / 100) * 3 + Vector3.up * ((obj.transform.localScale.y/2)/100);
        return Pos;
    }

    void UpdateLight(Vector3 Pos)
    {
        Vector3 lPos = Pos*5 + Vector3.up * 100;
        l.transform.position = lPos;
        Quaternion Rot = Quaternion.Euler(DNAMath.CalcEulerAngleRot(lPos, obj.transform.position, new Vector3(0, 0, 1)));
        l.transform.rotation = Rot;
    }

}
