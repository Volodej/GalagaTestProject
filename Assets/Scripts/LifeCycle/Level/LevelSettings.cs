using System.Collections.Generic;
using System.Linq;
using Settings;
using UnityEngine;

namespace LifeCycle.Level
{
    public class LevelSettings : MonoBehaviour
    {
        public List<FormationLineSettings> EnemiesLines;
        public float HorizontalMovementTime = 60;
        public float VerticalMovementTime = 300;
        
        [Space]
        public float SpeedMultiplier = 1;
        public float FireRateMultiplier = 1;
        public float ShotsNumberMultiplier = 1;
        
        
        [Space]
        public int ShipsInAttackWave = 3;
        public int AttackWaveDelayTime = 3;
        public int TimeBetweenWaves = 5;
        
        public int TotalEnemiesCount => EnemiesLines.Sum(line => line.EnemiesInLine);
    }
}