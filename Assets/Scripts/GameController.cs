using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    GameObject panel;
    Text currentScore;
    Text highScore;
    private UnityAction loseListener;
    private UnityAction endListener;
    PlayerProperties pp;
    FBScript facebook;
    GameObject achievementCanvas;

    void Awake()
    {
        facebook = FindObjectOfType<FBScript>();
        achievementCanvas = GameObject.Find("AchievementMenu");
        pp = FindObjectOfType<PlayerProperties>();
        panel = GameObject.Find("Panel");
        currentScore = panel.transform.Find("PlayerScore").GetComponent<Text>();
        highScore = panel.transform.Find("HighScore").GetComponent<Text>();
        panel.SetActive(false); //hide the end of game screen
       // achievementCanvas.SetActive(false);
        loseListener = new UnityAction(LoseGame); //Create a new UnityAction to handle losing the game and ending the game
        endListener = new UnityAction(EndGame);
    }

    void OnEnable()
    {
        EventManager.StartListening("Lose Game", loseListener); //Start listening for events
        EventManager.StartListening("End Game", endListener);
    }

    void OnDisable()
    {
        EventManager.StopListening("Lose Game", loseListener); //Stop listening for events
        EventManager.StopListening("End Game", endListener);
    }

    public void EndGame()
    {
        SceneManager.LoadScene("Menu"); //If end game event is invoked, open the Menu screen. Not used in this version.
    }

    public void replayGame()
    {
        SceneManager.LoadScene("Test"); //If game is replayed, reload the current scene
    }

    void LoseGame()
    {
        //Debug.Log("lose triggered");
        Time.timeScale = 0f; //If game is lost, stop time.
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("Spawner");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject spawner in spawners)
        {
            spawner.SetActive(false); //Turn all spawners off
        }

        foreach(GameObject enemy in enemies)
        {
            Destroy(enemy); //Destroy all enemies
        }
        panel.SetActive(true); //Show end of game screen
        currentScore.text = pp.daysSurvived.ToString();
        if (pp.daysSurvived > PlayerPrefs.GetInt("highScore")) //If player has achieved new high score
        {
            PlayerPrefs.SetInt("highScore", pp.daysSurvived); //Set the highscore locally and post it to facebook API
            facebook.SetScore();
        }
        highScore.text = PlayerPrefs.GetInt("highScore").ToString(); //Set end of game data.
       // Debug.Log(PlayerPrefs.GetInt("highScore").ToString());
        
        
    }

    public void toggleAchievementMenu() //Toggle achievement menu on and off
    {
        if(achievementCanvas.activeInHierarchy)
        {
            achievementCanvas.SetActive(false);
        }
        else
        {
            achievementCanvas.SetActive(true);
        }
    }
}
