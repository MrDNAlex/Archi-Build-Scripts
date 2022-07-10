using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{

    [SerializeField]
    private Camera playerCamera;

    private bool buildModeOn = false;
    private bool canBuild = false;

    private BlockSystem bSys;

    [SerializeField]
    private LayerMask buildableSurfacesLayer;

    private Vector3 buildPos;

    private GameObject currentTemplateBlock;

    [SerializeField]
    private GameObject blockTemplatePrefab;
    [SerializeField]
    private GameObject blockPrefab;

    [SerializeField]
    private Material templateMaterial;

    private int blockSelectCounter = 0;

    private bool rotateStatus = false;
    private int rotateCounter = 0;

    private void Start()
    {
        bSys = GetComponent<BlockSystem>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            buildModeOn = !buildModeOn;

            if (buildModeOn)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }

        if (Input.GetKeyDown("r"))
        {
            blockSelectCounter++;
            if (blockSelectCounter >= bSys.allBlocks.Count) blockSelectCounter = 0;
        }

        if (Input.GetKeyDown("q"))
        {
            rotateStatus = !rotateStatus;
            rotateCounter += 1;
        }


        if (buildModeOn)
        {
            RaycastHit buildPosHit;

            if (Physics.Raycast(playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)), out buildPosHit, 10, buildableSurfacesLayer))
            {
                Vector3 point = buildPosHit.point;
                buildPos = new Vector3(Mathf.Round(point.x * 10)/10, Mathf.Round(point.y*10)/10, Mathf.Round(point.z*10)/10);
                canBuild = true;
            }
            else
            {
                Destroy(currentTemplateBlock.gameObject);
                canBuild = false;
            }
        }

        if (!buildModeOn && currentTemplateBlock != null)
        {
            Destroy(currentTemplateBlock.gameObject);
            canBuild = false;
        }

        if (canBuild && currentTemplateBlock == null)
        {
            currentTemplateBlock = Instantiate(blockTemplatePrefab, buildPos, Quaternion.identity);
            currentTemplateBlock.GetComponent<MeshRenderer>().material = templateMaterial;
        }

        if (canBuild && currentTemplateBlock != null)
        {
            
            currentTemplateBlock.transform.position = buildPos;
            

            if (rotateCounter > 0)
            {
            currentTemplateBlock.transform.Rotate(0, 90, 0);
            rotateCounter -= 1;
            }
            currentTemplateBlock.transform.Translate(0,currentTemplateBlock.transform.localScale.y/2,0);
            
            if (Input.GetMouseButtonDown(0))
            {
                PlaceBlock();
            }

            //if (Input.GetMouseButtonDown(1))
            //{
            //    DrestroyBlock();
            //}
        }
    }

    private void PlaceBlock()
    {
        GameObject newBlock = Instantiate(blockPrefab, buildPos, Quaternion.identity);
        Block tempBlock = bSys.allBlocks[blockSelectCounter];
        newBlock.name = tempBlock.blockName;
        newBlock.GetComponent<MeshRenderer>().material = tempBlock.blockMaterial;

        
        if (rotateStatus == true)
        {
            newBlock.transform.Rotate(0, 90, 0);
        }
        
        newBlock.transform.Translate(0,newBlock.transform.localScale.y/2,0);
    }

    //private void GetMouseButtonDown()
    //{
    //    
    //}
}