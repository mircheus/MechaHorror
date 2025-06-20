using UnityEngine;

namespace Game.Scripts.Enemies._BaseEnemy
{
    public abstract class BaseEnemyAttack : MonoBehaviour
    {
        [SerializeField] protected int damage = 10;
        [SerializeField] protected float attackCooldown = 3f;

        public float AttackCooldown => attackCooldown;

        public virtual void Attack(Transform target)
        {

        }
    }
}