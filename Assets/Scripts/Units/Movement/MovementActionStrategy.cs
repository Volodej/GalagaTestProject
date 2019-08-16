using System;
using UnityEngine;

namespace Units.Movement
{
    public abstract class MovementActionStrategy
    {
        public event Action Finished = () => { };

        protected readonly Rigidbody2D _rigidbody;

        protected MovementActionStrategy(Rigidbody2D rigidbody)
        {
            _rigidbody = rigidbody;
        }

        public void Tick()
        {
            var (position, angle) = GetNewTransform();
            _rigidbody.position = position;
            _rigidbody.rotation = angle;
            if (CheckIfFinished())
                Finished();
        }

        protected abstract (Vector2 position, float angle) GetNewTransform();
        protected abstract bool CheckIfFinished();

        public void Cancel()
        {
            Finished();
        }
    }
}