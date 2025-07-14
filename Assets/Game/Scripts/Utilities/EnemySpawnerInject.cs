using System;
using Game.Scripts.Enemies._BaseEnemy;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Scripts.Utilities
{
    public class EnemySpawnerInject : MonoBehaviour
    {
        [SerializeField] private Transform[] spawnPositions;
        
        [Inject] private EnemyAI.Factory _enemyFactory;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                Debug.Log("Spawn Enemy");
                SpawnEnemy(spawnPositions[Random.Range(0, spawnPositions.Length)].position);
                Debug.Log("Point3");
            }
        }

        public void SpawnEnemy(Vector3 position)
        {
            Debug.Log("Point1");
            var enemy = _enemyFactory.Create();
            Debug.Log("Point2");
            enemy.transform.position = position;
            
        }
    }
}
