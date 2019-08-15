using LifeCycle.Game;
using LifeCycle.Game.States;
using TopPlayers;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // Game management
            Container.Bind<GameLifeCycleStateMachine>().AsSingle();
            Container.Bind<GameLifeCycleTransitionManager>().AsSingle();
            Container.Bind<GameContext>().AsSingle();
            Container.Bind<GameSoundController>().FromComponentOn(gameObject).AsSingle();
            Container.QueueForInject(gameObject.GetComponent<GameStarter>());
            
            // Game states
            Container.Bind<LevelSelectionState>().AsSingle();
            Container.Bind<LevelState>().AsSingle();
            Container.Bind<RecordApplyingState>().AsSingle();
            Container.Bind<TopPlayersState>().AsSingle();

            Container.Bind<TopPlayersStorage>().AsSingle();
        }
    }
}