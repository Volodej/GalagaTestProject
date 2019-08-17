using UnityEngine;

namespace Units.Movement
{
    public class MoveOffscreenStrategy : MoveTowardsStrategy
    {
        public MoveOffscreenStrategy(Rigidbody2D rigidbody, float speed, float rotationSpeed) : base(rigidbody, speed, rotationSpeed)
        {
        }

        protected override Vector2 GetPoint() => _rigidbody.position + Vector2.down * 500;

        protected override float GetFinishDistance() => 10;

        protected override bool CheckIfFinished()
        {
            var position = _rigidbody.position;
            var isFinished = position.y < -220;
            if(isFinished)
                _rigidbody.position = new Vector2(position.x, 220);

            return isFinished;
        }
    }
}