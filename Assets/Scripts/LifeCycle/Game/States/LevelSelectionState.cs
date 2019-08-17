using System;
using System.Collections;
using System.Threading.Tasks;
using Settings;
using StateMachines;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LifeCycle.Game.States
{
    public class LevelSelectionState : IAwaitableState<GameStateType>
    {
        private readonly GameContext _gameContext;
        private readonly ScenesOrder _scenesOrder;

        public LevelSelectionState(GameContext gameContext, ScenesOrder scenesOrder)
        {
            _gameContext = gameContext;
            _scenesOrder = scenesOrder;
        }

        public async Task RunState()
        {
            _gameContext.LevelNumber++;
            if (_scenesOrder.Scenes.Count <= _gameContext.LevelNumber)
            {
                _gameContext.NewLevelsAvailable = false;
                return;
            }

            //await UnloadScene(SceneManager.GetActiveScene().name);
            var sceneToLoad = _scenesOrder.Scenes[_gameContext.LevelNumber].GetSceneName();
            await LoadScene(sceneToLoad);
        }

        public GameStateType Type => GameStateType.LevelSelection;
        public bool IsExitState => false;

        private Task LoadScene(string sceneName) => DoAsyncOperationWithScene(sceneName, SceneManager.LoadSceneAsync);
        private Task UnloadScene(string sceneName) => DoAsyncOperationWithScene(sceneName, SceneManager.UnloadSceneAsync);

        private Task DoAsyncOperationWithScene(string sceneName, Func<string, AsyncOperation> sceneOperation)
        {
            var tcs = new TaskCompletionSource<Unit>();
            Observable.FromMicroCoroutine(() => DoAsyncOperationWithSceneCoroutine(sceneName, sceneOperation))
                .DoOnCompleted(() => tcs.SetResult(Unit.Default)).Subscribe();
            return tcs.Task;
        }

        IEnumerator DoAsyncOperationWithSceneCoroutine(string sceneName, Func<string, AsyncOperation> sceneOperation)
        {
            var asyncLoad = sceneOperation(sceneName);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}