using System;
using System.Collections.Generic;
using Game.Scripts.Enemies._BaseEnemy;
using Game.Scripts.Enemies.Kamikaje.States;
using UnityEngine;

namespace Game.Scripts.Enemies.Kamikaje
{
    public class KamikajeAI : EnemyAI
    {
        [Header("Kamikaje Settings: ")] 
        [SerializeField] private float destroyDelay;
        
        public override void Init(Dictionary<Type, IState> states)
        {
            base.Init(states);
            stateMachine.Enter<ChasingState>();
        }
        
        public void SelfDestroy(DeadState deadState)
        {
            Destroy(gameObject, destroyDelay);
        }
    }
}