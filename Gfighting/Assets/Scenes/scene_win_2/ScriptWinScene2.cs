using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ScriptWinScene2 : MonoBehaviour
{
    public void ToNextLevel3()
    {
        SceneManager.LoadScene("Level3");
        PlayerController.speed = 5f;
        PlayerController.rotationSpeed = 5f;
    }

 

    public void ToMainMenuScene()
    {
        SceneManager.LoadScene("MainMenuScene");
        PlayerController.speed = 5f;
        PlayerController.rotationSpeed = 5f;
    }
}
