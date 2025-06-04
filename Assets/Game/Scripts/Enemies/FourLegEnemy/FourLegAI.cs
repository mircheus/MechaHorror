using System;
using System.Collections.Generic;
using Game.Scripts.Enemies._BaseEnemy;
using Game.Scripts.Enemies.FourLegEnemy.States;
using UnityEngine;

namespace Game.Scripts.Enemies.FourLegEnemy
{
    public class FourLegAI : EnemyAI
    {
        public override void Init(Dictionary<Type, IState> states)
        {
            base.Init(states);
            stateMachine.Enter<IdleState>();
        }
    }
}