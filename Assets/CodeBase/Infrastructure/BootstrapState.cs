using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class BootstrapState : IState
    {
        private const string InitialSceneName = "Initial";
        private const string MainSceneName = "Main";
        
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            RegisterServices();
            _sceneLoader.Load(InitialSceneName, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
            
        }

        private void EnterLoadLevel() => 
            _stateMachine.Enter<LoadLevelState, string>(MainSceneName);

        private void RegisterServices() => 
            Game.InputService = RegisterInputService();

        private static IInputService RegisterInputService() => 
            Application.isEditor ? new StandaloneInputService() : new MobileInputService();
    }
}