using UIElements;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private HudPanel _hudPanel;
        [SerializeField] private ControlsPanel _controlsPanel;
        [SerializeField] private EnterNamePanel _enterNamePanel;
        [SerializeField] private TopScorePanel _topScorePanel;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_hudPanel).AsSingle();
            Container.BindInstance(_controlsPanel).AsSingle();
            Container.BindInstance(_enterNamePanel).AsSingle();
            Container.BindInstance(_topScorePanel).AsSingle();
        }
    }
}