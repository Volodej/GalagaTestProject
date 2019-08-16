using UnityEngine;

namespace Units.Movement
{
    public class StartPointStrategy : MovementActionStrategy
    {
        private readonly Vector2 _startPoint;
        private readonly Transform _targetCell;

        public StartPointStrategy(Rigidbody2D rigidbody, Vector2 startPoint, Transform targetCell) : base(rigidbody)
        {
            _startPoint = startPoint;
            _targetCell = targetCell;
        }

        protected override (Vector2 position, float angle) GetNewTransform()
        {
            var angle = Vector2.SignedAngle(Vector2.up, (Vector2) _targetCell.position - _startPoint);
            return (_startPoint, angle);
        }

        protected override bool CheckIfFinished() => true;
    }
}