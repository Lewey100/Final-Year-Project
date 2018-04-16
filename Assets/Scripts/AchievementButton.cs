using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementButton : MonoBehaviour
{

    public GameObject achievementList;

    public Sprite neutral, highlight;

    public Image sprite;
    // Use this for initialization
    void Start()
    {
        sprite = GetComponent<Button>().image; //pointer to the achievement buttons image so it can be altered in the script
    }

    // Update is called once per frame
    public void Click()
    {
        if(sprite.sprite == neutral) //If the button is not highlighted and therefore, the category is not selected.
        {
            sprite.sprite = highlight; //Set the button to be highlighted
            achievementList.SetActive(true); //Open the achievement category
        }
        else
        {
            sprite.sprite = neutral; //Otherwise, if a different category is clicked, set it to not be highlighted
            achievementList.SetActive(false); //And hide the achievement category
        }
    }
}
