using System;
using UnityEngine;
using Zenject;

namespace Units
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        public event Action<Projectile> Destroyed = _ => { };

        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private float _lifeTime = 5;
        [SerializeField] private Vector2 _velocity;
        private float _startTime;

        public void Setup(Vector2 velocity, Vector2 position)
        {
            _velocity = velocity;
            _startTime = Time.time;
            _rigidbody.rotation = Vector2.SignedAngle(_velocity, Vector2.up);
            _rigidbody.position = position;
        }

        private void Update()
        {
            if (!(Time.time - _startTime < _lifeTime))
            {
                Destroy();
                return;
            }

            var newPosition = _rigidbody.position + _velocity * Time.deltaTime;
            _rigidbody.MovePosition(newPosition);
        }

        private void OnTriggerEnter(Collider other)
        {
            var isCorrectLayer = (_targetLayer.value & (1 << other.gameObject.layer)) != 0;
            if (!isCorrectLayer)
                return;

            var damageable = other.GetComponent<Damageable>();
            if (damageable == null)
                return;

            damageable.Hit();
            Destroy();
        }

        private void Destroy()
        {
            gameObject.SetActive(false);
            Destroyed(this);
        }

        /// <summary>
        /// Projectiles pool. First parameter - velocity, second - position.
        /// </summary>
        public class Pool : MonoMemoryPool<Vector2, Vector2, Projectile>
        {
            protected override void Reinitialize(Vector2 velocity, Vector2 position, Projectile projectile)
            {
                projectile.Setup(velocity, position);
            }

            protected override void OnSpawned(Projectile projectile)
            {
                projectile.Destroyed += ReturnToPool;
                base.OnSpawned(projectile);
            }

            protected override void OnDespawned(Projectile projectile)
            {
                projectile.Destroyed -= ReturnToPool;
                base.OnDespawned(projectile);
            }

            private void ReturnToPool(Projectile projectile)
            {
                Despawn(projectile);
            }
        }
    }
}