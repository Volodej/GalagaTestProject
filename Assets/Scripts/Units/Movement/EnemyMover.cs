using System;
using System.Threading.Tasks;
using Settings;
using UniRx;
using UnityEngine;

namespace Units.Movement
{
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Transform _player;
        [SerializeField] private Transform _formationCell;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _rotationSpeed;

        private MovementActionStrategy _currentStrategy;

        public void Setup(float speed, float rotationSpeed, Transform player, Transform formationCell)
        {
            _speed = speed;
            _rotationSpeed = rotationSpeed;
            _player = player;
            _formationCell = formationCell;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public async Task DoMovement(MovementCommandsQueue movementCommandsQueue)
        {
            transform.SetParent(null);

            foreach (var movementAction in movementCommandsQueue.Actions)
            {
                if (enabled && gameObject.activeInHierarchy)
                    await DoAction(movementAction);
            }

            _currentStrategy = null;

            if (enabled && gameObject.activeInHierarchy)
            {
                transform.SetParent(_formationCell);
                transform.localRotation = Quaternion.identity;
                transform.localPosition = Vector3.zero;
            }
        }

        private void Update()
        {
            _currentStrategy?.Tick();
        }

        private Task DoAction(MovementAction action)
        {
            var strategy = GetStrategy(action);
            var tcs = new TaskCompletionSource<Unit>();
            strategy.Finished += () => tcs.TrySetResult(Unit.Default);
            _currentStrategy = strategy;
            return tcs.Task;
        }

        private MovementActionStrategy GetStrategy(MovementAction action)
        {
            switch (action.Type)
            {
                case MovementType.StartPoint:
                    return new StartPointStrategy(_rigidbody, action.Point, _formationCell);
                case MovementType.MoveToPoint:
                    return new MoveToPointStrategy(_rigidbody, action.Point, _speed, _rotationSpeed);
                case MovementType.MoveToPlayer:
                    return new MoveToPlayerStrategy(_rigidbody, _player, action.Point, _speed, _rotationSpeed);
                case MovementType.MoveToFormation:
                    return new MoveToFormationStrategy(_rigidbody, _formationCell, _speed, _rotationSpeed);
                case MovementType.JoinFormation:
                    return new JoinFormationStrategy(_rigidbody, _formationCell);
                case MovementType.GoOffscreen:
                    return new MoveOffscreenStrategy(_rigidbody, _speed, _rotationSpeed);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnDisable()
        {
            _currentStrategy?.Cancel();
        }
    }
}