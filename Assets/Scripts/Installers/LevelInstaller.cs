using LifeCycle.Level;
using LifeCycle.Level.States;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private LevelSettings _levelSettings;
        
        public override void InstallBindings()
        {
            // Level management
            Container.Bind<LevelLifeCycleTransitionManager>().AsSingle();
            Container.Bind<LevelLifeCycleStateMachine>().AsSingle();
            Container.Bind<LevelContext>().AsSingle();
            Container.BindInstance(_levelSettings).AsSingle();
            Container.QueueForInject(GetComponent<LevelStarter>());
            
            // States
            Container.Bind<DivingWaveState>().AsSingle();
            Container.Bind<IncomingWaveState>().AsSingle();
            Container.Bind<IntroState>().AsSingle();
            Container.Bind<LevelEndState>().AsSingle();
            Container.Bind<WaitingState>().AsSingle();
        }
    }
}