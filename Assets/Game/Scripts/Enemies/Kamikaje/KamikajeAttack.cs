using Game.Scripts.Enemies._BaseEnemy;
using Hertzole.GoldPlayer;
using UnityEngine;

namespace Game.Scripts.Enemies.Kamikaje
{
    public class KamikajeAttack : BaseEnemyAttack
    {
        [Header("References: ")]
        [SerializeField] private SphereCollider explosionCollider;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out GoldPlayerController playerController))
            {
                Debug.Log("Damage Player");
            }
        }
    }
}