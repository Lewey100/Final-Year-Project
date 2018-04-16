using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDialog : MonoBehaviour {
    public bool satellitePlaced = false;
    public GameObject satellite;
    private Rect windowRect = new Rect((Screen.width /3), (Screen.height/ 3f), Screen.width / 3, Screen.height / 5); //Sets window rect using screen width and height. Makes it dynamic for multiple phone screen sizes.
    private bool show = true;
    BuildingManager bm;

    private GUIStyle guiStyle;
    private void OnGUI()
    {
        guiStyle = new GUIStyle(GUI.skin.box);
        guiStyle.fontSize = 30;
        guiStyle.normal.textColor = Color.white;
        if (show)
            windowRect = GUI.Window(0, windowRect, DialogWindow, "Mission Brief", guiStyle); //Create a new GUI window for the entrance dialog
    }

    void DialogWindow(int windowID)
    {
        guiStyle.fontSize = 25;
        guiStyle.wordWrap = true;
        float y = 40;
        GUI.Label(new Rect(5, y, windowRect.width, windowRect.height), "To begin your mission you must first place the satellite to communicate with Earth if we are with any hope of survival.", guiStyle);
        if(GUI.Button(new Rect(5,y + 150, windowRect.width-10,75), "I'm ready, Commander!",guiStyle)) //Create text and button.
        {
            
            show = false; //If button is clicked, hide the GUI
        }
    }
    // Use this for initialization
    void Start () {
        bm = FindObjectOfType<BuildingManager>();
        Time.timeScale = 0f;
        bm.SelectTowerType(satellite); //Set the tower selected to satellite so it is the only tower the player can place to begin with
	}
	
	// Update is called once per frame
	void Update () {
		if(satellitePlaced) //If it is placed, start the game and earn an achievement
        {
            Time.timeScale = 1f;
            achievementManager.Instance.EarnAchievement("Mission Started!");
        }
	}
}
