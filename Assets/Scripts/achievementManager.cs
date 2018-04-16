using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class achievementManager : MonoBehaviour {

    public GameObject achievementPrefab;
    public Sprite[] sprites;

    public AchievementButton activeButton;
    public ScrollRect scrollRect;

    public GameObject visualAchievement;


    public Dictionary<string, Achievement> achievements = new Dictionary<string, Achievement>();

    public Sprite unlockedSprite;

    public Text textPoints;

    private static achievementManager instance;

    public static achievementManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<achievementManager>();
            }
            return achievementManager.instance;
        }
    }

    GameController gc;

    // Use this for initialization
    void Start () {

        //PlayerPrefs.DeleteAll();
        gc = FindObjectOfType<GameController>();
      

        activeButton = GameObject.Find("GeneralBtn").GetComponent<AchievementButton>();



        CreateAchievement("General", "Mission Started!", "You placed your satellite in hopes of rescue, will you make it?", 10, 0);
        CreateAchievement("General", "Making Upgrades!", "You're quite the engineer, Soldier! You upgraded your first turret!", 10, 0);
        CreateAchievement("General", "Spare Change?", "I see you're struggling to upkeep your defence? You sold your first turret!", 10, 0);
        CreateAchievement("Kills", "System Error!", "You sent you first scrap of metal back to the Junkyard. Good work, Soldier!", 10, 0);
        CreateAchievement("Kills", "System Crash!", "10 enemies defeated! You're really getting the hang of these Metalheads, huh?", 25, 1);
        CreateAchievement("Kills", "Blue Screen of Death!", "50 enemies down! When will you let up? You're a machine!", 50, 2);
        CreateAchievement("Kills", "System32 Deleted!", "100 enemies defeated! You're wiping their systems clean!", 100, 3);
        CreateAchievement("Money", "Pocket Money", "I see you're saving up for a big upgrade! Have $500 saved in your war effort.", 10, 0);
        CreateAchievement("Money", "Savings Account", "What are you planning on buying, Soldier!? Have $1000 saved in your account.", 25, 0);
        CreateAchievement("Money", "Retirement Fund", "We won't make it to retirement if you don't start spending that money! Have $2500 in your account.", 50, 0);
        CreateAchievement("Money", "Money Bags!", "Now you're just showing off your saving skills! Have $5000 saved in your account", 100, 0);
        CreateAchievement("Money", "Rothschild?", "How have you managed to penny pinch during this mechanical onslaught!? Have $10,000 saved in your account.", 500, 0);



        foreach(GameObject achievementList in GameObject.FindGameObjectsWithTag("AchievementList"))
        {
            achievementList.SetActive(false);
        }

        activeButton.Click();

        gc.toggleAchievementMenu();
    }

    public void EarnAchievement(string title)
    {
        if (achievements[title].EarnAchievement())
        {
            GameObject achievement = (GameObject)Instantiate(visualAchievement);
            SetAchievementInfo("EarnCanvas", achievement, title);

            textPoints.text = "Points: " + PlayerPrefs.GetInt("AchievementPoints");
            StartCoroutine(HideAchievement(achievement));
        }
    }

    public IEnumerator HideAchievement(GameObject achievement)
    {
        yield return new WaitForSeconds(5);
        Destroy(achievement);
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.W))
        {
            EarnAchievement("System Error!");
        }
	}

    public void CreateAchievement(string category, string title, string description, int points, int spriteIndex)
    {
        GameObject achievement = (GameObject)Instantiate(achievementPrefab); //Instantiate achievement prefab

        Achievement newAchievement = new Achievement(title, description, points, spriteIndex, achievement); //Create a new achievement object, referenced to prefab created
        achievements.Add(title, newAchievement); //Add achievement to dictionary

        SetAchievementInfo(category, achievement, title); //Set the achievement info
    }

    public void SetAchievementInfo(string parent, GameObject achievement, string title)
    {
        achievement.transform.SetParent(GameObject.Find(parent).transform, false); //sets the parent to be within the correct category mask

        //Set the achievement prefab's title, description, points and image
        achievement.transform.GetChild(0).GetComponent<Text>().text = title;
        achievement.transform.GetChild(1).GetComponent<Text>().text = achievements[title].Description;
        achievement.transform.GetChild(2).GetComponent<Text>().text = achievements[title].Points.ToString();
        achievement.transform.GetChild(3).GetComponent<Image>().sprite = sprites[achievements[title].SpriteIndex];
    }

    public void ChangeCategory(GameObject button)
    {
        AchievementButton achievementButton = button.GetComponent<AchievementButton>();

        scrollRect.content = achievementButton.achievementList.GetComponent<RectTransform>(); //Sets the current scrollRect to show the clicked category

        achievementButton.Click();
        activeButton.Click();
        activeButton = achievementButton; //Sets the activeCategory so the game knows when another button is clicked, it should change category.
    }
}
