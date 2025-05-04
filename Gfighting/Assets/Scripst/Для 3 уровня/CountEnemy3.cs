using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountEnemy3 : MonoBehaviour
{
    [Header("Настройки врагов")]
    public int totalEnemies;
    private int enemiesDefeated = 0;
    private int enemiesDefeatedBoss = 0;
    public int totalBoss = 1;

    [Header("Настройки босса")]
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] private Transform bossSpawnPoint;
    [SerializeField] private float delayAfterBoss = 5f;

    private GameObject spawnedBoss;
    private bool bossSpawned = false;

    void Start()
    {
        FindEnemies();
    }

    void FindEnemies()
    {
        totalEnemies = FindObjectsOfType<Enemy3>().Length;
    }

    public void EnemyDefeated()
    {
        enemiesDefeated++;
        CheckForLevelEnd();
    }

    public void EnemyDefeatedBoss()
    {
        enemiesDefeatedBoss++;
        CheckForLevelEndBoss();
    }

    private void CheckForLevelEndBoss()
    {
        if (enemiesDefeatedBoss >= totalBoss)
        {
            EndLevel();
        }
    }

    private void CheckForLevelEnd()
    {
        if (!bossSpawned && enemiesDefeated >= totalEnemies)
        {
            SpawnBoss();
        }
    }

    private void SpawnBoss()
    {
        bossSpawned = true;

        if (bossPrefab != null && bossSpawnPoint != null)
        {
            spawnedBoss = Instantiate(bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);

            // Передаем ссылку на менеджер боссу
            Boss3 bossComponent = spawnedBoss.GetComponent<Boss3>();
            if (bossComponent != null)
            {
                bossComponent.SetEnemyCountReference(this);
            }
        }
    }

    private void EndLevel()
    {
        StartCoroutine(LoadSceneAfterDelay());
    }

    private IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayAfterBoss);
        SceneManager.LoadScene("WinScene3");
    }
}