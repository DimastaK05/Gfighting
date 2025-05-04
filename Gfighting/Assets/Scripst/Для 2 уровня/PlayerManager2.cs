using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
public class PlayerManager2 : MonoBehaviour
{
    public AudioSource moveSound;
    Animator animator;
    public static float playerHealth = 100f;
    public static bool gameOver;
    public TextMeshProUGUI playerHealthText;
    public static bool isTakingDamage { get; private set; }
    public Image Bar;
    void Start()
    {
        animator = GetComponent<Animator>();
        playerHealth = 100;
        gameOver = false;
        isTakingDamage = false;
        Bar.fillAmount = (float)playerHealth / 100f;
    }

    public void Damage(int amount)
    {
        if (playerHealth <= 0) return;

        StartCoroutine(HandleDamage());
        if (!PlayerController.isBlock) playerHealth -= amount;

        if (playerHealth >= 0) Bar.fillAmount = (float)playerHealth / 100f;
        if (playerHealth > 0 && !PlayerAttack2.isAttacking && !PlayerAttack2.isCrossAttacking && !PlayerController.isBlock)
        {
            moveSound.Play();
            animator.SetTrigger("Hurt");
        }
        if (playerHealth <= 0)
        {
            gameOver = true;
            animator.SetTrigger("Die");
            PlayerController.speed = 0f;
            PlayerController.rotationSpeed = 0f;
            StartCoroutine(WaitAndEndGame());
        }
    }

    private IEnumerator HandleDamage()
    {
        isTakingDamage = true;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        isTakingDamage = false;
    }

    private IEnumerator WaitAndEndGame()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        SceneManager.LoadScene("scene_died_2");
    }
}