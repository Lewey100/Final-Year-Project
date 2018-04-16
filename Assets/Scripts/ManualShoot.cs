using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualShoot : MonoBehaviour
{
    private Vector3 screenPos;
    Camera cam;
    string mouseTarget = "Mouse Target";
    TurretMovement movement;

    void Start()
    {
        cam = FindObjectOfType<Camera>();
        movement = transform.gameObject.GetComponent<TurretMovement>();
    }

    void Update()
    {  
        if (Input.GetMouseButton(0)) //If click
        {
            if (GameObject.Find(mouseTarget))
            {
                DestroyImmediate(GameObject.Find(mouseTarget));
            }

            Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition); //Ray from camera

            Plane groundPlane = new Plane(Vector3.up, new Vector3(0, 0, 0)); //Plane used for getting position of finger
            float rayLength;

            if (groundPlane.Raycast(cameraRay, out rayLength)) //If the ray from the camera has hit the plane
            {
                Debug.DrawLine(cameraRay.origin, cameraRay.GetPoint(rayLength), Color.red);
                GameObject mousePoint = new GameObject(mouseTarget); 
                mousePoint.transform.position = cameraRay.GetPoint(rayLength); //Set where the point of collision to where ray from the camera hits the plane.
                movement.SetTarget(GameObject.Find(mouseTarget)); //Set target for manual turrets to look at.
            }            
        }
    }
}