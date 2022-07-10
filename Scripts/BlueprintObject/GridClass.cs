using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridClass
{

    GameObject parentObj;

    private int width;
    private int height;
    private int length;
    public float cellSize;
    private float maxDim;
    public float GridWidth;
    public float GridHeight;
    public float GridLength;

    public float zoom;

    public float MaxNegative = -1;

    public bool Lines; //true = active false = destroyed

    int number;

    int maxDimThin;
    float maxNegativeThin;

    //public List<GameObject> LineList = new List<GameObject>();


    public GridClass(int MaxDim, float cellSize, GameObject parent)
    {
        this.width = MaxDim;
        this.height = MaxDim;
        this.length = MaxDim;
        this.cellSize = cellSize;
        this.maxDim = (float)MaxDim;
        this.GridWidth = width * cellSize;
        this.GridHeight = height * cellSize;
        this.GridLength = length * cellSize;
        this.MaxNegative = -1 * this.maxDim;

        this.parentObj = parent;

        maxDimThin = (int)maxDim / 10;
        maxNegativeThin = maxDimThin*-1;

        Lines = true;

        DrawGrid();
    }

    private void DrawGrid()
    {
        number = 0;
        //Debug.Log(cellSize);
        for (float i = MaxNegative; i < maxDim; i++)
        {
            if ((i % 10) == 0)
            {
                if (i >= 0)
                {
                    //Draw the 1 meter lines
                    DrawLine1M(WorldPos(i, 0, 0), WorldPos(i, maxDim, 0), Color.magenta, parentObj);
                    DrawLine1M(WorldPos(0, i, 0), WorldPos(maxDim, i, 0), Color.magenta, parentObj);

                    //Z Axis Wall
                    DrawLine1M(WorldPos(0, 0, i), WorldPos(0, maxDim, i), Color.magenta, parentObj);
                    DrawLine1M(WorldPos(0, i, 0), WorldPos(0, i, maxDim), Color.magenta, parentObj);


                    //Y Axis Wall
                    DrawLine1M(WorldPos(i, 0, 0), WorldPos(i, 0, maxDim), Color.magenta, parentObj);
                    DrawLine1M(WorldPos(0, 0, i), WorldPos(maxDim, 0, i), Color.magenta, parentObj);
                }
            }
            else
            {

                if (i > 200)
                {

                } else
                {
                    if (i >= 0)
                    {
                        //X Axis Wall
                        DrawLine(WorldPos(i, 0, 0), WorldPos(i, maxDimThin, 0), Color.red, parentObj);
                        DrawLine(WorldPos(0, i, 0), WorldPos(maxDimThin, i, 0), Color.red, parentObj);

                        //Z Axis Wall
                        DrawLine(WorldPos(0, 0, i), WorldPos(0, maxDimThin, i), Color.blue, parentObj);
                        DrawLine(WorldPos(0, i, 0), WorldPos(0, i, maxDimThin), Color.blue, parentObj);


                        //Y Axis Wall
                        DrawLine(WorldPos(i, 0, 0), WorldPos(i, 0, maxDimThin), Color.green, parentObj);
                        DrawLine(WorldPos(0, 0, i), WorldPos(maxDimThin, 0, i), Color.green, parentObj);
                    }
                   
                }
                         
              
            }




            /*
            DrawLine(WorldPos(i * maxDim, 0, 0), WorldPos(i * maxDim, maxDim, 0), Color.blue);
            DrawLine(WorldPos(0, 0, i * maxDim), WorldPos(0, maxDim, i * maxDim), Color.blue);


            DrawLine(WorldPos(0, i * maxDim, 0), WorldPos(0, i * maxDim, maxDim), Color.blue);
            DrawLine(WorldPos(0, i * maxDim, 0), WorldPos(maxDim, i * maxDim, 0), Color.blue);


            DrawLine(WorldPos(i * maxDim, 0, 0), WorldPos(i * maxDim, 0, maxDim), Color.blue);
            DrawLine(WorldPos(0, 0, i * maxDim), WorldPos(maxDim, 0, i * maxDim), Color.blue);
            */
        }




    }


    private Vector3 WorldPos(float x, float y, float z)
    {
        return new Vector3(x, y, z) * cellSize;
    }

    public static TextMesh WorldText(string text, Transform parent, Vector3 localPos, int fontsize)
    {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        gameObject.transform.parent = parent;
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPos;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.text = text;
        textMesh.fontSize = fontsize;
        return textMesh;
    }

    public static GameObject WorldBlock(string text, Transform parent, Vector3 localPos, int fontsize, GameObject gO)
    {
        GameObject gameObject = gO;
        gameObject.transform.parent = parent;
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPos;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.text = text;
        textMesh.fontSize = fontsize;
        return gameObject;
    }


    public void DrawLine(Vector3 start, Vector3 end, Color color, GameObject parent)
    {
        GameObject line = new GameObject("ThinLine" + number );
        line.transform.localPosition = start;
        line.AddComponent<LineRenderer>();
        line.transform.parent = parent.transform;
        LineRenderer lr = line.GetComponent<LineRenderer>();
        lr.material.color = color;
        lr.startWidth = 0.02f;
        lr.endWidth = 0.02f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        number++;

    }

    public void DrawLine1M(Vector3 start, Vector3 end, Color color, GameObject parent)
    {
        GameObject line = new GameObject("ThickLine");
        line.transform.localPosition = start;
        line.AddComponent<LineRenderer>();
        line.transform.parent = parent.transform;
        LineRenderer lr = line.GetComponent<LineRenderer>();
        lr.material.color = color;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);

    }
   

}

