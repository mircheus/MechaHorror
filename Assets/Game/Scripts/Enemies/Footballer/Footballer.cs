using System;
using System.Collections.Generic;
using Game.Scripts.Enemies._BaseEnemy;
using Game.Scripts.Enemies.Footballer.States;
using UnityEngine;

namespace Game.Scripts.Enemies.Footballer
{
    public class Footballer : BaseEnemy.BaseEnemy
    {
        [Header("Footballer References: ")]
        [SerializeField] private Animator animator;
        [SerializeField] private FootballerAttack footballerAttack;
        
        protected override Dictionary<Type, IState> GetStates()
        {
            return new Dictionary<Type, IState>
            {
                {typeof(AttackState), new AttackState(enemyAI, animator)}
                // { typeof(ChasingState), new ChasingState(enemyAI, animator) },
                // { typeof(DeadState), new DeadState(enemyAI, deathParticle, mechGameObject) }
            };
        }
    }
}