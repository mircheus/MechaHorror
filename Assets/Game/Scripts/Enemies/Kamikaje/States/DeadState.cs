using Game.Scripts.Enemies._BaseEnemy;
using UnityEngine;
using UnityEngine.Events;
using VFavorites.Libs;

namespace Game.Scripts.Enemies.Kamikaje.States
{
    public class DeadState : IState
    {
        private ParticleSystem _deathParticle;
        private GameObject _mechGameObject;
        private KamikajeAI _enemyAI;
        private BoxCollider _mainCollider;

        public DeadState(EnemyAI enemyAI, ParticleSystem deathParticle, GameObject mechGameObject, BoxCollider mainCollider)
        {
            _enemyAI = (KamikajeAI)enemyAI;
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
            
            _enemyAI.SelfDestroy(this);
        }

        public void Execute()
        {

        }

        public void Exit()
        {

        }
    }
}