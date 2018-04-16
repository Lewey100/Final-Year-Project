using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateValues : MonoBehaviour {
    Text health;
    Text upgradeCost;
    Text sellPrice;
    BuildingProperties bp;
    BuildingManager bm;
	// Use this for initialization
	void Start () {
        health = this.transform.Find("HealthNumber").GetComponent<Text>();
        upgradeCost = transform.Find("UpgradeTower").GetComponentInChildren<Text>();
        sellPrice = transform.Find("SellTower").GetChild(0).GetComponent<Text>();
        bm = FindObjectOfType<BuildingManager>();
        
    }
	
	// Update is called once per frame
	void Update () { //Function checks for whichever tower is clicked and displays the relevant data on the UI buttons for upgrading and selling.
        if(bm.clickedTower != null)
        {
            bp = bm.clickedTower.GetComponent<BuildingProperties>();
        }
        if (bp != null)
        {
            health.text = "HP: " + bp.health.ToString();
            upgradeCost.text = "Upgrade Cost: " + bp.upgradeCost.ToString();
            sellPrice.text = "Sell: " + bp.sellPrice.ToString();
        }
    }
}
