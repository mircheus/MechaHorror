using Game.Scripts.Enemies._BaseEnemy;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Utilities
{
    public class TargetInstaller : MonoInstaller
    {
        [SerializeField] private Transform playerTarget;
        [SerializeField] private EnemyAI enemyAIPrefab;
        
        public override void InstallBindings()
        {
            Container.Bind<Transform>().FromInstance(playerTarget).AsSingle();
            Container.BindFactory<EnemyAI, EnemyAI.Factory>().FromComponentInNewPrefab(enemyAIPrefab);
        }
    }
}
