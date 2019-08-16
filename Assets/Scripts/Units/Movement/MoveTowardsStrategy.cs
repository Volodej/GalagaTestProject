using UnityEngine;

namespace Units.Movement
{
    public abstract class MoveTowardsStrategy : MovementActionStrategy
    {
        private const float TIME_TO_INCREASE_ROTATION = 5;
        private const float TIME_TO_MAXIMUM_ROTATION = 5;
        private const float MAX_ADDITIONAL_ROTATION_MULTIPLIER = 5;

        private readonly float _speed;
        private readonly float _rotationSpeed;
        private float _startTime;

        protected MoveTowardsStrategy(Rigidbody2D rigidbody, float speed, float rotationSpeed) : base(rigidbody)
        {
            _speed = speed;
            _rotationSpeed = rotationSpeed;
            _startTime = Time.time;
        }

        protected abstract Vector2 GetPoint();

        protected abstract float GetFinishDistance();

        protected override (Vector2 position, float angle) GetNewTransform()
        {
            var toTargetDirection = GetPoint() - _rigidbody.position;
            var currentDirection = Quaternion.Euler(0, 0, _rigidbody.rotation) * Vector2.up;
            var distanceToTargetAngle = Vector2.SignedAngle(currentDirection, toTargetDirection);
            //var normalizedDistanceToAngle = distanceToTargetAngle >= 0 ? distanceToTargetAngle : 360 + distanceToTargetAngle;
            var currentRotation = _rigidbody.rotation;
            var normalizedRotation = currentRotation >= 0 ? currentRotation : 360 + currentRotation;
            //var distanceToTargetAngle = angleToRotate - normalizedRotation;
            var newAngle =
                Mathf.Sign(distanceToTargetAngle) *
                Mathf.Min(CalculateChangedRotationSpeed() * Time.deltaTime, Mathf.Abs(distanceToTargetAngle)) +
                normalizedRotation;

            var newPosition = _rigidbody.position + (Vector2) (Quaternion.Euler(0, 0, newAngle) * Vector2.up * _speed * Time.deltaTime);

            Debug.DrawRay(newPosition, toTargetDirection);
            Debug.DrawRay(newPosition, currentDirection * 100);

            return (newPosition, newAngle % 360);
        }

        protected override bool CheckIfFinished()
        {
            return (_rigidbody.position - GetPoint()).magnitude < GetFinishDistance();
        }

        private float CalculateChangedRotationSpeed()
        {
            var multiplyFactor = Mathf.Clamp(Time.time - _startTime - TIME_TO_INCREASE_ROTATION, 0, TIME_TO_MAXIMUM_ROTATION) /
                                 TIME_TO_MAXIMUM_ROTATION;
            return (1 + MAX_ADDITIONAL_ROTATION_MULTIPLIER * multiplyFactor) * _rotationSpeed;
        }
    }
}