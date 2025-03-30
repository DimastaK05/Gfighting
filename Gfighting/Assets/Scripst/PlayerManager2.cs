
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerManager2 : MonoBehaviour
{
    Animator animator;
    public static int playerHealth = 100;
    public static bool gameOver;
    public TextMeshProUGUI playerHealthText;

    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();
        playerHealth = 100;
        gameOver = false;
    }

    public void Damage(int amount)
    {

        if (playerHealth >= 0) playerHealthText.text = "" + playerHealth;
        if (playerHealth > 0) animator.SetTrigger("Hurt");
        playerHealth -= amount;

        if (playerHealth <= 0)
        {
            gameOver = true;
        }
        if (gameOver)
        {
            animator.SetTrigger("Die");
            PlayerController.speed = 0f;
            PlayerController.rotationSpeed = 0f;
            StartCoroutine(WaitAndEndGame());
        }
        if (playerHealth >= 0) playerHealthText.text = "" + playerHealth;
    }
    private IEnumerator WaitAndEndGame()
    {

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        SceneManager.LoadScene("scene_died_2");
    }
}