using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgain : MonoBehaviour
{
    public void playAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void nextScene()
    {
        string current = SceneManager.GetActiveScene().name;

        if (current == "Level1")
            SceneManager.LoadScene("Level2");
        else if (current == "Level2")
            SceneManager.LoadScene("Level3");
        else
            SceneManager.LoadScene("Level1"); // loop back or change to MainMenu
    }

    public void lastScene()
    {
        SceneManager.LoadScene("Level3");
    }

    public void prevScene()
    {
        string current = SceneManager.GetActiveScene().name;

        if (current == "Level3")
            SceneManager.LoadScene("Level2");
        else if (current == "Level2")
            SceneManager.LoadScene("Level1");
        else
            SceneManager.LoadScene("Level1"); // loop back or change to MainMenu
    }
}
