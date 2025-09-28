using System.Collections.Generic;
using _ProjectBoy.Scripts.Core.CharacterSc;
using _ProjectBoy.Scripts.Core.MainMenu;
using _ProjectBoy.Scripts.Infostructure.AssetManagement;
using _ProjectBoy.Scripts.Infostructure.Factory;
using _ProjectBoy.Scripts.Infostructure.Services;
using _ProjectBoy.Scripts.Service;
using UnityEngine;
using YG;

namespace _ProjectBoy.Scripts.Infostructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";
        private const string Main = "Main";
        private readonly SceneLoader _sceneLoaders;
        private readonly AllServices _services;

        private readonly GameStateMachine _stateMachine;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoaders, AllServices services)
        {
            _stateMachine = stateMachine;
            _sceneLoaders = sceneLoaders;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoaders.Load(Initial, EnterLoadLevel);
        }

        public void Exit()
        {
        }

        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState, string>(Main);
        }

        private void RegisterServices()
        {
            _services.RegisterSingle(InputService());
            _services.RegisterSingle<IAssets>(new AssetProvider());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssets>()));
            _services.RegisterSingle<ISkinDataContainer>(
                new SkinDataContainer(Resources.LoadAll<Skin>(AssetPath.SkinsPath)));
            AuthorizationPlayer(YG2.player.auth);
            RegisterProgressPlayer();
        }

        private static IInputService InputService()
        {
            return Application.isMobilePlatform ? new MobileInputService() : new StandartaloneInputService();
        }

        private void RegisterProgressPlayer()
        {
            if (YG2.saves.currentSkin == null)
            {
                var skins = new List<Skin>
                {
                    Resources.Load<Skin>(
                        AssetPath.DefaultSkinPath)
                };
                _services.RegisterSingle<IPlayerProgress>(new GamingProgress(0, skins, skins[0]));
                return;
            }

            _services.RegisterSingle<IPlayerProgress>(new GamingProgress(YG2.saves.score, YG2.saves.skins,
                YG2.saves.currentSkin));
        }

        private void AuthorizationPlayer(bool isLogged)
        {
            Debug.Log(isLogged
                ? "Пользователь <color=green>Авторизован!</color>"
                : "Пользователь <color=red>Не авторизован!</color>");
        }
    }
}