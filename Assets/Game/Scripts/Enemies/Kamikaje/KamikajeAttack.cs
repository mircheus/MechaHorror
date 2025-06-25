using Game.Scripts.Enemies._BaseEnemy;
using Game.Scripts.Interfaces;
using Hertzole.GoldPlayer;
using UnityEngine;

namespace Game.Scripts.Enemies.Kamikaje
{
    public class KamikajeAttack : BaseEnemyAttack
    {
        private bool _isAlreadyDamaged = false;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                if (_isAlreadyDamaged == false)
                {
                    _isAlreadyDamaged = true;
                    damageable.TakeDamage(1);
                }
            }
        }
    }
}