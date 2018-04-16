using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSpawner : MonoBehaviour {
    Sun sun;
   List <GameObject> spawners;
	// Use this for initialization
	void Start () {
        sun = FindObjectOfType<Sun>();
        spawners = new List<GameObject>();
        spawners.AddRange(GameObject.FindGameObjectsWithTag("Spawner"));

        foreach (GameObject go in spawners)
        {
            go.SetActive(false); //Turn all spawners off to begin with
        }
	}
	
	// Update is called once per frame
	void Update () {
        
		if(sun.timer == 0.0f) //At the start of a new day
        {
            if(sun.numberOfDays == 1 || sun.numberOfDays == 5 || sun.numberOfDays == 10 || sun.numberOfDays == 15) //At intervals in the days
            {
                int i = Random.Range(0, spawners.Count); //Activate a random spawner
                spawners[i].SetActive(true);
                Debug.Log("SpawnerActive");
                spawners.Remove(spawners[i]); //Remove it from the list so it can't be selected and attempted to be activated again.
               
            }
        }
	}
}
