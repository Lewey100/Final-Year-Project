using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockHealthRot : MonoBehaviour {
    Quaternion fixedRot;
	// Use this for initialization
	void Start () {
        fixedRot = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = fixedRot; //Script to always have the enemies' health bar at the same rotation.
	}
}
