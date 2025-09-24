using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject applePrefab;
    [SerializeField] private int minSpawnTimer;
    [SerializeField] private int maxSpawnTimer;

    private bool canSpawn;

    private void Start()
    {
        canSpawn = true;
        StartCoroutine(SpawnApple());
    }

    private IEnumerator SpawnApple()
    {
        while (canSpawn)
        {
            yield return new WaitForSeconds((float)Random.Range(minSpawnTimer, maxSpawnTimer + 1));
            GameObject apple = Instantiate(applePrefab, transform.position, Quaternion.identity);
            apple.transform.parent = null;
        }        
    }
}
