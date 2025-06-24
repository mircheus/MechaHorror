using Game.Scripts.Enemies._BaseEnemy;
using UnityEngine;

namespace Game.Scripts.Enemies.Kamikaje.States
{
    public class ChasingState : IState
    {
        private readonly KamikajeAI _enemyAI;
        private readonly KamikajeTrigger _kamikajeTrigger;
        private readonly Animator _animator;

        private readonly int _run = Animator.StringToHash("Run");
        
        public ChasingState(EnemyAI enemyAI, Animator animator, KamikajeTrigger kamikajeTrigger)
        {
            _enemyAI = (KamikajeAI)enemyAI;
            _animator = animator;
            _kamikajeTrigger = kamikajeTrigger;
        }
        
        public void Enter()
        {
            _animator.Play(_run);
            _kamikajeTrigger.PlayerEntered += _enemyAI.StateMachine.Enter<AttackState>;
        }

        public void Execute()
        {
            _enemyAI.Agent.SetDestination(_enemyAI.Target.position);
            _enemyAI.Agent.updateRotation = true;
        }

        public void Exit()
        {
            _kamikajeTrigger.PlayerEntered -= _enemyAI.StateMachine.Enter<AttackState>;
        }
    }
}