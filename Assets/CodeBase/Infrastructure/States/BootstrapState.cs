using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.PersistantProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.StaticData;
using CodeBase.UI.Services.Factory;
using CodeBase.UI.Services.Windows;
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
            IInputService input = RegisterInputService();
            IRandomService random = RegisterRandomService();
            IAssets assets = RegisterAssets();
            IPersistantProgressService progress = RegisterPersistantProgressService();
            IUIFactory uiFactory = RegisterUIFactoryService(assets, staticData);
            IWindowService windowService = RegisterWindowService(uiFactory);
            IGameFactory factory = RegisterFactory(assets, staticData, random, progress, windowService);
            ISaveLoadService saveLoadService = RegisterSaveLoadService(progress, factory);
        }

        private IWindowService RegisterWindowService(IUIFactory uiFactory)
        {
            return _container.RegisterSingle<IWindowService>(new WindowService(uiFactory));
        }

        private IUIFactory RegisterUIFactoryService(IAssets assets, IStaticDataService staticData)
        {
            return _container.RegisterSingle<IUIFactory>(new UIFactory(assets, staticData));
        }

        private ISaveLoadService RegisterSaveLoadService(IPersistantProgressService progress, IGameFactory factory)
        {
            return _container.RegisterSingle<ISaveLoadService>(new SaveLoadService(progress, factory));
        }

        private IAssets RegisterAssets()
        {
            return _container.RegisterSingle<IAssets>(new Assets());
        }

        private IRandomService RegisterRandomService()
        {
            return _container.RegisterSingle<IRandomService>(new UnityRandomService());
        }

        private IInputService RegisterInputService()
        {
            return _container.RegisterSingle(InputService());
        }

        private IPersistantProgressService RegisterPersistantProgressService()
        {
            return _container.RegisterSingle<IPersistantProgressService>(new PersistantProgressService());
        }

        private IGameFactory RegisterFactory(IAssets assets, IStaticDataService staticData, IRandomService random,
            IPersistantProgressService progress, IWindowService windowService)
        {
            return _container.RegisterSingle<IGameFactory>(new GameFactory(assets, staticData, random, progress, windowService));
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