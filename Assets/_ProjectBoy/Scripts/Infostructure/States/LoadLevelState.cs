using _ProjectBoy.Scripts.Core.CharacterSc;
using _ProjectBoy.Scripts.Core.MainMenu;
using _ProjectBoy.Scripts.Core.RunnerGameplay;
using _ProjectBoy.Scripts.Environment;
using _ProjectBoy.Scripts.Infostructure.AssetManagement;
using _ProjectBoy.Scripts.Infostructure.Factory;
using _ProjectBoy.Scripts.Infostructure.Services;
using _ProjectBoy.Scripts.Service;
using _ProjectBoy.Scripts.UI.Gameplay.Fighter;
using _ProjectBoy.Scripts.UI.Gameplay.Fighter.View;
using _ProjectBoy.Scripts.UI.Gameplay.Runner;
using _ProjectBoy.Scripts.UI.Gameplay.Runner.View;
using _ProjectBoy.Scripts.UI.MainMenu;
using _ProjectBoy.Scripts.UI.MainMenu.View;
using UnityEngine;
using YG;

namespace _ProjectBoy.Scripts.Infostructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string WindowsScreenTag = "InitialWindow";
        private const string WindowsRunnerViewTag = "HudRunner";
        private const string WindowsFightViewTag = "HudFighter";
        private const string InitialPointTag = "SpawnPointCharacter";
        private const string Main = "Main";
        private const string Fighting = "Fighting";
        private const string Running = "Running";
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _service;

        private readonly GameStateMachine _stateMachine;

        public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
            AllServices services, IGameFactory factory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = factory;
            _service = services;
            _curtain = curtain;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();

            switch (sceneName)
            {
                case Main:
                    _sceneLoader.Load(sceneName, OnLoadedMain);
                    break;

                case Fighting:
                    _sceneLoader.Load(sceneName, OnLoadedFighter);
                    break;

                case Running:
                    _sceneLoader.Load(sceneName, OnLoadedRunner);
                    break;
            }
        }

        public void Exit()
        {
            _curtain.Hide();
        }

        private void OnLoadedRunner()
        {
            CreateGameRunner();
            var view = GameObject.FindWithTag(WindowsRunnerViewTag).GetComponent<WindowsRunnerView>();
            var presenter = new GameRunnerPresenter(view, new ScoreRunnerModel(),
                _service.Single<IPlayerProgress>());
            _stateMachine.Enter<GameRunnerState, GameRunnerPresenter>(presenter);
        }

        private void OnLoadedFighter()
        {
            CreateGameFight();
            var view = GameObject.FindWithTag(WindowsFightViewTag).GetComponent<WindowsFightView>();
            var presenter = new GameFightPresenter(view, _service.Single<IPlayerProgress>());
            _stateMachine.Enter<GameFightState, GameFightPresenter>(presenter);
        }

        private void OnLoadedMain()
        {
            var view = GameObject.FindWithTag(WindowsScreenTag).GetComponent<WindowsScreenView>();
            var presenter = new MainMenuPresenter(view, _service.Single<ISkinDataContainer>(),
                _service.Single<IPlayerProgress>(),
                _service.Single<IPlayerProgress>().Score);
            _stateMachine.Enter<MainMenuState, MainMenuPresenter>(presenter);
        }

        private void CreateGameFight()
        {
            _gameFactory.CreateHub(AssetPath.HudFighterPath);
            CreateCharacters();
        }

        private void CreateGameRunner()
        {
            _gameFactory.CreateHub(AssetPath.HudRunnerPath);
            CreateCharacters();
        }

        private void CreateCharacters()
        {
            var skinData = _service.Single<ISkinDataContainer>();
            var spawnPoint = GameObject.FindWithTag(InitialPointTag).GetComponent<SpawnPoint>();
            var player = _gameFactory.CreateCharacter(AssetPath.PlayerPath, spawnPoint).GetComponent<Player>();
            player.Init(AllServices.Container.Single<IPlayerProgress>().CurrentSkin,
                YG2.player.name);
            var count = spawnPoint.GetCountSpawnPoints();
            CameraFollow(player);

            if (count == 1)
            {
                var skin = skinData.GetSkin(Random.Range(0, skinData.Count - 1));
                var enemy = _gameFactory.CreateCharacter(AssetPath.EnemyPath, spawnPoint).GetComponent<Enemy>();
                enemy.Init(skin, $"{skin.name}");
                return;
            }

            for (var i = 0; i < count; i++)
            {
                var enemy = _gameFactory.CreateCharacter(AssetPath.EnemyPath, spawnPoint).GetComponent<Enemy>();
                enemy.Init(skinData.GetSkin(Random.Range(0, skinData.Count - 1)), $"Bot {i + 1}");
            }
        }

        private void CameraFollow(Player player)
        {
            if (player == null)
            {
                Debug.LogError("Player is null");
                return;
            }

            var mainCamera = Camera.main;

            if (mainCamera == null)
            {
                Debug.LogError("Main Camera is missing in the scene.");
                return;
            }

            var followComponent = mainCamera.GetComponent<CameraFollow>();

            if (followComponent == null)
            {
                Debug.LogError("CameraFollow component is missing on Main Camera.");
                return;
            }

            followComponent.SetFollowing(player.transform);
        }
    }
}