using System;
using UnityEngine;

namespace Settings
{
    [Serializable]
    public class FormationSettings
    {
        public Bounds AllowedFormationBounds = new Bounds(Vector2.zero, new Vector2(500,300));
        public int UnitSize = 9;
        public int MinUnitsDistance = 2;
        public int MaxUnitsDistance = 20;
        public float SqueezeExpandCycleTime = 10;
    }
}