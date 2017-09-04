using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    [SerializeField] GameObject spawnPoint;
    [SerializeField] GameObject[] enemies;

    [SerializeField] int maxEnemeiesOnScreen;
    [SerializeField] int totalEnemies;
    [SerializeField] int enemiesPerSpawn;

    public List<Enemy> EnemyList = new List<Enemy>();

    const float spawnDelay = 1f;

    void Start ()
    {
        StartCoroutine(SpawnEnemies());
	}

    IEnumerator SpawnEnemies()
    {
        if (enemiesPerSpawn > 0 && EnemyList.Count < totalEnemies)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                if (EnemyList.Count < maxEnemeiesOnScreen)
                {
                    GameObject newEnemy = Instantiate(enemies[1]) as GameObject;
                    newEnemy.transform.position = spawnPoint.transform.position;
                }
            }
        }
        yield return new WaitForSeconds(spawnDelay);
        StartCoroutine(SpawnEnemies());
    }

    public void RegisterEnemy(Enemy enemy)
    {
        EnemyList.Add(enemy);
    }

    public void UnregisterEnemy(Enemy enemy)
    {
        EnemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    public void DestoryAllEnemies()
    {
        foreach (Enemy enemy in EnemyList)
        {
            Destroy(enemy.gameObject);
        }

        EnemyList.Clear();
    }	
}
