using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour {

	public void toInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void toInstructions2()
    {
        SceneManager.LoadScene("Instructions2");
    }

    public void toCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void toGame()
    {
        SceneManager.LoadScene("DemoScene");
    }
    public void toMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void quitGame()
    {
        Application.Quit();
    }
}
