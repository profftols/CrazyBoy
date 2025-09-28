using System;
using System.Collections.Generic;
using _ProjectBoy.Scripts.Infostructure.AssetManagement;
using _ProjectBoy.Scripts.Infostructure.Factory;
using _ProjectBoy.Scripts.Infostructure.Services;

namespace _ProjectBoy.Scripts.Infostructure.States
{
    public class GameStateMachine
    {
        private readonly IAssets _assets;
        private readonly IGameFactory _factory;
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain curtain, AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LoadLevelState)] =
                    new LoadLevelState(this, sceneLoader, curtain, services, services.Single<IGameFactory>()),
                [typeof(MainMenuState)] = new MainMenuState(this),
                [typeof(GameRunnerState)] = new GameRunnerState(this, services.Single<IGameFactory>()),
                [typeof(GameFightState)] = new GameFightState(this)
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            var state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            var state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}