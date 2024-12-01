using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ScriptSceneDied : MonoBehaviour
{
  
    public void ToSceneLevel1()
    {
        SceneManager.LoadScene("Level1");
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
