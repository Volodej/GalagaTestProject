using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Installers;
using LifeCycle.Level;
using Zenject;

namespace Units.Formation
{
    public class EnemiesSpawner
    {
        private readonly LevelSettings _levelSettings;
        private readonly EnemyShip.Pool _beePool;
        private readonly EnemyShip.Pool _bossPool;
        private readonly EnemyShip.Pool _mothPool;

        [Inject]
        public EnemiesSpawner(LevelSettings levelSettings,
            [Inject(Id = Identifiers.BeeEnemy)]
            EnemyShip.Pool beePool,
            [Inject(Id = Identifiers.BossEnemy)]
            EnemyShip.Pool bossPool,
            [Inject(Id = Identifiers.MothEnemy)]
            EnemyShip.Pool mothPool)
        {
            _levelSettings = levelSettings;
            _beePool = beePool;
            _bossPool = bossPool;
            _mothPool = mothPool;
        }

        public EnemyShip Spawn(FormationCell cell, EnemyType enemyType)
        {
            var pool = SelectPool(enemyType);
            return pool.Spawn(cell.transform, _levelSettings);
        }

        public List<EnemyShip> Spawn(IncomingWaveQueueItem wave)
        {
            var pool = SelectPool(wave.Line.EnemyType);
            return wave.CellsToReach.Select(cell => pool.Spawn(cell.transform, _levelSettings)).ToList();
        }

        private EnemyShip.Pool SelectPool(EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.Bee:
                    return _beePool;
                case EnemyType.Boss:
                    return _bossPool;
                case EnemyType.Moth:
                    return _mothPool;
                default:
                    throw new ArgumentOutOfRangeException(nameof(enemyType), enemyType, null);
            }
        }
    }
}