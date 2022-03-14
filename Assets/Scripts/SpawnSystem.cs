using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject healthPrefab;
    [SerializeField] private GameObject speedPrefab;
    [SerializeField] private float spawnRadius = 8f;
    [SerializeField] private float spawnOffset = 10f;
    [SerializeField] private float spawnInterval = 0.4f;

    private WaitForSeconds spawnWait;
    private Player player;

    private void Start()
    {
        spawnWait = new WaitForSeconds(spawnInterval);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            Vector2 spawnPos = (Vector2)player.transform.position + player.LookDir * spawnOffset + Random.insideUnitCircle * spawnRadius;
            float randVal = Random.value;
            if (randVal < 0.7f)
            {
                Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);
            }
            else if (randVal < 0.90f)
            {
                Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            }
            else if (randVal < 0.95f)
            {
                Instantiate(healthPrefab, spawnPos, Quaternion.identity);
            }
            else
            {
                Instantiate(speedPrefab, spawnPos, Quaternion.identity);
            }
            yield return spawnWait;
        }
    }
}
