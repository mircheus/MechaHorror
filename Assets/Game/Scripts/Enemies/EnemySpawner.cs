using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private BaseEnemy.BaseEnemy enemyPrefab;
        [SerializeField] private List<Transform> spawnPoints;

        private BaseEnemy.BaseEnemy _currentEnemy;

        private void Start()
        {
            SpawnEnemy();
        }

        public void SpawnEnemy()
        {
            if(_currentEnemy != null)
            {
                _currentEnemy.Death -= SpawnEnemy;
                _currentEnemy = null;
            }
            
            if (enemyPrefab == null || spawnPoints.Count == 0)
            {
                Debug.LogWarning("Missing enemyPrefab or spawn points.");
                return;
            }

            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            _currentEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            _currentEnemy.Init(playerTransform);
            
            if (_currentEnemy != null)
            {
                _currentEnemy.Death += SpawnEnemy;
                _currentEnemy.Death += () =>
                {
                    Debug.Log("Enemy died, spawning a new one.");
                };
            }
            else
            {
                Debug.LogError("Enemy prefab does not have an Enemy script with OnEnemyDied event.");
            }
        }
    }
}