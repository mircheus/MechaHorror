using Game.Scripts.Enemies._BaseEnemy;
using Game.Scripts.Enemies.BaseEnemy;
using UnityEngine;

namespace Game.Scripts.Enemies.Footballer.States
{
    public class DeadState : IState
    {
        private ParticleSystem _deathParticle;
        private GameObject _mechGameObject;
        private FootballerAI _enemyAI;
        private BoxCollider _mainCollider;
        // private float _destroyDelay = 2f; 
        
        public DeadState(EnemyAI enemyAI, ParticleSystem deathParticle, GameObject mechGameObject, BoxCollider mainCollider)
        {
            _enemyAI = (FootballerAI)enemyAI;
            _deathParticle = deathParticle;
            _mechGameObject = mechGameObject;
            _mainCollider = mainCollider;
        }
        
        public void Enter()
        {
            _mechGameObject.SetActive(false);
            _mainCollider.enabled = false;
            
            if (_deathParticle != null)
            {
                _deathParticle.Play();
            }
            else
            {
                Debug.LogWarning("Death particle system is not assigned.");
            }
        }

        public void Execute()
        {
        }

        public void Exit()
        {
        }
    }
}