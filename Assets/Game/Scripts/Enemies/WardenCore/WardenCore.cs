using System;
using System.Collections.Generic;
using Game.Scripts.Enemies._BaseEnemy;
using Game.Scripts.Enemies.FourLegEnemy.States;

namespace Game.Scripts.Enemies.WardenCore
{
    public class WardenCore : BaseEnemy
    {
        protected override Dictionary<Type, IState> GetStates()
        {
            return new Dictionary<Type, IState>
            {
                // { typeof(IdleState), new IdleState(enemyAI) },
                { typeof(ChaseState), new ChaseState(enemyAI) }
                // { typeof(AttackState), new AttackState(enemyAI) },
            };
        }
    }
}