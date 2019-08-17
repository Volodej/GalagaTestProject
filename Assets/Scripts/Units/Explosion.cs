using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Units
{
    [RequireComponent(typeof(Animator))]
    public class Explosion : MonoBehaviour
    {
        
        private event Action<Explosion> Destroyed = _ => { };
        
        [SerializeField] private Animator _animator;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private float _animationTime = 0.3f;
        [SerializeField] private string _stateName = "";
        
        private void Setup(Vector2 position)
        {
            transform.position = position;
            _animator.Play(_stateName);
            _audioSource.time = 0;
            _audioSource.Play();
            Observable.Timer(TimeSpan.FromSeconds(_animationTime))
                .Subscribe(_ => Destroyed(this));
        }
        
        public class Pool : MonoMemoryPool<Vector2, Explosion>
        {
            protected override void Reinitialize(Vector2 position, Explosion explosion)
            {
                explosion.Setup(position);
            }

            protected override void OnSpawned(Explosion explosion)
            {
                explosion.Destroyed += ReturnToPool;
                base.OnSpawned(explosion);
            }

            protected override void OnDespawned(Explosion explosion)
            {
                explosion.Destroyed -= ReturnToPool;
                base.OnDespawned(explosion);
            }

            private void ReturnToPool(Explosion explosion)
            {
                Despawn(explosion);
            }
        }
    }
}