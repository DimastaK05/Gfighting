using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptSceneDied3 : MonoBehaviour
{

    public void ToSceneLevel3()
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