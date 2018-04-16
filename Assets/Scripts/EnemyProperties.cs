using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyProperties : MonoBehaviour {
    private float startHealth;
    public float health;
    public int moveSpeed;
    public int scoreForKill;
    public int moneyForKill;
    public int damage;
    public int attackSpeed;
    PlayerProperties pp;
    Sun sun;
    bool running = false;
    public Image healthBar;
	// Use this for initialization
	void Start () { //When enemy spawns
        sun = FindObjectOfType<Sun>();
        health = Mathf.RoundToInt(health + (sun.numberOfDays * 10)); //Spawn with base health damage, money and score. Increased based on how many days have passed
        startHealth = health; //Set start health value to be used by health bar
        damage = Mathf.RoundToInt(damage + (sun.numberOfDays * 10));
        moneyForKill = Mathf.RoundToInt(moneyForKill + (sun.numberOfDays * 10));
        scoreForKill = Mathf.RoundToInt(scoreForKill + (sun.numberOfDays *  10));
        
        pp = FindObjectOfType<PlayerProperties>();
    }
	
	// Update is called once per frame
	void Update () {
        healthBar.fillAmount = health / startHealth; //Calculation to show how much of the health bar should be red.
		if(health <= 0 && !running)
        {
            StartCoroutine(addMoneyHealth()); //If enemy has been killed
        }
       
	}

    IEnumerator addMoneyHealth()
    {
        pp.UpdateStats(moneyForKill, scoreForKill, 1); //Add money and score and kill count to player's stats.
        running = true;
        yield return new WaitForSeconds(4); //Wait until death animation so score isn't added exponentially.

    }
}
