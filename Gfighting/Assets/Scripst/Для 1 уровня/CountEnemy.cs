using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountEnemy : MonoBehaviour
{
    public int totalEnemies;
    private int enemiesDefeated = 0;
    [SerializeField] private float delay = 5f; // Время задержки в секундах

    void Start()
    {
        totalEnemies = FindObjectsOfType<Enemy>().Length;
    }

    public void EnemyDefeated()
    {
        enemiesDefeated++;
        CheckForLevelEnd();
    }

    private void CheckForLevelEnd()
    {
        if (enemiesDefeated >= totalEnemies)
        {
            EndLevel();
        }
    }

    private void EndLevel()
    {
        // Запускаем корутину с задержкой
        StartCoroutine(LoadSceneAfterDelay());
    }

    // Новая корутина для задержки
    private IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delay); // Ждем указанное время
        SceneManager.LoadScene("WinScene"); // Загружаем сцену после задержки
    }
}