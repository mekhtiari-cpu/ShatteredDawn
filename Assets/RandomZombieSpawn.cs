using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomZombieSpawn : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] GameObject zombiePrefab;
    [SerializeField] GameObject zombieParent;
    [SerializeField] Transform spawnPoints;
    [SerializeField] bool hasActivatedSpawner;
    [SerializeField] float spawnInterval;
    [SerializeField] int currentZombieCount;
    [SerializeField] int maxZombieCount;

    private void Update()
    {
        if (!hasActivatedSpawner)
        {
            StartCoroutine(SpawnZombie());
            hasActivatedSpawner = true;
        }
    }

    IEnumerator SpawnZombie()
    {
        while(true)
        {
            if(currentZombieCount < maxZombieCount)
            {
                int randomSpawnPoint = Random.Range(0, spawnPoints.childCount - 1);
                Instantiate(zombiePrefab, spawnPoints.GetChild(randomSpawnPoint).position, Quaternion.identity, zombieParent.transform);
                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }
}
