using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingProperties : MonoBehaviour {
    public int cost;
    public int health;
    public int upgradeCost;
    public int damage;
    public int range;
    public int fireSpeed;
    public int sellPrice;

    PlayerProperties pp;
	// Use this for initialization
	void Start () {
        pp = FindObjectOfType<PlayerProperties>();
	}
	
	// Update is called once per frame
	void Update () {
		if(health <= 0)
        {
            //Debug.Log("Destroyed");
            Destroy(transform.gameObject); //If building's health is zero or less, destroy the tower
        }
	}

    public void Upgrade()
    {
        if (pp.money >= this.upgradeCost) //If the player has enough money
        {
            pp.money -= this.upgradeCost; //Remove the upgrade cost from the player's money
            this.health = this.health * 2; //Double the towers health, damage, sell and upgrade costs. Add 2 to the range and add 1 to fireSpeed.
            this.damage = this.damage * 2;
            this.sellPrice = this.sellPrice * 2;
            this.range += 2;
            this.fireSpeed++;
            this.upgradeCost = this.upgradeCost * 2;
            achievementManager.Instance.EarnAchievement("Making Upgrades!"); //Unlock an achievement
        }
    }

    public void Sell()
    {
        pp.money += sellPrice; //Add the sell price to the player's money
        Destroy(transform.gameObject); //Destroy the sold tower
        achievementManager.Instance.EarnAchievement("Spare Change?"); //Unlock an achievement
    }
}
