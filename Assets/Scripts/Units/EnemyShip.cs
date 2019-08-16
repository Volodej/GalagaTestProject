using System;
using System.Threading.Tasks;
using Installers;
using LifeCycle.Level;
using Settings;
using UniRx;
using Units.Movement;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Units
{
    public class EnemyShip : MonoBehaviour
    {
        private event Action<EnemyShip> Destroyed = _ => { };

        [SerializeField] private EnemyMover _enemyMover;
        [SerializeField] private EnemyFireController _fireController;
        [SerializeField] private Damageable _damageable;

        private readonly Subject<EnemyShip> _destroyedSubject = new Subject<EnemyShip>();
        private EnemySettings _enemySettings;
        private PlayerShip _playerShip;

        private Projectile.Pool _projectilesPool;
        public IObservable<EnemyShip> DestroyedStream => _destroyedSubject;
        public bool IsAlive { get; private set; }
        public int PointsCont => _enemySettings.PointsForEnemy;

        [Inject]
        public void Initialize(PlayerShip playerShip, [Inject(Id = Identifiers.EnemyProjectile)] Projectile.Pool projectilesPool)
        {
            _playerShip = playerShip;
            _projectilesPool = projectilesPool;
            _damageable.GotHit += OnHit;
        }

        public Task StartAttack()
        {
            return _enemyMover.DoMovement(GetRandomAttackQueue());
        }
        
        public async Task StartMovement(MovementCommandsQueue lineMovementCommandsQueue)
        {
            _fireController.enabled = true;
            await _enemyMover.DoMovement(lineMovementCommandsQueue);
            _fireController.enabled = false;
        }

        private void Setup(EnemySettings enemySettings, Transform formationCell, LevelSettings levelSettings)
        {
            _enemySettings = enemySettings;
            var speed = levelSettings.SpeedMultiplier * _enemySettings.Speed;
            var rotationSpeed = levelSettings.SpeedMultiplier * _enemySettings.RotationSpeed;
            _enemyMover.Setup(speed, rotationSpeed, _playerShip.transform, formationCell);

            var shotsInBurst = Mathf.RoundToInt(_enemySettings.ShotsInBurst * levelSettings.ShotsNumberMultiplier);
            var burstDelay = _enemySettings.BurstDelay * levelSettings.FireRateMultiplier;
            var shotsDelay = _enemySettings.ShotsDelay * levelSettings.FireRateMultiplier;
            _fireController.Setup(_playerShip.transform, _projectilesPool, shotsInBurst, burstDelay, shotsDelay,
                _enemySettings.ProjectileSpeed);
            
            IsAlive = true;
            gameObject.SetActive(true);
        }

        private MovementCommandsQueue GetRandomAttackQueue()
        {
            return _enemySettings.AttackCommandQueues[Random.Range(0, _enemySettings.AttackCommandQueues.Count)];
        }

        private void OnHit()
        {
            IsAlive = false;
            _destroyedSubject.OnNext(this);
            Destroyed(this);
        }

        public class Pool : MonoMemoryPool<Transform, LevelSettings, EnemyShip>
        {
            private readonly EnemySettings _enemySettings;

            [Inject]
            public Pool(EnemySettings enemySettings)
            {
                _enemySettings = enemySettings;
            }
            
            protected override void Reinitialize(Transform formationCell, LevelSettings levelSettings, EnemyShip ship)
            {
                ship.Setup(_enemySettings, formationCell, levelSettings);
            }

            protected override void OnSpawned(EnemyShip ship)
            {
                ship.Destroyed += ReturnToPool;
                base.OnSpawned(ship);
            }

            protected override void OnDespawned(EnemyShip ship)
            {
                ship.Destroyed -= ReturnToPool;
                base.OnDespawned(ship);
            }

            private void ReturnToPool(EnemyShip projectile)
            {
                Despawn(projectile);
            }
        }
    }
}