using System;
using System.Collections.Generic;
using Units;

namespace Settings
{
    [Serializable]
    public class EnemySettings
    {
        public float Speed = 100;
        public float RotationSpeed = 90;
        public EnemyShip Prefab;
        public List<MovementCommandsQueue> AttackCommandQueues;
        public int ShotsInBurst = 1;
        public float BurstDelay = 10;
        public float ShotsDelay = 2;
        public float ProjectileSpeed = 150;
        public int PointsForEnemy = 200;
    }
}