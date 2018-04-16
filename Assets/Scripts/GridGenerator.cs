using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{

    public Transform FloorPrefab;
  
    public Vector2 GridSize;


    private void Start()
    {
        if(PlayerPrefs.GetInt("highScore") >= 10 && PlayerPrefs.GetInt("highScore") < 20) //If the player has progressed far enough, increase the size of the grid.
        {
            GridSize.x = 7;
            GridSize.y = 7;
        }
        GenerateGrid();
    }

    private void OnDrawGizmos()
    {
        GridSize.x = Mathf.Clamp(GridSize.x, 0, 20);
        GridSize.y = Mathf.Clamp(GridSize.y, 0, 20); //set a max size of the grid
    }

    public void GenerateGrid()
    {
       
        string holderName = "Generated Grid";
        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject); //If there is already a grid, destroy it.
        }

        Transform gridHolder = new GameObject(holderName).transform;
        gridHolder.parent = transform;



        for (int x = 0; x < GridSize.x; ++x)
        {
            for (int y = 0; y < GridSize.y; ++y)
            {
                Vector3 blockPos = new Vector3(-GridSize.x / 2 + 0.5f + x, 0, -GridSize.y / 2 + 0.5f + y); //Set position of each grid square
                Transform floorBlock = Instantiate(FloorPrefab, blockPos, Quaternion.Euler(Vector3.right * 0)) as Transform; //Instantiate each square
                floorBlock.parent = gridHolder; //Set each square as a child of the grid
            }
        }
    }

}
