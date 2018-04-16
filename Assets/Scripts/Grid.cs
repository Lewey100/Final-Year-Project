using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
    public GameObject plane;
    public int width = 10;
    public int height = 10;


    private GameObject[,] grid = new GameObject[100,100]; //OLD GRID SCRIPT NOT USED
	// Use this for initialization
	void Start () {
		for (int x = 0; x < width; x++)
        {
            for(int z = 0; z < height; z++)
            {
                GameObject gridPlane = (GameObject)Instantiate(plane);
                gridPlane.transform.parent = gameObject.transform;
                gridPlane.name = "(" + x + "," + z + ")";
                gridPlane.transform.position = new Vector3(gridPlane.transform.position.x + x, gridPlane.transform.position.y, gridPlane.transform.position.z + z);
               
                grid[x, z] = gridPlane;
                
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
