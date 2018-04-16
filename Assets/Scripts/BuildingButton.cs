using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour {
    public Button Button;
    public Text NameLabel;
    public Text CostLabel;
    public Image IconImage;
    public GameObject BuildingType;
    public GameObject Preview;

    ItemData itemData;
    BuildingItemList itemList;

	void Start ()
    {
        Button.onClick.AddListener(HandleClick); //Adds a listener for onClick events, calls HandleClick when pressed
	}

    public void Setup(ItemData currItem, BuildingItemList currList)
    {
        itemData = currItem; //Gets the current building's properties
        NameLabel.text = itemData.ItemName; //Sets the UI info up.
        CostLabel.text = "Cost: " + itemData.Cost.ToString();
        IconImage.sprite = itemData.Icon;
        itemList = currList;
        BuildingType = itemData.LinkedBuilding; //Sets the building type that this button is linked to
        Preview = Instantiate(itemData.DisplayBuilding, this.transform); //Instantiates a preview of the building in the building selection bar
        Preview.transform.localScale = new Vector3(40, 40, 40);
        Preview.transform.localEulerAngles = new Vector3(-30, 30, 0); //Transforms the building preview so the user can see what is being clicked
        Preview.transform.localPosition = Vector3.zero;
        Preview.transform.localPosition = new Vector3(0, -11, -22);
        Preview.tag = ("Untagged"); //Sets the tag so the the building previews don't interfere with other code, like the Robot pathfinding AI

    }

    public void HandleClick()
    {
        GameObject.Find("Building Manager").GetComponent<BuildingManager>().SelectTowerType(BuildingType); //Selects the building prefab from the building manager
    }
}
