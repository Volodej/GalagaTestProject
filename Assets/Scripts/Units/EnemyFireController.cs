using UnityEngine;
using Random = UnityEngine.Random;

namespace Units
{
    public class EnemyFireController : MonoBehaviour
    {
        [SerializeField] private Transform _playerShipTransform;
        [SerializeField] private Projectile.Pool _projectilesPool;
        [SerializeField] private int _shotsInBurst;
        [SerializeField] private float _burstDelay;
        [SerializeField] private float _shotsDelay;
        [SerializeField] private float _projectileSpeed;

        [SerializeField] private float _timeToNextShot;
        [SerializeField] private float _firedInBurst;

        public void Setup(Transform playerShipTransform, Projectile.Pool projectilesPool, int shotsInBurst, float burstDelay,
            float shotsDelay, float projectileSpeed)
        {
            _playerShipTransform = playerShipTransform;
            _projectilesPool = projectilesPool;
            _shotsInBurst = shotsInBurst;
            _burstDelay = burstDelay;
            _shotsDelay = shotsDelay;
            _projectileSpeed = projectileSpeed;
                
            _timeToNextShot = burstDelay + Random.Range(0, burstDelay);
            _firedInBurst = 0;
        }

        private void Update()
        {
            _timeToNextShot -= Time.deltaTime;
            if (_timeToNextShot <= 0)
            {
                FireToPlayer();
                _firedInBurst++;
                _timeToNextShot = _shotsDelay;
            }

            if (_firedInBurst >= _shotsInBurst)
            {
                _firedInBurst = 0;
                _timeToNextShot = _burstDelay;
            }
        }

        private void FireToPlayer()
        {
            var position = transform.position;
            var playerDirection = (_playerShipTransform.position - position).normalized;
            _projectilesPool.Spawn(playerDirection * _projectileSpeed, position);
        }
    }
}