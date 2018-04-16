using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour{
    public enum AIStates { NEAREST, FARTHEST, WEAKEST, STRONGEST };
    public AIStates AIState = AIStates.NEAREST;

    TurretMovement movement;
    EnemyLocator locator;
    RocketTowerSpin rocketTower;
    void Start()
    {
        if (this.transform.parent.name == "RocketTower(Clone)")
        {
            rocketTower = GetComponent<RocketTowerSpin>();
        }
        else
        {
            movement = GetComponent<TurretMovement>();
        }
        locator = GetComponent<EnemyLocator>();
    }

    void Update()
    {
        

        switch(AIState)
        {
            case AIStates.NEAREST: //Enum switch statement for different firing priority. Currently only nearest and farthest are functional.
                TargetNearest();
                break;
            case AIStates.FARTHEST:
                TargetFarthest();
                break;
            case AIStates.WEAKEST:

                break;
            case AIStates.STRONGEST:

                break;
            
        }
    }

    void TargetNearest()
    {
        List<GameObject> validTargets = locator.GetTargets(); //List of targets from the enemy locator

        GameObject curTarget = null;
        float closestDist = 0.0f;

        for (int i = 0; i < validTargets.Count; ++i)
        {
            if (validTargets[i] == null || validTargets[i].tag != "Enemy") //If enemy is destroyed, remove it from list
            {
                validTargets.RemoveAt(i);
                --i;
                continue;
            }
            float dist = Vector3.Distance(transform.position, validTargets[i].transform.position);
            if (!curTarget || dist < closestDist) //If the distance between the turret and the target is less than the closest distance
            {
                curTarget = validTargets[i];
                closestDist = dist; //Set the target to that enemy and reduce the closest distance
            }
        }

        if (this.transform.parent.name == "RocketTower(Clone)")
        {
          //  Debug.Log("AssignedTarget");
            rocketTower.SetTarget(curTarget);
        }
        else
        {
            movement.SetTarget(curTarget); //Return the closest target to the turret movement script
        }
    }

    void TargetFarthest()
    {
        List<GameObject> validTargets = locator.GetTargets();

        GameObject curTarget = null;
        float farthestDist = 0.0f;

        for (int i = 0; i < validTargets.Count; ++i)
        {
            float dist = Vector3.Distance(transform.position, validTargets[i].transform.position);
            if (!curTarget || dist > farthestDist)
            {
                curTarget = validTargets[i];
                farthestDist = dist;
            }
        }

        //if (this.transform.parent.name == "RocketTower(Clone)")
        //{
        //    Debug.Log("AssignedTarget");
        //    rocketTower.SetTarget(curTarget);
        //}
        rocketTower.SetTarget(curTarget);
        movement.SetTarget(curTarget);
    }
}
