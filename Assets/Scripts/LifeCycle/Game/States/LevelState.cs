using System.Threading.Tasks;
using LifeCycle.Level;
using StateMachines;
using UIElements;
using UnityEngine;
using UserInput;

namespace LifeCycle.Game.States
{
    public class LevelState : IAwaitableState<GameStateType>
    {
        private readonly IUserInput _userInput;

        public LevelState(IUserInput userInput)
        {
            _userInput = userInput;
        }
        
        public Task RunState()
        {
            _userInput.Enable();
            return Object.FindObjectOfType<LevelStarter>().RunLevel();
        }

        public GameStateType Type => GameStateType.Level;
        public bool IsExitState => false;
    }
}