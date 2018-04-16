using System.Collections;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;

public class FBScript : MonoBehaviour {

   public GameObject DialogLoggedIn;
   public GameObject DialogLoggedOut;
    public GameObject DialogUsername;
    public GameObject DialogProfilePic;
    public GameObject scorePrefab;
    public GameObject scrollScoreList;
    private GameObject scorePanel;
    public GameObject fbLeader;
    private event Action<string> s;

    PlayerProperties pp;
    void Awake()
    {
        
    
        pp = FindObjectOfType<PlayerProperties>();
        FB.Init(SetInit, OnHideUnity);
        fbLeader.SetActive(false);
    }

    void SetInit()
    {
        if(FB.IsLoggedIn)
        {
            Debug.Log("FB is logged in");
        }
        else
        {
            Debug.Log("FB is not logged in");
        }

        ToggleFBMenus(FB.IsLoggedIn);
    }

    void OnHideUnity(bool isGameShown)
    {
        if(!isGameShown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void FBLogin()
    {
        
        FB.LogInWithReadPermissions(new List<string>() { "public_profile", "user_friends" }, AuthCallBack); //Read permissions of facebook
        
        FB.LogInWithPublishPermissions(new List<string>() { "publish_actions" }, AuthCallBack); //Write permissions of facebook, used to post scores to API
        
       
    }

    void AuthCallBack(IResult result)
    {
        if(result.Error != null)
        {
            Debug.Log(result.Error);
        }
        else
        {
            if (FB.IsLoggedIn) //If successfully logged in
            {
                Debug.Log("FB is logged in");
                //FB.API("/me/permissions", HttpMethod.GET, delegate (IGraphResult result1) {
                //    Debug.Log(result1.RawResult); //Debugging permissions
                //});
            }
            else
            {
                Debug.Log("FB is not logged in");
            }

            //Set which UI is supposed to be shown
            ToggleFBMenus(FB.IsLoggedIn);
        }
    }

    void ToggleFBMenus(bool isLoggedIn)
    {
        if(isLoggedIn) //If user is logged in
        {
            DialogLoggedIn.SetActive(true); //Show the UI related to being logged in
            DialogLoggedOut.SetActive(false); //Hide the logged out UI

            FB.API("/me?fields=first_name", HttpMethod.GET, DisplayUsername); //Get the user's first name
            FB.API("/me/picture?redirect=false", HttpMethod.GET, (IGraphResult result) => ProfilePhotoCallback(result, "Joe", "0")); //Get the user's profile picture
            
            refreshScores(); //Populate the scoreboard
        }
        else //Otherwise show logged out UI.
        {
            DialogLoggedOut.SetActive(true);
            DialogLoggedIn.SetActive(false);
        }
    }

    void DisplayUsername(IResult result) //Show a welcome message before deleting it
    {
        Text UserName = DialogUsername.GetComponent<Text>();
        if(string.IsNullOrEmpty(result.Error))
        {
            UserName.text = "Hi, " + result.ResultDictionary["first_name"];
            StartCoroutine(RemoveWelcomeMessage(UserName));
        }
        else
        {
            Debug.Log(result.Error);
        }
    }

   IEnumerator RemoveWelcomeMessage(Text message) //Removes the "Hi, Lewis!" message when user first logs in
    {

        yield return new WaitForSeconds(5);
        message.text = "";
    }

    void DisplayProfilePic(IGraphResult result) //Old attempt at creating profile pictures, no longer gets the picture in result
    {
        if(string.IsNullOrEmpty(result.Error))
        {
            Image ProfilePic = DialogProfilePic.GetComponent<Image>();

            ProfilePic.sprite = Sprite.Create(result.Texture, new Rect(0, 0, 128, 128), new Vector2());
        }
        else
        {
            Debug.Log(result.Error);
        }
    }

    private void ProfilePhotoCallback(IGraphResult result, string name, string score)
    {
        
        if (string.IsNullOrEmpty(result.Error) && !result.Cancelled)
        {
            IDictionary data = result.ResultDictionary["data"] as IDictionary;
            string photoURL = data["url"] as string; //Get the profile photo's url used to download
            //Debug.Log(photoURL);
            StartCoroutine(fetchProfilePic(photoURL, name, score)); //Call function to download photo
        }
    }

    private IEnumerator fetchProfilePic(string url, string name, string score)
    {
        Image ProfilePic = DialogProfilePic.GetComponent<Image>();
        Image tempPic = GameObject.Find("TempPic").GetComponent<Image>();
        WWW www = new WWW(url); //New web request for the photo
        yield return www; //Wait until it has been downloaded
        if (ProfilePic.sprite == null) //If the user is logging in, so their profile picture isn't set
        {
            ProfilePic.sprite = Sprite.Create(www.texture, new Rect(0, 0, 50, 50), new Vector2()); //Set profile picture on UI
        }
        else //otherwise
        {
           
            tempPic.sprite = Sprite.Create(www.texture, new Rect(0, 0, 50, 50), new Vector2()); //set an invisible image to the downloaded image
            CreateScore(tempPic, name ,score); //Create score
           // SetProfilePic(www.texture);
           // Debug.Log("pic set");
        }
    }

    public void CreateScore(Image pic, string name, string score) //Create a score from the prefab
    {
        scorePanel = Instantiate(scorePrefab) as GameObject;
        scorePanel.transform.SetParent(scrollScoreList.transform, false); //Set the scores position in the UI

        Text friendName = scorePanel.transform.Find("Name").GetComponent<Text>();
        Text friendScore = scorePanel.transform.Find("Score").GetComponent<Text>();
        Image friendPic = scorePanel.transform.Find("Image").GetComponent<Image>();

        friendName.text = name; //Set the data of the score
        friendScore.text = score;
        friendPic.sprite = pic.sprite;
        if(name == "Joe" && score == "0")
        {
            Destroy(scorePanel); //Delete the dummy data from the lamba expression.
        }
    }

    void refreshScores() //Delete and repopulate the scoreboard everytime a new game is begun.
    {
        GameObject[] scores = GameObject.FindGameObjectsWithTag("Score");
        foreach(GameObject score in scores)
        {
            Destroy(score);
        }
        QueryScore();
    }

    public void ToggleLeaderboard() //Toggle the leaderboard UI on and off
    {
        if(fbLeader.active)
        {
            fbLeader.SetActive(false);
        }
        else
        {
            fbLeader.SetActive(true);
        }
    }

    public void QueryScore()
    {
        
        //fbLeader.SetActive(true);

        FB.API("/app/?fields=scores.limit(30){score,user}", HttpMethod.GET, getScoreCallBack); //Gets 30 scores from the API.
    }

    void SetProfilePic(Texture2D pic)
    {
          
          //friendPic.sprite = Sprite.Create(pic, new Rect(0,0,50,50), new Vector2());
    }

    private static int CompareScores(int x, int y) //Test function when trying to order the list of friend scores.
    {
       
      return x.CompareTo(y);
               
    }

    void getScoreCallBack(IResult result)
    {
        
        IDictionary data = result.ResultDictionary["scores"] as IDictionary; //Creates a dictionary of the API data gotten from QueryScore()
        
        List<object> scoreList = (List<object>)data["data"]; //Createa a list the score objects pulled from the API


        
        foreach(object obj in scoreList) //for each score object in that list
        {
           
            var entry = (Dictionary<string, object>)obj; //Go down a level into the score entry
            var user = (Dictionary<string, object>)entry["user"]; //Get the name from that score entry
   
            string n = user["name"].ToString(); //Get the string values of name and score
            string s = entry["score"].ToString();
            FB.API(user["id"].ToString() + "/picture?redirect=false", HttpMethod.GET, (IGraphResult result1) => ProfilePhotoCallback(result1, n, s)); //Pass name and score into the profile picture download call.
          
        }
    }

    private IEnumerator checkIfPicSet(Image pic, Dictionary<string,object>user) //Test function when trying to fix the profile pictures loading in random order error
    {
        string n = user["name"].ToString();
        FB.API(user["id"].ToString() + "/picture?redirect=false", HttpMethod.GET, (IGraphResult result) => ProfilePhotoCallback(result,n, "0"));
        // yield return new WaitUntil(() => tempPic.sprite != null);
        //Debug.Log(tempPic.sprite.ToString());
        yield return new WaitForSeconds(5);
        //pic.sprite = tempPic.sprite;
        //tempPic.sprite = null;
    }



    public void SetScore() //Function to post the user's score to the Facebook API.
    {
        var scoreData = new Dictionary<string, string>();
        scoreData["score"] = pp.daysSurvived.ToString();

        FB.API("/me/scores", HttpMethod.POST, delegate (IGraphResult result){}, scoreData);
    }
}
