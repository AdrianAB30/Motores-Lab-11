using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ObstaclesPool : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private int poolSize = 5;
    [SerializeField] private float spawnTime = 2f;
    [SerializeField] private float xSpawnPosition = 10f;
    [SerializeField] private float minYPosition = -2f;
    [SerializeField] private float maxYPosition = 1.5f;
    [SerializeField] private float resetPositionX = -10f;

    private float elapsedTime;
    private int currentObstacleIndex;
    private GameObject[] obstacles;

    private void Start()
    {
        obstacles = new GameObject[poolSize];

        for (int i = 0; i < poolSize; ++i)
        {
            obstacles[i] = Instantiate(obstaclePrefab);
            obstacles[i].SetActive(false);
        }
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= spawnTime)
        {
            SpawnObstacle();
            elapsedTime = 0f;
        }

        for (int i = 0; i < poolSize; i++)
        {
            if (obstacles[i].activeSelf)
            {
                obstacles[i].transform.position += Vector3.left * Time.deltaTime * 2f;

                if (obstacles[i].transform.position.x < resetPositionX)
                {
                    obstacles[i].SetActive(false);
                }
            }
        }
    }

    private void SpawnObstacle()
    {
        float ySpawnPosition = Random.Range(minYPosition, maxYPosition);
        Vector2 spawnPosition = new Vector2(xSpawnPosition, ySpawnPosition);

        obstacles[currentObstacleIndex].transform.position = spawnPosition;
        obstacles[currentObstacleIndex].SetActive(true);

        currentObstacleIndex = (currentObstacleIndex + 1) % poolSize;
    }
}
