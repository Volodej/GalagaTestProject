using System;
using Installers;
using UnityEngine;
using UserInput;
using Zenject;

namespace Units
{
    public class PlayerShip : MonoBehaviour
    {
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

        private IUserInput _userInput;
        private Explosion _playerExplosion;
        private Rigidbody2D _rigidBody;
        private Projectile.Pool _projectilesPool;

        [Inject]
        public void Initialize(IUserInput userInput, Explosion playerExplosion,
            [Inject(Id = Identifiers.PlayerProjectile)]
            Projectile.Pool projectilesPool)
        {
            _userInput = userInput;
            _playerExplosion = playerExplosion;
            _projectilesPool = projectilesPool;
        }

        public void PlaceShipOnScene()
        {
            gameObject.SetActive(true);
            transform.position = _startPosition;
        }

        private void Fire()
        {
            _projectilesPool.Spawn(Vector2.up * _playerRocketSpeed, transform.position);
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

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _userInput.Moved += Move;
            _userInput.Fired += Fire;
        }

        private void OnDisable()
        {
            _userInput.Moved -= Move;
            _userInput.Fired -= Fire;
        }
    }
}