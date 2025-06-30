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
        private Kamikaje _kamikaje;

        public DeadState(EnemyAI enemyAI, ParticleSystem deathParticle, GameObject mechGameObject, BoxCollider mainCollider, BaseEnemy.BaseEnemy baseEnemy)
        {
            _enemyAI = (KamikajeAI)enemyAI;
            _deathParticle = deathParticle;
            _mechGameObject = mechGameObject;
            _mainCollider = mainCollider;
            _kamikaje = (Kamikaje)baseEnemy;
        }

        public void Enter()
        {
            _kamikaje.InvokeDeathEvent();
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