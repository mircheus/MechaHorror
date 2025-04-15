using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [Header("Car Settings")]
    public GameObject carPrefab;
    public float spawnInterval = 2f;
    public float carSpeed = 10f;

    [Header("Lane Settings")]
    public Transform player; // Assign the player transform
    public float spawnZ = 50f; // Distance in front of the player to spawn cars
    // public float[] laneXPositions = { -6f, -2f, 2f, 6f }; // Four lanes
    public float[] laneXPositions = { -6f, -2f, 2f }; // three lanes

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnCars();
            timer = 0f;
        }
    }

    void SpawnCars()
    {
        // Pick 1 or 2 lanes at random
        // List<int> availableLanes = new List<int> { 0, 1, 2, 3};
        List<int> availableLanes = new List<int> { 0, 1, 2};
        int carsToSpawn = Random.Range(1, 2); // 1 or 2

        for (int i = 0; i < carsToSpawn; i++)
        {
            int index = Random.Range(0, availableLanes.Count);
            float laneX = laneXPositions[availableLanes[index]];
            availableLanes.RemoveAt(index); // Don't pick the same lane twice

            Vector3 spawnPos = new Vector3(laneX, 0f, player.position.z + spawnZ);
            GameObject car = Instantiate(carPrefab, spawnPos, Quaternion.Euler(Quaternion.identity.x, Quaternion.identity.y  + 180, Quaternion.identity.z)); // Rotate the car to face the player));
            car.AddComponent<CarMover>().speed = carSpeed;
        }
    }
}