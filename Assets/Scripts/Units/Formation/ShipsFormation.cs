using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LifeCycle.Game;
using LifeCycle.Level;
using Settings;
using UniRx;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Units.Formation
{
    public class ShipsFormation : MonoBehaviour
    {
        private readonly Subject<EnemyShip> _enemyDestroyed = new Subject<EnemyShip>();
        private readonly List<EnemyShip> _enemies = new List<EnemyShip>();

        private LevelSettings _levelSettings;
        private EnemiesSpawner _enemiesSpawner;

        private Bounds _centerBounds;
        private List<FormationCell> _formationCells;
        private Queue<IncomingWaveQueueItem> _incomingWaves;
        private GameSoundController _soundController;

        public IObservable<int> PointsForEnemiesStream => _enemyDestroyed.Select(ship => ship.PointsCont);
        public IObservable<int> LeftEnemiesStream => _enemyDestroyed.Scan(_levelSettings.TotalEnemiesCount, (i, _) => --i);
        public int LeftIncomingWaves => _incomingWaves.Count;

        [Inject]
        public void Initialize(FormationSettings formationSettings, LevelSettings levelSettings, EnemiesSpawner enemiesSpawner,
            GameSoundController soundController)
        {
            _levelSettings = levelSettings;
            _enemiesSpawner = enemiesSpawner;
            _soundController = soundController;

            var maxColumnsCount = levelSettings.EnemiesLines.Max(line => line.EnemiesInLine);

            (_formationCells, _incomingWaves) = BuildFormation(levelSettings.EnemiesLines, maxColumnsCount);
            FormationExpander.AttachTo(gameObject, formationSettings, _formationCells,
                levelSettings.EnemiesLines.Count, maxColumnsCount);

            FormationMover.AttachTo(gameObject, formationSettings, levelSettings, levelSettings.EnemiesLines.Count,
                maxColumnsCount);
        }

        public async Task SpawnEnemiesWave()
        {
            var wave = _incomingWaves.Dequeue();

            var allMovementTasks = new List<Task>(wave.CellsToReach.Count);
            foreach (var cell in wave.CellsToReach)
            {
                var enemy = _enemiesSpawner.Spawn(cell, wave.Line.EnemyType);
                _enemies.Add(enemy);
                enemy.DestroyedStream.Subscribe(_enemyDestroyed);
                var movementTask = enemy.StartIncomingMovement(wave.Line.MovementCommandsQueue);
                allMovementTasks.Add(movementTask);
                await Task.Delay(TimeSpan.FromSeconds(wave.Line.TimeToSpawn / wave.Line.EnemiesInLine));
            }

            await Task.WhenAll(allMovementTasks);
        }

        public async Task StartAttackWave()
        {
            var attackShips = _enemies.Where(ship => ship.IsAlive)
                .OrderBy(_ => Random.Range(0, 1000))
                .Take(_levelSettings.ShipsInAttackWave);

            var allAttackTasks = new List<Task>(_levelSettings.ShipsInAttackWave);
            foreach (var attackShip in attackShips)
            {
                if (!attackShip.IsAlive)
                    continue;

                _soundController.PlayAttackWave();
                allAttackTasks.Add(attackShip.StartAttack());
                await Task.Delay(TimeSpan.FromSeconds(_levelSettings.AttackWaveDelayTime));
            }

            await Task.WhenAll(allAttackTasks);
        }

        private (List<FormationCell> cells, Queue<IncomingWaveQueueItem> waves) BuildFormation(List<FormationLineSettings> enemiesLines,
            int maxRowsCount)
        {
            var incomingWaves = new Queue<IncomingWaveQueueItem>();
            var allCells = new List<FormationCell>(enemiesLines.Sum(line => line.EnemiesInLine));
            for (var lineIndex = 0; lineIndex < enemiesLines.Count; lineIndex++)
            {
                var line = enemiesLines[lineIndex];
                var cells = Enumerable.Range(0, line.EnemiesInLine)
                    .Select(columnIndex =>
                        FormationCell.Create(transform, lineIndex, columnIndex, enemiesLines.Count, line.EnemiesInLine, maxRowsCount))
                    .ToList();
                allCells.AddRange(cells);
                incomingWaves.Enqueue(new IncomingWaveQueueItem(line, cells));
            }

            return (allCells, incomingWaves);
        }
    }
}