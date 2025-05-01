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
        
        [Header("Weapon Objects:")]
        [SerializeField] private bool isSwordEnabled;
        [SerializeField] private GameObject sword;
        
        private new void Start()
        {
            base.Start();
            
            if (isSwordEnabled)
            {
                sword.SetActive(true);
            }
        }

        protected override Dictionary<Type, IState> GetStates()
        {
            return new Dictionary<Type, IState>
            {
                { typeof(IdleState), new IdleState(enemyAI, animator) },
                { typeof(RangeAttackState), new RangeAttackState((MiniBossAI)enemyAI, animator) },
                { typeof(DashState), new DashState((MiniBossAI)enemyAI, animator) }
            };
        }
    }
}
