using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ScriptsScene : MonoBehaviour
{
    public void ToSceneLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void ToMainMenuScene()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
