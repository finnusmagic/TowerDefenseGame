using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    public GameObject spawnPoint;
    public GameObject[] enemies;

    public int maxEnemeiesOnScreen;
    public int totalEnemies;
    public int enemiesPerSpawn;

    private int enemiesOnScreen = 0;
    const float spawnDelay = 1f;

    void Start ()
    {
        StartCoroutine(SpawnEnemies());
	}

    IEnumerator SpawnEnemies()
    {
        if (enemiesPerSpawn > 0 && enemiesOnScreen < totalEnemies)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                if (enemiesOnScreen < maxEnemeiesOnScreen)
                {
                    GameObject newEnemy = Instantiate(enemies[1]) as GameObject;
                    newEnemy.transform.position = spawnPoint.transform.position;
                    enemiesOnScreen += 1;
                }
            }
        }
        yield return new WaitForSeconds(spawnDelay);
        StartCoroutine(SpawnEnemies());
    }

    public void RemoveEnemyFromScreen()
    {
        if (enemiesOnScreen > 0)
        {
            enemiesOnScreen -= 1;
        }
    }
	
}
