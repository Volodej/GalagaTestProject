using Settings;
using Units;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = "EnemiesInstaller", menuName = "Installers/EnemiesInstaller")]
    public class EnemiesInstaller : ScriptableObjectInstaller<EnemiesInstaller>
    {
        [SerializeField] private FormationSettings _formationSettings;
        [SerializeField] private EnemySettings _beeEnemy;
        [SerializeField] private EnemySettings _bossEnemy;
        [SerializeField] private EnemySettings _mothEnemy;
        [SerializeField] private Projectile _enemyProjectilePrefab;
        [SerializeField] private Explosion _enemyExplosionPrefab;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_formationSettings).AsSingle();
            
            Container.BindMemoryPool<Projectile, Projectile.Pool>()
                .WithId(Identifiers.EnemyProjectile)
                .WithInitialSize(10)
                .FromComponentInNewPrefab(_enemyProjectilePrefab)
                .UnderTransformGroup("Projectiles");
            
            Container.BindMemoryPool<EnemyShip, EnemyShip.Pool>()
                .WithId(Identifiers.BeeEnemy)
                .WithInitialSize(10)
                .WithFactoryArguments(_beeEnemy)
                .FromComponentInNewPrefab(_beeEnemy.Prefab)
                .UnderTransformGroup("Enemies");
            
            Container.BindMemoryPool<EnemyShip, EnemyShip.Pool>()
                .WithId(Identifiers.BossEnemy)
                .WithInitialSize(10)
                .WithFactoryArguments(_bossEnemy)
                .FromComponentInNewPrefab(_bossEnemy.Prefab)
                .UnderTransformGroup("Enemies");
            
            Container.BindMemoryPool<EnemyShip, EnemyShip.Pool>()
                .WithId(Identifiers.MothEnemy)
                .WithInitialSize(10)
                .WithFactoryArguments(_mothEnemy)
                .FromComponentInNewPrefab(_mothEnemy.Prefab)
                .UnderTransformGroup("Enemies");

            Container.BindMemoryPool<Explosion, Explosion.Pool>()
                .WithId(Identifiers.EnemyExplosion)
                .WithInitialSize(5)
                .FromComponentInNewPrefab(_enemyExplosionPrefab)
                .UnderTransformGroup("Explosions");
        }
    }
}