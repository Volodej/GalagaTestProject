using System;
using Units;
using UnityEngine;

namespace Settings
{
    [Serializable]
    public class FormationLineSettings
    {
        public EnemyType EnemyType = EnemyType.Bee;

        [Range(1, 10)] 
        public int EnemiesInLine = 6;

        public MovementCommandsQueue MovementCommandsQueue;
        public float TimeToSpawn = 4;
    }
}