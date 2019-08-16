using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        private FormationSettings _formationSettings;
        private LevelSettings _levelSettings;
        private EnemiesSpawner _enemiesSpawner;

        private Bounds _centerBounds;
        private List<FormationCell> _formationCells;
        private FormationExpander _formationExpander;
        private FormationMover _formationMover;
        private Queue<IncomingWaveQueueItem> _incomingWaves;

        public IObservable<int> PointsForEnemiesStream => _enemyDestroyed.Select(ship => ship.PointsCont);
        public IObservable<int> LeftEnemiesStream => _enemyDestroyed.Scan(_levelSettings.TotalEnemiesCount, (i, _) => --i);
        public int LeftIncomingWaves => _incomingWaves.Count;

        [Inject]
        public void Initialize(FormationSettings formationSettings, LevelSettings levelSettings, EnemiesSpawner enemiesSpawner)
        {
            _formationSettings = formationSettings;
            _levelSettings = levelSettings;
            _enemiesSpawner = enemiesSpawner;

            var maxColumnsCount = levelSettings.EnemiesLines.Max(line => line.EnemiesInLine);

            (_formationCells, _incomingWaves) = BuildFormation(levelSettings.EnemiesLines, maxColumnsCount);
            _formationExpander = FormationExpander.AttachTo(gameObject, formationSettings, _formationCells,
                levelSettings.EnemiesLines.Count, maxColumnsCount);

            _formationMover = FormationMover.AttachTo(gameObject, formationSettings, levelSettings, levelSettings.EnemiesLines.Count,
                maxColumnsCount);
        }

        public async Task SpawnEnemiesWave()
        {
            var wave = _incomingWaves.Dequeue();
            //var spawnedEnemies = _enemiesSpawner.Spawn(wave);
            //_enemies.AddRange(spawnedEnemies);
            //
            Debug.Log($"SpawnEnemiesWave: i: {wave.Line.EnemiesInLine}; ");

            var allMovementTasks = new List<Task>(wave.CellsToReach.Count);
            foreach (var cell in wave.CellsToReach)
            {
                var enemy = _enemiesSpawner.Spawn(cell, wave.Line.EnemyType);
                _enemies.Add(enemy);
                enemy.DestroyedStream.Subscribe(_enemyDestroyed);
                var movementTask = enemy.StartMovement(wave.Line.MovementCommandsQueue);
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