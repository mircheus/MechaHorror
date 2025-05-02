using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Enemies._BaseEnemy;
using UnityEngine;
using VFavorites.Libs;

namespace Game.Scripts.Enemies.MiniBoss.States
{
    public class ShieldState : IState
    {
        private readonly MiniBossAI _enemyAI;
        private readonly Shield _shield;
        private readonly bool _isTimeBased;
        private readonly float _timeDuration;
        private bool _isDamageBased;
        private int _damageThreshold;
        private int _currentDamage;
        private Coroutine _shieldCoroutine;
        
        public ShieldState(MiniBossAI enemyAI, Shield shield)
        {
            _enemyAI = enemyAI;
            _shield = shield;
            _isTimeBased = enemyAI.IsTimeBased;
            _timeDuration = enemyAI.ShieldDuration;
            _isDamageBased = enemyAI.IsDamageBased;
            _damageThreshold = enemyAI.DamageThreshold;
        }
        
        public void Enter()
        {
            if (_isTimeBased)
            {
                ExecuteTimeBasedBehaviour();
            }
            
            if (_isDamageBased)
            {
                _shield.ActivateShield();
                _shield.ShieldHit += OnShieldHit;
                _currentDamage = 0;
            }
        }

        public void Execute()
        {
        }

        public void Exit()
        {
            if (_isDamageBased)
            {
                _shield.ShieldHit -= OnShieldHit;
            }
        }

        private void ExecuteTimeBasedBehaviour()
        {
            if(_shieldCoroutine != null)
            {
                _enemyAI.StopCoroutine(_shieldCoroutine);
            }
                
            _shieldCoroutine = _enemyAI.StartCoroutine(ActivateShieldCoroutine());
        }

        private void OnShieldHit()
        {
            _currentDamage++;
            // Debug.Log("Shield hit! Current damage: " + _currentDamage + "/" + _damageThreshold + "");
            
            if (_currentDamage >= _damageThreshold)
            {
                _shield.DeactivateShield();
                _shield.ShieldHit -= OnShieldHit;
                _enemyAI.StateMachine.Enter<IdleState>();
            }
        }

        private IEnumerator ActivateShieldCoroutine()
        {
            _shield.ActivateShield();
            yield return new WaitForSeconds(_timeDuration);
            _shield.DeactivateShield();
            _enemyAI.StateMachine.Enter<IdleState>();
        }
    }
}