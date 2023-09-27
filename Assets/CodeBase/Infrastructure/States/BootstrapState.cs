using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.PersistantProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string InitialSceneName = "Initial";

        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _container;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices container)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _container = container;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(InitialSceneName, onLoaded: EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel() =>
            _stateMachine.Enter<LoadProgressState>();

        private void RegisterServices()
        {
            IStaticDataService staticData = RegisterStaticData();
            IInputService input = _container.RegisterSingle(InputService());
            IRandomService random = _container.RegisterSingle<IRandomService>(new UnityRandomService());
            IAssets assets = _container.RegisterSingle<IAssets>(new Assets());
            
            IPersistantProgressService progress = _container.RegisterSingle<IPersistantProgressService>(new PersistantProgressService());
            IGameFactory factory = _container.RegisterSingle<IGameFactory>(new GameFactory(assets, staticData, random));
            _container.RegisterSingle<ISaveLoadService>(new SaveLoadService(progress, factory));
        }

        private IStaticDataService RegisterStaticData()
        {
            IStaticDataService staticData = new StaticDataService();
            staticData.LoadMonsters();
            return _container.RegisterSingle(staticData);
        }

        private static IInputService InputService() =>
            Application.isEditor ? new StandaloneInputService() : new MobileInputService();
    }
}