using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTowerSpin : MonoBehaviour
{
    public float TurnSpeed = 3f;
    public GameObject target = null;

    float rateOfFire = 2f;
    GameObject towerPivot;
    public GameObject projectilePrefab;

    BuildingProperties bp;
    // Use this for initialization
    void Start()
    {
        bp = GetComponentInParent<BuildingProperties>();
        towerPivot = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        towerPivot.transform.Rotate(Vector3.up * (Time.deltaTime * 20f), Space.World); //Rotate the rocket turret around it's why axis.
        if (towerPivot.transform.rotation.y >= 360f)
        {
            towerPivot.transform.rotation = new Quaternion(0, 0, 25, 0); //if over 360, set it back to 0
        }
        if (target) //If tower has a target
        { 
            Fire(); //Shoot a rocket
        }
        rateOfFire += Time.deltaTime; //Increase attack cycle timer
    }

    public void Fire()
    {
        BuildingProperties bp = GetComponentInParent<BuildingProperties>();

        if(rateOfFire > bp.fireSpeed) //If cycle is far enough through
        {
            rateOfFire = 0; //reset cycle

            GameObject rocket = Instantiate(projectilePrefab, towerPivot.transform.GetChild(0).transform.position, Quaternion.Euler(-40, towerPivot.transform.eulerAngles.y + 110, 0)) as GameObject; //Create a rocket
            rocket.GetComponent<RocketMovement>().targetEnemy = target; //Set the rocket's target to the tower's current target
            rocket.GetComponent<RocketMovement>().damage = bp.damage; //Set the damage of the rocket to the tower's damage

            if (target.gameObject.GetComponent<EnemyProperties>().health <= 0) //If target has died
            {

                target.tag = "Untagged"; //Set target to null so the tower can find a new one.
                target = null;
            }
        }
    }

    public void SetTarget(GameObject n_target)
    {
        target = n_target;
    }

    public GameObject GetTarget()
    {
        return target;
    }

}