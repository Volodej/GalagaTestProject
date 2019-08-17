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
        private Explosion.Pool _explosionsPool;
        public IObservable<EnemyShip> DestroyedStream => _destroyedSubject;
        public bool IsAlive { get; private set; }
        public int PointsCont => _enemySettings.PointsForEnemy;

        [Inject]
        public void Initialize(PlayerShip playerShip,
            [Inject(Id = Identifiers.EnemyProjectile)]
            Projectile.Pool projectilesPool,
            [Inject(Id = Identifiers.EnemyExplosion)]
            Explosion.Pool explosionsPool)
        {
            _playerShip = playerShip;
            _projectilesPool = projectilesPool;
            _explosionsPool = explosionsPool;
            _damageable.GotHit += OnHit;
        }

        public Task StartAttack()
        {
            return StartMovement(GetRandomAttackQueue(), false);
        }

        public Task StartIncomingMovement(MovementCommandsQueue lineMovementCommandsQueue)
        {
            return StartMovement(lineMovementCommandsQueue, true);
        }

        private async Task StartMovement(MovementCommandsQueue lineMovementCommandsQueue, bool delayFire)
        {
            _fireController.AllowFire(delayFire?3:0);
            await _enemyMover.DoMovement(lineMovementCommandsQueue);
            _fireController.RestrictFire();
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
            _explosionsPool.Spawn(transform.position);
            _destroyedSubject.OnNext(this);
            Destroyed(this);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var playerShip = other.GetComponent<PlayerShip>();
            if (playerShip == null)
                return;
            
            playerShip.GetComponent<Damageable>().Hit();
            OnHit();
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
                ship.transform.position = new Vector2(1000, 0);
                ship.Destroyed += ReturnToPool;
                base.OnSpawned(ship);
            }

            protected override void OnDespawned(EnemyShip ship)
            {
                ship.transform.position = new Vector2(1000, 0);
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