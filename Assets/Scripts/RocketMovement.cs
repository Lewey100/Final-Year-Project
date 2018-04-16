using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour {
    Transform targetHeight;
    public GameObject targetEnemy;
    bool hasReachedPeak = false;
    public float damage;
    public GameObject explosion;
	// Use this for initialization
	void Start () {
        targetHeight = GameObject.Find("rocket target").transform;

	}
	
	// Update is called once per frame
	void Update () {

        if (targetEnemy.GetComponent<EnemyProperties>().health <= 0 || targetEnemy == null) //If target is destroyed, delete rocket
        {
            Destroy(this.gameObject);
        }

        if (!hasReachedPeak) //Fly into the sky
        {
            transform.position = Vector3.MoveTowards(transform.position, targetHeight.position, Time.deltaTime * 15f);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetHeight.position), 10f * Time.deltaTime);
            if (transform.position.y >= targetHeight.position.y)
            {
                hasReachedPeak = true;
            }
        }

        else if (hasReachedPeak)//Fly towards target
        {
            Vector3 dir = transform.position - targetEnemy.transform.position;
            Vector3 dir2 = targetEnemy.transform.position + dir.normalized;
            Quaternion lookTo = Quaternion.LookRotation(-dir2);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(-targetHeight.position), Time.deltaTime * 200f);
            transform.position = Vector3.MoveTowards(transform.position, targetEnemy.transform.position, Time.deltaTime * 15f);
        }

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Enemy")
        {
           // Physics.IgnoreCollision(this.GetComponent<CapsuleCollider>(), collision.gameObject.GetComponent<Collider>());
        }

        else
        {
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, 5.0f); //Explosion radius
            foreach(Collider hit in colliders) //For each enemy hit in the explodison radius
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if(rb)
                {
                    if(hit.gameObject.tag == "Enemy")
                    {
                        GameObject boom = Instantiate(explosion, collision.gameObject.transform); //Create exposion prefab
                        var enemyProperties = hit.GetComponent<EnemyProperties>();

                        enemyProperties.health -= damage; //Remove health from enemy
                    }
                }
            }

            Destroy(this.gameObject); //Destroy the rocket when it collides with an enemy
        }
    }

}
