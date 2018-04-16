using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class ItemData //Data for each building
{
    public string ItemName; //Name of building
    public Sprite Icon;     //Image of building
    public float Cost = 1f; //Cost of building
    public GameObject LinkedBuilding; //Prefab of the building
    public GameObject DisplayBuilding; //Prefab of the UI Display Object for the building
}

public class BuildingItemList : MonoBehaviour {
    public List<ItemData> ItemList; //Holds the different types of towers
    public GameObject prefab; //The prefab of the building button which will be used to select towers
    public Transform ContentPanel; //Where the buttons will be instantiated

    void Start ()
    {
        RefreshDisplay(); //Repopulates the item list
    }

    public void RefreshDisplay()
    {
        RemoveItems();
        AddItems();
    }

    public void RemoveItems()
    {
        while (ContentPanel.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject); //Destroys all instantiated building buttons
        }
    }

    void AddItems()
    {
        for (int i = 0; i < ItemList.Count; ++i) //Creates building buttons
        {
            GameObject newButton = Instantiate(prefab, ContentPanel) as GameObject;
            BuildingButton buildingButton = newButton.GetComponent<BuildingButton>();
            buildingButton.Setup(ItemList[i], this); //Sets the item data for each building button
        }
    }
}
