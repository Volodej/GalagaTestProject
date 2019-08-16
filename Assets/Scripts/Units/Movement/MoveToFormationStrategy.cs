using UnityEngine;

namespace Units.Movement
{
    public class MoveToFormationStrategy : MoveTowardsStrategy
    {
        private readonly Transform _cell;

        public MoveToFormationStrategy(Rigidbody2D rigidbody, Transform cell, float speed, float rotationSpeed) 
            : base(rigidbody, speed, rotationSpeed)
        {
            _cell = cell;
        }

        protected override Vector2 GetPoint() => _cell.position;

        protected override float GetFinishDistance() => 10;
    }
}