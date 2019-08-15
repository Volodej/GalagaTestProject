using System.Threading.Tasks;
using LifeCycle.Level;
using StateMachines;
using UIElements;
using UnityEngine;

namespace LifeCycle.Game.States
{
    public class LevelState : IAwaitableState<GameStateType>
    {
        public Task RunState()
        {
            return Object.FindObjectOfType<LevelStarter>().RunLevel();
        }

        public GameStateType Type => GameStateType.Level;
        public bool IsExitState => false;
    }
}