using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {
    openDialog oD;
    public GameObject selectedTower;
    public GameObject clickedTower;
    List<GameObject> buildings;
    public GameObject buildingSelection;
    bool CanLose = false;
    bool lost = false;

    private void Start()
    {
        buildingSelection.SetActive(false); //hide building selection bar on start so player can't place a building before the satellite
        oD = FindObjectOfType<openDialog>();
        buildings = new List<GameObject>();
    }

    private void Update()
    {
        CheckBuildingsDestroyed();       
    }

    private void CheckBuildingsDestroyed()
    {
        List<GameObject> toRemove = new List<GameObject>();
        foreach (GameObject b in buildings) //If a building has ben destroyed, set it to be removed from the building list
        {
            if (b == null)
                toRemove.Add(b);
        }

        foreach (GameObject b in toRemove)
        {
            //Debug.Log(toRemove.Count);
            buildings.Remove(b); //Remove from list
        }

        if (!buildings.Contains(GameObject.FindGameObjectWithTag("Satellite")) && CanLose) //If satellite is not present then lose the game
        {
            //Debug.Log("Lose game");
            CanLose = false;
            EventManager.TriggerEvent("Lose Game");
            
        }
       

    }

    public void Select(GameObject buildingTower)
    {
        foreach (GameObject b in buildings)
        {
            if(b.GetComponent<Light>() != null)
            b.GetComponent<Light>().intensity = 0f; //set all tower selection lights to off
         
        }
        clickedTower = buildingTower; //Set the clicked tower to be whatever has been selected
        //Debug.Log(clickedTower.name);
    }

    public void Upgrade()
    {
        clickedTower.GetComponent<BuildingProperties>().Upgrade(); //call upgrade function on selected tower
    }

    public void Sell()
    {
        clickedTower.GetComponent<BuildingProperties>().Sell(); //call sell function on selected tower
    }

    public void SelectTowerType(GameObject prefab)
    {
        selectedTower = prefab; //set which tower is to be built
    }

    public void AddBuilding(GameObject building)
    { if(building.tag == "Satellite") //If the satellite is being added
        {
            oD.satellitePlaced = true; 
            buildingSelection.SetActive(true); //Show building selection bar
            CanLose = true; //Enable the lose possibility
            selectedTower = null; //Unselect the satellite so only one can be placed
            
        }
        buildings.Add(building); //Add building to the building list
    }
}
