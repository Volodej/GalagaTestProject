using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Units
{
    [RequireComponent(typeof(Animation))]
    public class Explosion : MonoBehaviour
    {
        private event Action<Explosion> Destroyed = _ => { };
        
        [SerializeField] private Animation _animation;
        [SerializeField] private AnimationClip _animationClip;
        [SerializeField] private AudioSource _audioSource;
        
        private void Setup(Vector2 position)
        {
            transform.position = position;
            _animation.Rewind();
            _animation.Play();
            _audioSource.time = 0;
            _audioSource.Play();
            Observable.Timer(TimeSpan.FromSeconds(_animationClip.length))
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