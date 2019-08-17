using System;
using UIElements;

namespace UserInput
{
    public class JoystickInput : IUserInput
    {
        public event Action Fired
        {
            add => _controlsPanel.Fired += value;
            remove => _controlsPanel.Fired -= value;
        }

        public event Action<float> Moved
        {
            add => _controlsPanel.Moved += value;
            remove => _controlsPanel.Moved -= value;
        }

        public void Enable() => _controlsPanel.gameObject.SetActive(true);

        public void Disable() => _controlsPanel.gameObject.SetActive(false);

        private readonly ControlsPanel _controlsPanel;

        public JoystickInput(ControlsPanel controlsPanel)
        {
            _controlsPanel = controlsPanel;
        }
    }
}