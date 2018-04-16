using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingRemover : MonoBehaviour {
    Canvas info;
    Light light;

    BuildingManager bm;
    private void Start()
    { 
        light = this.GetComponent<Light>();
        light.intensity = 0.0f;
        bm = FindObjectOfType<BuildingManager>();
    }

    private void OnMouseUp()
    {
        //if(!info.isActiveAndEnabled)
        //{
        //    info.gameObject.SetActive(true);
        //    light.intensity = 1000f;
        //}
        //else
        //{
        //    info.gameObject.SetActive(false);
        //    light.intensity = 0f;
        //}

        bm.Select(this.gameObject);
        light.intensity = 100f; //When building has been clicked, select this building and set its light to on so the player can see which building is currently selected.


    }
  
    void DestroyBuilding()
    {
        Destroy(transform.gameObject);
    }
}
