using System;
using System.Collections.Generic;
using Game.Scripts.Enemies._BaseEnemy;
using Game.Scripts.Enemies.MiniBoss.States;
using UnityEngine;

namespace Game.Scripts.Enemies.MiniBoss
{
    public class MiniBoss : BaseEnemy
    {
        [SerializeField] private Animator animator;

        protected override Dictionary<Type, IState> GetStates()
        {
            return new Dictionary<Type, IState>
            {
                { typeof(IdleState), new IdleState(enemyAI, animator) },
                { typeof(RangeAttackState), new RangeAttackState(enemyAI, animator) }
            };
        }
    }
}
