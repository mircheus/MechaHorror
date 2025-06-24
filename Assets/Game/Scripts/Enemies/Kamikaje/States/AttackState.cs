using Game.Scripts.Enemies._BaseEnemy;
using UnityEngine;

namespace Game.Scripts.Enemies.Kamikaje.States
{
    public class AttackState : IState
    {
        private EnemyAI _enemyAI;
        private ParticleSystem _explosionParticle;
        private SphereCollider _explosionCollider;
        private BoxCollider _mainCollider;

        public AttackState(EnemyAI enemyAI, ParticleSystem explosionParticle, SphereCollider explosionCollider, BoxCollider mainCollider)
        {
            _enemyAI = enemyAI;
            _explosionParticle = explosionParticle;
            _explosionCollider = explosionCollider;
            _mainCollider = mainCollider;
        }
        
        public void Enter()
        {
            Explode();
        }

        public void Execute()
        {
           
        }

        public void Exit()
        {
           
        }

        private void Explode()
        {
            _explosionParticle.Play();
            _explosionCollider.enabled = true;
            _mainCollider.enabled = false;
            _enemyAI.StateMachine.Enter<DeadState>();
        }
    }
}