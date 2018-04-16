using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMovement : MonoBehaviour
{
    public float TurnSpeed = 3f;
    public GameObject target = null;
    Vector3 prevPos = Vector3.zero;
    Quaternion lookAtTurret;
    Quaternion lookAtGun;
    Transform turretPivot;
    Transform gunPivot;
    Transform turretGunRay;
    float rateOfFire = 2f;
    bool isManual;
    string nameOfLayer = "Base";
    LayerMask layer;

    void Start()
    {
        if(transform.parent.name.Contains("Manual"))
        {
            isManual = true;
        }
        else
        {
            isManual = false;
        }
        turretPivot = transform.GetChild(0); //hierarchy of the turret, off of the base
        gunPivot = transform.GetChild(0).GetChild(0).GetChild(0); //hierarchy of the gun object off it's base
        turretGunRay = gunPivot.GetChild(0).GetChild(1).transform.GetChild(1); //gun object in hierarchy 
        layer = ~(1 << LayerMask.NameToLayer(nameOfLayer)); //layerMask to allow ray to pass through other buildings
    }

    void Update()
    {
        
        if (target)
        {
            if (prevPos != target.transform.position) //only move if target has moved
            {
                prevPos = target.transform.position;
                lookAtTurret = Quaternion.LookRotation(prevPos - turretPivot.position); //rotation of the turret towards target
                lookAtGun = Quaternion.LookRotation(prevPos - gunPivot.position); //rotation of the gun towards target
            }

            if (turretPivot.rotation != lookAtTurret)
            {
                turretPivot.rotation = Quaternion.RotateTowards(turretPivot.rotation, lookAtTurret, TurnSpeed * Time.deltaTime); //rotate to target
                turretPivot.eulerAngles = new Vector3(0, turretPivot.eulerAngles.y, 0); //Set euler angles to avoid gimbal lock
                Debug.DrawRay(turretPivot.position, turretPivot.forward, Color.blue); //Show the ray
            }

            if (gunPivot.rotation != lookAtGun)
            {
                gunPivot.rotation = Quaternion.RotateTowards(gunPivot.rotation, lookAtGun, TurnSpeed * Time.deltaTime); //rotate gun to target
                float clampedZ = Mathf.Clamp(gunPivot.eulerAngles.z, -30, 30); //clamp the z rotation of the gun so it can't aim directly up or down
                gunPivot.eulerAngles = new Vector3(0, gunPivot.parent.eulerAngles.y, clampedZ); //set euler angles to avoid gimbal
                Debug.DrawRay(gunPivot.position, gunPivot.forward, Color.blue);
            }
            
        }
        if (!isManual)
        {
            Fire();
        }
        else if(isManual && Input.GetMouseButton(0))
        {
            Fire();
        }
        rateOfFire += Time.deltaTime;
    }

    public void Fire()
    {
        RaycastHit hit;
        BuildingProperties bp = GetComponentInParent<BuildingProperties>();
        Debug.DrawRay(turretGunRay.position, gunPivot.TransformDirection(Vector3.left) * bp.range, Color.magenta);
        if (rateOfFire > bp.fireSpeed)
        {
            rateOfFire = 0;

            if (Physics.Raycast(turretGunRay.position, gunPivot.TransformDirection(Vector3.left), out hit, bp.range, layer)) //Create a ray from the gun in the direction it is facing.
            {


                if (hit.transform.tag == "Enemy" && target) //If it hit's an enemy
                {
                    var enemyProperties = hit.transform.GetComponent<EnemyProperties>();
                  
                    enemyProperties.health -= bp.damage; //Damage the enemy
                   if(enemyProperties.health <= 0) //If enemy is dead
                    {
                        
                        target.tag = "Untagged"; //Set target to null to find a new target.
                        target = null;
                    }
                }
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
