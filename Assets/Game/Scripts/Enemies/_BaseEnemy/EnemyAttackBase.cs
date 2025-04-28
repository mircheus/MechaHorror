using UnityEngine;

namespace Game.Scripts.Enemies._BaseEnemy
{
    public abstract class EnemyAttackBase : MonoBehaviour
    {
        [SerializeField] protected int damage = 10;
        [SerializeField] protected float attackCooldown = 3f;


        public virtual void Attack(Transform target)
        {

        }
    }
}