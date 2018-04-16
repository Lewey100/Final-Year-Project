using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour {
    private GameObject targetBuilding;
    Vector3 center;
    float radius;
    EnemyProperties ep;
    float attackSpeed = 2f;
    robotAnimation robAnim;
    // Use this for initialization
    void Start() {
        robAnim = GetComponent<robotAnimation>();
       ep = GetComponent<EnemyProperties>();

       center = GetComponentInChildren<SphereCollider>().center;
       radius = GetComponentInChildren<SphereCollider>().radius;
	}
	
	// Update is called once per frame
	void Update () {
 
        if (ep.health > 0)
        {
            if (targetBuilding == null) //If enemy is alive and doesn't have a target
            {
                robAnim.walkAnimPlay(); //Start walking
                FindClosestEnemy(); //Find a target
            }
            attackSpeed += Time.deltaTime; //Increase attack speed used for cycling attacks
            if (targetBuilding != null)
            {
                moveTowardsClosestTarget(); //If enemy has a target, move towards it.
            }
        }
        else if(ep.health <= 0)
        {
            
            robAnim.deathAnimPlay(); //If enemy has zero or less health, play death animation
            this.GetComponent<CapsuleCollider>().enabled = false; //Deactivate it's attack range
            if (this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("death")) //If death animation had started
            { 
                StartCoroutine(destroyTimer()); //Start coroutine to destroy enemy
            }
        }

	}
   IEnumerator destroyTimer()
    {
        
        yield return new WaitForSeconds(3); //Wait for 3 seconds to let death animation play, then destroy enemy.
        Destroy(this.transform.gameObject);
    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos; //Array of all buildings
        gos = GameObject.FindGameObjectsWithTag("Wall");
        GameObject sat;
        sat = GameObject.FindGameObjectWithTag("Satellite"); //The satellite
        if(gos.Length == 0)
        {
            return null; //Only find a target if there is a building
        }
        targetBuilding = null;
        float distance = Mathf.Infinity; //set distance to max possible value so the first building found will always be less than this
        Vector3 position = transform.position;
        foreach(GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position; //For each building on the map, calculate the distance between the enemy and that.
            float curDistance = diff.sqrMagnitude;
            if(curDistance < distance) //If the distance is less than the previous distance
            {
                //targetBuilding = go; //Set the target building to the new closest distance
                targetBuilding = sat;   //Set target building to always be satellite so the enemies will go straight for it, destroying any buildings in its path
                distance = curDistance; //Set new distance for check to be the current distance
            }
        }
        distance = Mathf.Infinity; //set distance to max possible number
        
        return targetBuilding; //return the closest building
        
    }

    
    void moveTowardsClosestTarget()
    {
        Vector3 dir = transform.position - targetBuilding.transform.position; //Creating direction vector from enemy to the target
        Quaternion lookTo = Quaternion.LookRotation(-dir); //Setting the target rotation for the enemy to look at the building.
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetBuilding.transform.position.x, transform.position.y, targetBuilding.transform.position.z), Time.deltaTime * ep.moveSpeed); //Move towards the target building
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookTo, Time.deltaTime * 500f); //Rotate towards the target building
        
    }


    private void OnCollisionStay(Collision collision)
    {
        if((collision.gameObject.tag == "Wall") || (collision.gameObject.tag == "Satellite")) //If the enemy is hitting a building
        {
            BuildingProperties bp = collision.gameObject.GetComponent<BuildingProperties>();
            if (attackSpeed > ep.attackSpeed) //And the enemy has ran through the attack cycle
            {
                robAnim.attackAnimPlay(); //Play the attack animation
                attackSpeed = 0;           //Reset the attack cycle
                bp.health -= ep.damage;     //Remove health from the hit building equal to the enemy's damage value.
            }
        }
        
    }


}

