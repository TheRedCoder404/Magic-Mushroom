using System;
using UnityEngine;

public class SpawnerPlane : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float spawnAreaPercentage = 0.8f;

    private GameObject spawnPrefab;
    private Vector3 planeSize;
    private Vector3 spawnPosition;

    private void Awake()
    {
        planeSize = gameObject.GetComponent<Renderer>().bounds.size * spawnAreaPercentage;
        spawnPrefab = gameManager.GetEnemyPrefab();
    }

    public void SpawnAtRandomPosition()
    {
        float x = UnityEngine.Random.Range(-planeSize.x / 2, planeSize.x / 2);
        float z = UnityEngine.Random.Range(-planeSize.z / 2, planeSize.z / 2);
        spawnPosition = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
        SpawnAt(spawnPosition);
    }

    private void SpawnAt(Vector3 spawnPos)
    {
        Instantiate(spawnPrefab, spawnPos, Quaternion.identity);
    }
}
