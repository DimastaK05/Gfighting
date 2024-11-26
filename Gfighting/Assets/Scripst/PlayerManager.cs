using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerManager : MonoBehaviour
{

    public int playerHealth =100;
    public static bool gameOver;
    public TextMeshProUGUI playerHealthText;
    
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 100;
        gameOver = false;
    }

    public void Damage(int amount)
    {

        playerHealthText.text = "" + playerHealth;
        playerHealth -= amount;
        if (playerHealth <= 0)
        {
            gameOver = true;
        }
        if (gameOver)
        {
            SceneManager.LoadScene("scene_died");
        }
        playerHealthText.text = "" + playerHealth;
    }
}
    


