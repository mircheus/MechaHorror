using Game.Scripts.Enemies._BaseEnemy;
using UnityEngine;

namespace Game.Scripts.Enemies.Kamikaje.States
{
    public class AttackState : IState
    {
        private EnemyAI _enemyAI;
        private ParticleSystem _explosionParticle;
        private SphereCollider _explosionCollider;

        public AttackState(EnemyAI enemyAI, ParticleSystem explosionParticle, SphereCollider explosionCollider)
        {
            _enemyAI = enemyAI;
            _explosionParticle = explosionParticle;
            _explosionCollider = explosionCollider;
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
            _enemyAI.StateMachine.Enter<DeadState>();
        }
    }
}