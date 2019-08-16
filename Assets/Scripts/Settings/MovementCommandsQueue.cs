using System;
using System.Collections.Generic;
using Units.Movement;
using UnityEngine;
using Zenject;

namespace Settings
{
    [CreateAssetMenu(menuName = "Settings/MovementStrategy")]
    public class MovementCommandsQueue : ScriptableObjectInstaller<MovementCommandsQueue>
    {
        public List<MovementAction> Actions;
    }
}