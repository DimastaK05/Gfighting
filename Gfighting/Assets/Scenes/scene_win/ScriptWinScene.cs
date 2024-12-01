using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ScriptWinScene : MonoBehaviour
{

    public void ToNextLevel()
    {
        SceneManager.LoadScene("Level2");
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
