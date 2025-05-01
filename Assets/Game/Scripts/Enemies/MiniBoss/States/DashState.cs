using DG.Tweening;
using Game.Scripts.Enemies._BaseEnemy;
using UnityEngine;

namespace Game.Scripts.Enemies.MiniBoss.States
{
    public class DashState : IState
    {
        private readonly MiniBossAI _enemyAI;
        private readonly Animator _animator;
        private readonly MiniBossFX _miniBossFX;
        
        private readonly int _dashLeft = Animator.StringToHash("Dash_Left");
        private readonly int _dashRight = Animator.StringToHash("Dash_Right");
        
        private float _dashDistance;
        private float _dashDuration;
        private int _sideToDash;

        public DashState(MiniBossAI enemyAI, Animator animator, MiniBossFX miniBossFX)
        {
            _enemyAI = enemyAI;
            _animator = animator;
            _miniBossFX = miniBossFX;
            _dashDistance = enemyAI.DashDistance;
            _dashDuration = enemyAI.DashDuration;
        }

        public void Enter()
        {
            Dash();
            _miniBossFX.EnableDashParticle();
            _animator.Play(_sideToDash == 0 ? _dashLeft : _dashRight);
            
            // Realtime Testing Variables
            _dashDistance = _enemyAI.DashDistance;
            _dashDuration = _enemyAI.DashDuration;
            // Realtime Testing Variables
            
        }

        public void Execute()
        {
        }

        public void Exit()
        {
            _miniBossFX.DisableDashParticle();
        }
        
        private void Dash()
        {
            _sideToDash = Random.Range(0, 2);
            // int doubledash = Random.Range(0, 2);
            Vector3 dashDirection = _sideToDash == 0 ? _enemyAI.transform.right : -_enemyAI.transform.right;
            Debug.Log("SideToDash: " + (_sideToDash == 0 ? "right" : "left"));
            Vector3 endPosition = _enemyAI.transform.position + dashDirection * _dashDistance;
            
            _enemyAI.transform.DOMove(endPosition, _dashDuration).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                // if (doubledash == 1)
                // {
                //     Vector3 dashDirection = _sideTodash == 0 ? _enemyAI.transform.right : -_enemyAI.transform.right;
                //     Vector3 endPosition = _enemyAI.transform.position + dashDirection * 30f;
                //     _enemyAI.transform.DOMove(endPosition, 2f).SetEase(Ease.OutQuad).OnComplete(() =>
                //     {
                //         _enemyAI.StateMachine.Enter<IdleState>();
                //     });
                // }
                // else
                // {
                //     _enemyAI.StateMachine.Enter<IdleState>();
                // }
                
                _enemyAI.StateMachine.Enter<IdleState>();
            });
        }
    }
}