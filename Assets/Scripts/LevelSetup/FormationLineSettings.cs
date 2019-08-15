using System;
using Units;
using UnityEngine;

namespace LevelSetup
{
    [Serializable]
    public class FormationLineSettings
    {
        public EnemyType EnemyType = EnemyType.Bee;

        [Range(1, 3)] 
        public int LinesCount = 1;

        [Range(3, 10)] 
        public int EnemiesInLine = 6;
    }
}