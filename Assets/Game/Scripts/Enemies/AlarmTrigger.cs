using System;
using Game.Scripts.Enemies._BaseEnemy;
using Game.Scripts.Enemies.FourLeg.States;
using UnityEngine;

namespace Game.Scripts.Enemies
{
    public class AlarmTrigger : MonoBehaviour
    {
        [SerializeField] private EnemyAI[] enemies;

        private void OnEnable()
        {
            foreach (var enemy in enemies)
            {
                enemy.PlayerDetected += OnPlayerDetected;
            }
        }

        private void OnDisable()
        {
            foreach (var enemy in enemies)
            {
                enemy.PlayerDetected -= OnPlayerDetected;
            }
        }

        private void OnPlayerDetected()
        {
            foreach (var enemy in enemies)
            {
                if (enemy.StateMachine.CurrentState is not ChaseState) // TODO: узкое место тк ChaseState взят только из FourLeg 
                {
                    enemy.StateMachine.Enter<ChaseState>();
                }
            }
        }
    }
}