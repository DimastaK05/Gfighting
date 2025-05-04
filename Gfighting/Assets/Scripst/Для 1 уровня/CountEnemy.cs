using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountEnemy : MonoBehaviour
{
    public int totalEnemies;
    private int enemiesDefeated = 0;
    [SerializeField] private float delay = 5f; // ����� �������� � ��������

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
        // ��������� �������� � ���������
        StartCoroutine(LoadSceneAfterDelay());
    }

    // ����� �������� ��� ��������
    private IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delay); // ���� ��������� �����
        SceneManager.LoadScene("WinScene"); // ��������� ����� ����� ��������
    }
}