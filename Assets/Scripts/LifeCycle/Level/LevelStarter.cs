using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace LifeCycle.Level
{
    public class LevelStarter : MonoBehaviour
    {
        private LevelLifeCycleStateMachine _stateMachine;

        [Inject]
        public void Initialize(LevelLifeCycleStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public Task RunLevel() => _stateMachine.StartWithState(LevelStateType.Intro);
    }
}