using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UIElements
{
    public class ControlsPanel : MonoBehaviour
    {
        public event Action Fired = () => {};
        public event Action<float> Moved = _ => {};
        
        [SerializeField] private FixedJoystick _movementJoystick;
        [SerializeField] private Button FireButton;

        public void OnFire()
        {
            Fired();
        }

        private void Update()
        {
            Moved(_movementJoystick.Horizontal);
        }
    }
}