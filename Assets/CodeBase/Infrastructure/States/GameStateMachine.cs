using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.PersistantProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.Logic;

namespace CodeBase.Infrastructure.States
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LoadSceneState)] = new LoadSceneState(this, sceneLoader, loadingCurtain, services.Single<IGameFactory>(), services.Single<IPersistantProgressService>()),
                [typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IPersistantProgressService>(), services.Single<ISaveLoadService>()),
                [typeof(GameLoopState)] = new GameLoopState(this),
            };
        }

        public void Enter<TState>() where TState : class, IState =>
            ChangeState<TState>().Enter();

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload> =>
            ChangeState<TState>().Enter(payload);

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = _states[typeof(TState)] as TState;
            _activeState = state;

            return state;
        }
    }
}