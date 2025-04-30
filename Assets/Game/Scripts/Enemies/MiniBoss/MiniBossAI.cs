using System;
using System.Collections.Generic;
using Game.Scripts.Enemies._BaseEnemy;
using Game.Scripts.Enemies.MiniBoss.States;

namespace Game.Scripts.Enemies.MiniBoss 
{
    public class MiniBossAI : EnemyAI
    {
        public override void Init(Dictionary<Type, IState> states)
        {
            base.Init(states);
            StateMachine.Enter<IdleState>();
        }
    }
}