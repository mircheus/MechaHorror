using System;
using System.Collections.Generic;
using Game.Scripts.Enemies.FourLegEnemy.States;

namespace Game.Scripts.Enemies.FourLegEnemy
{
    public class FourLegEnemy : EnemyBase
    {
        protected override Dictionary<Type, IState> GetStates()
        {
            return new Dictionary<Type, IState>
            {
                { typeof(IdleState), new IdleState(enemyAI) },
                { typeof(AttackState), new AttackState(enemyAI) },
                { typeof(DeadState), new DeadState() }
            };
        }
    }
}