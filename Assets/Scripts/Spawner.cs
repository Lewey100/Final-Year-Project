using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject[] enemies;
    public Vector3 spawnValues;
    public float spawnWait;
    public float spawnMostWait;
    public float spawnMinWait;
    public bool stop;
    public int maxEnemies;
    int enemiesSpawned;
    Sun sun;
    int randEnemy;
    bool coroutinerunning;
    public int startWait;
    Coroutine c;
    // Use this for initialization
    void Start () {
        sun = GameObject.Find("SunLightCycle").GetComponent<Sun>();
        enemiesSpawned = 0;
      //  StartCoroutine(waitSpawner());
	}
	
	// Update is called once per frame
	void Update () {
        maxEnemies = sun.numberOfDays; //Set max enemies to how many days have passed.
        spawnWait = Random.Range(spawnMinWait, spawnMostWait); //Spawn enemies at random intervals
        
        if(sun.isNight() && !coroutinerunning) //If it is night time and enemies haven't started spawning yet
        {
            c = StartCoroutine(waitSpawner()); //Start spawning enemies
     
        }
        if(!sun.isNight() && coroutinerunning) //If it is day time and enemies are spawning
        {
            enemiesSpawned = 0; //reset enemies spawned value for a new day
            StopCoroutine(c); //Stop spawning enemies
            coroutinerunning = false;
        }
       
        
	}

    IEnumerator waitSpawner()
    {
        coroutinerunning = true;
        yield return new WaitForSeconds(startWait); //wait until the set amount of time has passed before spawning enemies
        
        while (!stop)
        {
           
            if (enemiesSpawned != maxEnemies) //If the max number of enemies has not been reached yet
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), 0, Random.Range(-spawnValues.z, spawnValues.z)); //Spawn enemies at random positions
                Instantiate(enemies[0], spawnPosition + transform.TransformPoint(0, 0, 0), gameObject.transform.rotation);
                enemiesSpawned++; //Increase amount of enemies spawned for that day

            }
            yield return new WaitForSeconds(spawnWait); //Wait for a random time before spawning another enemy.
        }
       // coroutinerunning = false;
    }
}
