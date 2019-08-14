using System;
using UnityEngine;

namespace Units
{
    [Serializable]
    public class PlayerShipSettings
    {
        [Range(1, 20)] 
        public float MovementSpeed;

        [Range(0, 1)] 
        public float ShootingCooldown;

        [Range(1, 40)]
        public float PlayerRocketSpeed;
    }
}