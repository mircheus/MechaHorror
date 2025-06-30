using System;
using System.Collections.Generic;
using Game.Scripts.Enemies._BaseEnemy;
using Game.Scripts.Enemies.Footballer.States;
using UnityEngine;

namespace Game.Scripts.Enemies.Footballer
{
    public class FootballerAI : EnemyAI
    {
        [Header("FootballerAI References: ")]
        [SerializeField] private EnemySight enemySight;
        
        public EnemySight EnemySight => enemySight;
        
        private bool _isTargetVisible;

        private void OnEnable()
        {
            enemySight.LineOfSightChanged += OnLineOfSightChanged;
        }

        private void OnDisable()
        {
            enemySight.LineOfSightChanged -= OnLineOfSightChanged;
        }

        public override void Init(Dictionary<Type, IState> states)
        {
            base.Init(states);
            stateMachine.Enter<IdleState>();
        }

        private void OnLineOfSightChanged(bool isTargetVisible)
        {
            if (isTargetVisible == _isTargetVisible)
            {
                return;
            }
            
            _isTargetVisible = isTargetVisible;
            Debug.Log("FootballerAI: OnLineOfSightChanged called. Is target visible: " + _isTargetVisible);

            if (_isTargetVisible)
            {
                stateMachine.Enter<AttackState>();
            }
            else
            {
                stateMachine.Enter<IdleState>();
            }
        }
    }
}