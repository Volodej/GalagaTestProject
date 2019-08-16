using UnityEngine;

namespace Units.Movement
{
    public class JoinFormationStrategy : MovementActionStrategy
    {
        private const float TIME_TO_JOIN = 1;

        private readonly Transform _cell;
        private readonly float _startTime;
        private readonly Vector2 _initialPosition;
        private readonly float _initialRotation;

        public JoinFormationStrategy(Rigidbody2D rigidbody, Transform cell) : base(rigidbody)
        {
            _cell = cell;
            _startTime = Time.time;
            _initialPosition = rigidbody.position;
            var rotation = rigidbody.rotation%360;
            _initialRotation = rotation >= 180 ? rotation - 360 : rotation;
        }

        protected override (Vector2 position, float angle) GetNewTransform()
        {
            var time = (Time.time - _startTime)/TIME_TO_JOIN;
            var newAngle = Mathf.Lerp(_initialRotation, 0, time);
            var newPosition = Vector2.Lerp(_initialPosition, _cell.position, time);
            return (newPosition, newAngle);
        }

        protected override bool CheckIfFinished() => Time.time - _startTime > TIME_TO_JOIN;
    }
}