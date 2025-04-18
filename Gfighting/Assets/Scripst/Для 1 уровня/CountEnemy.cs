using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CountEnemy : MonoBehaviour
{
    public int totalEnemies; // Общее количество врагов
    private int enemiesDefeated = 0; // Количество уничтоженных врагов

    void Start()
    {
        totalEnemies = FindObjectsOfType<Enemy>().Length; // Подсчет всех врагов на старте
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
        
       SceneManager.LoadScene("WinScene");
    }

}
