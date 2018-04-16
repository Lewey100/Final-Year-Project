using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class displayScore : MonoBehaviour {
    Text money;
    
    // Use this for initialization
    void Start () {
        money = transform.GetComponent<Text>();
        
	}
	
	// Update is called once per frame
	void Update () {
        PlayerProperties pp = FindObjectOfType<PlayerProperties>();
        money.text = pp.money.ToString(); //Updates the money data on the UI
	}
}
