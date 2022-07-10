using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationAssetList 
{
    // Start is called before the first frame update
   
    public static GameObject chair ()
    {
        GameObject chair = Resources.Load("Models/Furniture/Prop_chair_b1") as GameObject;
        chair.name = "chair";
        chair.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        chair.layer = 11;
        //chair.AddComponent<BoxCollider>();
        return chair;
    }

    public static GameObject plant()
    {
        GameObject plant = Resources.Load("Models/Furniture/Prop_sci_plant_a2") as GameObject;
        plant.name = "plant";
        plant.transform.localScale = new Vector3(1,1,1);
        plant.layer = 11;
        //plant.AddComponent<BoxCollider>();
        return plant;
    }

    public static GameObject closet()
    {
        GameObject closet = Resources.Load("Models/Furniture/Prop_closet_sci0b1") as GameObject;
        closet.name = "closet";
        closet.transform.localScale = new Vector3(1,1,1);
        closet.layer = 11;
        //closet.AddComponent<BoxCollider>();
        return closet;
    }

    public static GameObject sTable()
    {
        GameObject sTable = Resources.Load("Models/Furniture/Prop_SC_Table1") as GameObject;
        sTable.name = "sTable";
        sTable.transform.localScale = new Vector3(2f, 1f, 2f);
        sTable.layer = 11;
       // sTable.AddComponent<BoxCollider>();
        return sTable;
    }

    public static GameObject desk()
    {
        GameObject desk = Resources.Load("Models/Furniture/Prop_sci_desk1") as GameObject;
        desk.name = "desk";
        desk.transform.localScale = new Vector3(1,1,1);
        desk.layer = 11;
       // desk.AddComponent<BoxCollider>();
        return desk;
    }

    public static GameObject stand()
    {
        GameObject stand = Resources.Load("Models/Furniture/Prop_SC_Table") as GameObject;
        stand.name = "stand";
        stand.transform.localScale = new Vector3(1,1,1);
        stand.layer = 11;
       // stand.AddComponent<BoxCollider>();
        return stand;
    }

    public static GameObject sofa()
    {
        GameObject sofa = Resources.Load("Models/Furniture/Prop_sofa_a1") as GameObject;
        sofa.name = "sofa";
        sofa.transform.localScale = new Vector3(1,1,1);
        sofa.layer = 11;
       // sofa.AddComponent<BoxCollider>();
        return sofa;
    }

    public static GameObject carpet()
    {
        GameObject sofa = Resources.Load("Models/Furniture/Prop_TD_1") as GameObject;
        sofa.name = "sofa";
        sofa.transform.localScale = new Vector3(1, 1, 1);
        sofa.layer = 11;
       // sofa.AddComponent<BoxCollider>();
        return sofa;
    }

}
