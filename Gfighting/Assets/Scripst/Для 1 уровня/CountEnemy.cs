using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CountEnemy : MonoBehaviour
{
    public int totalEnemies; // ����� ���������� ������
    private int enemiesDefeated = 0; // ���������� ������������ ������

    void Start()
    {
        totalEnemies = FindObjectsOfType<Enemy>().Length; // ������� ���� ������ �� ������
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
