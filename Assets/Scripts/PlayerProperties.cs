using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour {
    public int money;
    public int score;
    public int daysSurvived = 0;
    public int killCount = 0;
    public int totalMoney = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       
	}

    public void UpdateStats(int moneyEarned, int scoreEarned, int addedKillCount) //Update player's stats for the current game
    {
        money += moneyEarned; //add money and score earned from the enemy kill
        score += scoreEarned;
        killCount += addedKillCount; //increase the kill count
        totalMoney += moneyEarned;   //increase total money for the current game by the money earned from the kill
        switch (killCount) //Unlock achievements at different kill count numbers
        {
            case 1:
                achievementManager.Instance.EarnAchievement("System Error!");
                break;
            case 10:
                achievementManager.Instance.EarnAchievement("System Crash!");
                break;
            case 50:
                achievementManager.Instance.EarnAchievement("Blue Screen of Death!");
                break;
            case 100:
                achievementManager.Instance.EarnAchievement("System32 Deleted!");
                break;

        }

        if (money >= 500) //Unlock achievements at different money values
        {
            achievementManager.Instance.EarnAchievement("Pocket Money");
        }
        else if (money >= 1000)
        {
            achievementManager.Instance.EarnAchievement("Savings Account");
        }
        else if (money >= 2500)
        {
            achievementManager.Instance.EarnAchievement("Retirement Fund");
        }
        else if (money >= 5000)
        {
            achievementManager.Instance.EarnAchievement("Money Bags!");
        }
        else if (money >= 10000)
        {
            achievementManager.Instance.EarnAchievement("Rothschild?");
        }
        
    }
}
