using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

	public void PlayGameBtn(string loadLevel)
    {
        SceneManager.LoadScene(loadLevel); //Used in menu screen which is not being used
    }

    public void QuitGameBtn()
    {
        Application.Quit();
    }
}
