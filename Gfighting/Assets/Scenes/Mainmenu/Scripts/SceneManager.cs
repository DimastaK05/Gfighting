using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void ToHowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
