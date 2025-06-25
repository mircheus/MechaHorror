using System;
using System.Collections.Generic;
using Game.Scripts.Enemies._BaseEnemy;
using Game.Scripts.Enemies.Footballer.States;

namespace Game.Scripts.Enemies.Footballer
{
    public class FootballerAI : EnemyAI
    {
        public override void Init(Dictionary<Type, IState> states)
        {
            base.Init(states);
            stateMachine.Enter<AttackState>();
            stateMachine.Enter<AttackState>();
            stateMachine.Enter<AttackState>();
        }
    }
}