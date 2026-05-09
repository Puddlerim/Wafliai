using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformanceStressTest : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public int objectCount = 1000;
    public float spawnRadius = 25f;

    private readonly List<GameObject> spawnedObjects = new List<GameObject>();

    private IEnumerator Start()
    {
        for (int i = 0; i < objectCount; i++)
        {
            Vector3 position = transform.position + new Vector3(
                Random.Range(-spawnRadius, spawnRadius),
                0f,
                Random.Range(-spawnRadius, spawnRadius)
            );

            GameObject spawned = Instantiate(prefabToSpawn, position, Quaternion.identity);
            spawnedObjects.Add(spawned);

            if (i % 50 == 0)
            {
                yield return null;
            }
        }

        Debug.Log("Performance stress test finished. Spawned objects: " + spawnedObjects.Count);
    }
}