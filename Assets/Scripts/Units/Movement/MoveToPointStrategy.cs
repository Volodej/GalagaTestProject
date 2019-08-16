using UnityEngine;

namespace Units.Movement
{
    public class MoveToPointStrategy : MoveTowardsStrategy
    {
        private readonly Vector2 _targetPoint;

        public MoveToPointStrategy(Rigidbody2D rigidbody, Vector2 targetPoint, float speed, float rotationSpeed) 
            : base(rigidbody, speed, rotationSpeed)
        {
            _targetPoint = targetPoint;
        }

        protected override Vector2 GetPoint() => _targetPoint;
        protected override float GetFinishDistance() => 10;
    }
}