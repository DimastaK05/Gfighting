using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptWinScene3 : MonoBehaviour
{
    // Start is called before the first frame update
    //public void ToNextLevel3()
    //{
    //    SceneManager.LoadScene("Level3");
    //    PlayerController.speed = 5f;
    //    PlayerController.rotationSpeed = 5f;
    //}



    public void ToMainMenuScene()
    {
        SceneManager.LoadScene("MainMenuScene");
        PlayerController.speed = 5f;
        PlayerController.rotationSpeed = 5f;
    }
}
