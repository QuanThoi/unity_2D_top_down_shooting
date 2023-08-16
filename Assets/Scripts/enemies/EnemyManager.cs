using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    public float spawnRate = 1.0f;
    public bool canSpawn = true;
    public int maxEnemyCanSpawn = 10;
    public Enemy[] enemies;

    private int totalEnemySpawn = 0;

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner() { 
        while (canSpawn && totalEnemySpawn < maxEnemyCanSpawn)
        {
            yield return new WaitForSeconds(spawnRate);
            Enemy enemySpawn = enemies[Random.Range(0, enemies.Length - 1)];
            Instantiate(enemySpawn, Vector3.zero, Quaternion.identity);
            totalEnemySpawn += 1;
        }
    }
}
