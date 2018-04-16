using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement{

    private string name;

    private string description;

    private bool unlocked;

    private int points;

    private int spriteIndex;

    private GameObject achievementRef;
    //Getter and Setter functions of Achievement
    public string Name
    {
        get{ return name;}
        set{name = value;}
    }
    public string Description 
    {
        get { return description; }
        set { description = value; }
    }
    public bool Unlocked
    {
        get { return unlocked; }
        set { unlocked = value; }
    }
    public int Points
    {
        get { return points; }
        set { points = value; }
    }
    public int SpriteIndex
    {
        get { return spriteIndex; }
        set { spriteIndex = value; }
    }
    public GameObject AchievementRef
    {
        get { return achievementRef; }
        set { achievementRef = value; }
    }

    public Achievement(string name, string description, int points, int spriteIndex, GameObject achievementRef) //Constructor for an achievement
    {
        this.Name = name; //Title of the achievement
        this.description = description; //Description of achievement
        this.unlocked = false; //Not unlocked as default
        this.points = points; //How much the achievement is worth
        this.spriteIndex = spriteIndex; //Where the sprite is found in the array
        this.achievementRef = achievementRef; //Points to the achivement prefab which the achievement is being instantiated as

        LoadAchievement(); //Loads the achievement to see if it has already been unlocked. This is useful for people who play more than once.

    }
	// Use this for initialization
	void Start () {
       

	}

    public bool EarnAchievement()
    {
        if(!unlocked) //If the achievement has not been unlocked
        {
            //Debug.Log("unlocked");
            // achievementRef.GetComponent<Image>().sprite = achievementManager.Instance.unlockedSprite;
            achievementRef.GetComponent<Image>().color = new Color(0,0,0,1); //Set the background of the achievement to black
            SaveAchievement(true); //Save the achievement's updated data.
            return true;
        }
        else //If achievement has already been earned
        {
            return false;
        }
    }

    public void SaveAchievement(bool value)
    {
        unlocked = value;

        int tempPoints = PlayerPrefs.GetInt("AchievementPoints");

        PlayerPrefs.SetInt("AchievementPoints", tempPoints += points); //Updates the player's achievement points value

        PlayerPrefs.SetInt(name, value ? 1 : 0); //Sets the achievement to be unlocked or not.

        PlayerPrefs.Save(); //Saves the player's data to the device.
    }
	
    public void LoadAchievement()
    {
        unlocked = PlayerPrefs.GetInt(name) == 1 ? true : false; //Finds if the player has unlocked the achievement or not

        if (unlocked) //If they have, set the achievement background and add the points to the players overall points.
        {
            achievementManager.Instance.textPoints.text = "Points: " + PlayerPrefs.GetInt("AchievementPoints");
            achievementRef.GetComponent<Image>().color = new Color(0, 0, 0, 1);
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
