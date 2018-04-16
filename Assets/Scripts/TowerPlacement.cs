using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacement : MonoBehaviour
{
    void OnMouseUp()
    {
        PlayerProperties pp = GameObject.FindObjectOfType<PlayerProperties>();
        BuildingManager bm = GameObject.FindObjectOfType<BuildingManager>();
        BuildingProperties bp = GameObject.FindObjectOfType<BuildingProperties>();
        if ((bm.selectedTower != null) && (transform.childCount == 0)) //If a building is selected and there isn't a building placed on this square
        {
            if (pp.money >= bm.selectedTower.GetComponent<BuildingProperties>().cost) //If the player has enough money
            {
                GameObject b = Instantiate(bm.selectedTower, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), transform.rotation, transform);
                bm.AddBuilding(b); //Instantiate the building and add it to the building list
                if (bm.selectedTower != null)
                {
                    pp.money -= bm.selectedTower.GetComponent<BuildingProperties>().cost; //Remove the cost from the player's money
                }
                }
        }
    }
}
