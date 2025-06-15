using Game.Scripts.Enemies._BaseEnemy;
using UnityEngine;

namespace Game.Scripts.Enemies.Kamikaje.States
{
    public class ChasingState : IState
    {
        private readonly KamikajeAI _enemyAI;
        private readonly Animator _animator;

        private readonly int _run = Animator.StringToHash("Run");
        
        public ChasingState(EnemyAI enemyAI, Animator animator)
        {
            _enemyAI = (KamikajeAI)enemyAI;
            _animator = animator;
        }
        
        public void Enter()
        {
            _animator.Play(_run);
        }

        public void Execute()
        {
            _enemyAI.Agent.SetDestination(_enemyAI.Target.position);
            _enemyAI.Agent.updateRotation = true;
            
            if (Vector3.Distance(_enemyAI.transform.position, _enemyAI.Target.position) < _enemyAI.Agent.stoppingDistance)
            {
                Debug.Log("Enter Attack State from Chasing State");
                _enemyAI.StateMachine.Enter<AttackState>();
                
                // _animator.Play(_idle);
                // _enemyAI.Agent.ResetPath();
                // _enemyAI.StateMachine.Enter<IdleState>();
                // _enemyAI.StateMachine.ChangeState(new AttackState(_enemyAI));
            }
        }

        public void Exit()
        {
           
        }
    }
}