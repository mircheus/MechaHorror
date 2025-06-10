using UnityEngine;

public class HorrorSpawner : MonoBehaviour
{
    public GameObject horrorObjectPrefab; // Assign the prefab in the Inspector
    public Transform player; // Assign the player transform in the Inspector
    public float spawnDistance = 3f; // Distance behind the player
    public float spawnCooldown = 5f; // Time before the next spawn
    private float nextSpawnTime = 0f;
    public Terrain terrain; // Assign the terrain in the Inspector

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnBehindPlayer();
            nextSpawnTime = Time.time + spawnCooldown;
        }
    }

    void SpawnBehindPlayer()
    {
        if (horrorObjectPrefab == null || player == null)
        {
            Debug.LogWarning("HorrorSpawner: Missing prefab or player reference.");
            return;
        }

        // Calculate spawn position behind the player
        Vector3 spawnPosition = player.position - player.forward * spawnDistance;

        // Adjust height based on the terrain
        if (terrain != null)
        {
            float terrainHeight = terrain.SampleHeight(spawnPosition);
            spawnPosition.y = terrainHeight;
        }
        else
        {
            Debug.LogWarning("HorrorSpawner: Terrain is not assigned.");
        }

        // Instantiate the horror object facing the player
        GameObject spawnedObject = Instantiate(horrorObjectPrefab, spawnPosition, Quaternion.LookRotation(player.forward));

        Debug.Log("Spawned horror object behind the player!");
    }
}