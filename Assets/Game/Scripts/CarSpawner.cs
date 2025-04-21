using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [Header("Car Settings")]
    public GameObject carPrefab;
    public float carSpeed = 10f;

    [Header("Lane Settings")]
    public Transform player; // Assign player transform
    public float spawnZ = 50f;
    public float[] laneXPositions = { -6f, -2f, 2f, 6f };

    [Header("Beat Settings")]
    public float bpm = 187f; // Beats per minute

    private float beatTimer = 0f;
    private float secondsPerBeat;

    void Start()
    {
        secondsPerBeat = 60f / bpm;
    }

    void Update()
    {
        beatTimer += Time.deltaTime;

        if (beatTimer >= secondsPerBeat)
        {
            SpawnCars();
            beatTimer -= secondsPerBeat; // allow accurate rhythm even if frame skipped
        }
    }

    void SpawnCars()
    {
        List<int> availableLanes = new List<int> { 0, 1, 2, 3 };
        int carsToSpawn = Random.Range(1, 3); // 1 or 2 cars

        for (int i = 0; i < carsToSpawn; i++)
        {
            int index = Random.Range(0, availableLanes.Count);
            float laneX = laneXPositions[availableLanes[index]];
            availableLanes.RemoveAt(index);

            Vector3 spawnPos = new Vector3(laneX, 0f, player.position.z + spawnZ);
            // GameObject car = Instantiate(carPrefab, spawnPos, Quaternion.identity);
            GameObject car = Instantiate(carPrefab, spawnPos, Quaternion.Euler(Quaternion.identity.x, Quaternion.identity.y  + 180, Quaternion.identity.z)); // Rotate the car to face the player));
            car.AddComponent<CarMover>().speed = carSpeed;
        }
    }
}