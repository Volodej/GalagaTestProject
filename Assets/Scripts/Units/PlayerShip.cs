using System;
using Installers;
using UniRx;
using UnityEngine;
using UserInput;
using Zenject;

namespace Units
{
    public class PlayerShip : MonoBehaviour
    {
        public IObservable<Unit> PlayerShipDestroyedStream => _playerShipDestroyedSubject;

        [SerializeField] private Damageable _damageable;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Vector2 _startPosition;

        [SerializeField, Range(0, 300)]
        private float _maxPositionExtend;

        [Space]
        [SerializeField, Range(50, 200)]
        public float _movementSpeed = 100;

        [SerializeField, Range(0.1f, 2)]
        public float _shootingCooldown = 0.5f;

        [SerializeField, Range(50, 400)]
        private float _playerRocketSpeed = 200;

        private readonly Subject<Unit> _playerShipDestroyedSubject = new Subject<Unit>();
        private readonly Subject<Unit> _fireSubject = new Subject<Unit>();

        private IUserInput _userInput;
        private Rigidbody2D _rigidBody;
        private Projectile.Pool _projectilesPool;
        private Explosion.Pool _explosionsPool;

        [Inject]
        public void Initialize(IUserInput userInput,
            [Inject(Id = Identifiers.PlayerProjectile)]
            Projectile.Pool projectilesPool,
            [Inject(Id = Identifiers.PlayerExplosion)]
            Explosion.Pool explosionsPool)
        {
            _userInput = userInput;
            _projectilesPool = projectilesPool;
            _explosionsPool = explosionsPool;
        }

        public void PlaceShipOnScene()
        {
            gameObject.SetActive(true);
            transform.position = _startPosition;
        }

        private void OnFire()
        {
            _fireSubject.OnNext(Unit.Default);
        }

        private void Fire()
        {
            _projectilesPool.Spawn(Vector2.up * _playerRocketSpeed, transform.position);
            _audioSource.time = 0;
            _audioSource.Play();
        }

        private void Move(float velocityFactor)
        {
            var position = _rigidBody.position;
            var positionShift = velocityFactor * _movementSpeed * Time.deltaTime;
            var newXPosition = Mathf.Clamp(
                position.x + positionShift,
                _startPosition.x - _maxPositionExtend,
                _startPosition.x + _maxPositionExtend);
            var newPosition = new Vector2(newXPosition, position.y);
            _rigidBody.MovePosition(newPosition);
        }

        private void DestroyShip()
        {
            gameObject.SetActive(false);
            _explosionsPool.Spawn(transform.position);
            _playerShipDestroyedSubject.OnNext(Unit.Default);
        }

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _damageable.GotHit += DestroyShip;
            _fireSubject.Sample(TimeSpan.FromSeconds(_shootingCooldown))
                .Subscribe(_ => Fire());
        }

        private void OnEnable()
        {
            _userInput.Moved += Move;
            _userInput.Fired += OnFire;
        }

        private void OnDisable()
        {
            _userInput.Moved -= Move;
            _userInput.Fired -= OnFire;
        }
    }
}