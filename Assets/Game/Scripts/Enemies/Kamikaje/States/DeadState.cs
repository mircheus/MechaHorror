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

        public DeadState(EnemyAI enemyAI, ParticleSystem deathParticle, GameObject mechGameObject)
        {
            _enemyAI = (KamikajeAI)enemyAI;
            _deathParticle = deathParticle;
            _mechGameObject = mechGameObject;
        }

        public void Enter()
        {
            _mechGameObject.SetActive(false);
            
            if (_deathParticle != null)
            {
                _deathParticle.Play();
                // _enemyAI.
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