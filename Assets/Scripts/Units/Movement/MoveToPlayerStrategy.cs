using UnityEngine;

namespace Units.Movement
{
    public class MoveToPlayerStrategy : MoveTowardsStrategy
    {
        private readonly Transform _player;
        private readonly Vector2 _positionFromPlayer;

        public MoveToPlayerStrategy(Rigidbody2D rigidbody, Transform player, Vector2 positionFromPlayer, float speed, float rotationSpeed)
            : base(rigidbody, speed, rotationSpeed)
        {
            _player = player;
            _positionFromPlayer = positionFromPlayer;
        }

        protected override Vector2 GetPoint() => (Vector2) _player.position + _positionFromPlayer;
        protected override float GetFinishDistance() => 20;
    }
}