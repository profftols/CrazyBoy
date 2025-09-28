using System.Collections.Generic;
using System.Linq;
using _ProjectBoy.Scripts.Audio;
using _ProjectBoy.Scripts.Core.CharacterSc;
using _ProjectBoy.Scripts.Core.RunnerGameplay;
using _ProjectBoy.Scripts.Core.RunnerGameplay.Plane;
using _ProjectBoy.Scripts.Infostructure.AssetManagement;
using _ProjectBoy.Scripts.Infostructure.Factory;
using _ProjectBoy.Scripts.UI.Gameplay.Runner;
using UnityEngine;

namespace _ProjectBoy.Scripts.Infostructure.States
{
    public class GameRunnerState : IPayloadedState<GameRunnerPresenter>
    {
        private const string SpawnColorTag = "SpawnColor";
        private const string PlayerTag = "Player";
        private const string EnemyTag = "Enemy";
        private const string MapTag = "Map";
        private readonly IGameFactory _gameFactory;

        private readonly GameStateMachine _stateMachine;
        private Dictionary<Runner, TriggerHandler> _handlerPlayers;

        private LandGrabbing _landGrabbing;
        private GameRunnerPresenter _presenter;

        public GameRunnerState(GameStateMachine stateMachine, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _gameFactory = gameFactory;
        }

        public void Enter(GameRunnerPresenter presenter)
        {
            _presenter = presenter;
            InitGame();
            SubscribeEventGameMode();
            _presenter.Initialize();
            MasterSoundSettings.Instance.soundMusic.ChangeMusicRunner();
        }

        public void Exit()
        {
            UnsubscribeEventGameMode();
        }

        private void OnChangeScene(string sceneName)
        {
            _stateMachine.Enter<LoadLevelState, string>(sceneName);
        }

        private void InitGame()
        {
            _landGrabbing = new LandGrabbing(GameObject.FindWithTag(MapTag).GetComponent<Map>());
            _handlerPlayers = new Dictionary<Runner, TriggerHandler>();

            var spawnColor = GameObject.FindWithTag(SpawnColorTag).GetComponent<SpawnColor>();
            var player = GameObject.FindWithTag(PlayerTag).GetComponent<Player>();
            var enemies = GameObject.FindGameObjectsWithTag(EnemyTag).Where(x =>
                x.CompareTag(EnemyTag)).Select(x =>
                x.GetComponent<Enemy>()).ToArray();

            foreach (var enemy in enemies) CreateRunner(enemy, spawnColor, AssetPath.EnemyMoverPath);

            CreateRunner(player, spawnColor, AssetPath.PlayerMoverPath);
        }

        private void CreateRunner(Character character, SpawnColor spawnColor, string path)
        {
            Runner runner;
            var transferTrigger =
                _gameFactory.CreateGameElement(path, character.transform).GetComponent<TriggerHandler>();
            var controller = character.GetComponent<CharacterController>();

            if (character is Enemy)
            {
                var mover = transferTrigger.GetComponent<EnemyMove>();
                runner = new Runner(character, spawnColor.GetColor(), character.transform, mover);
                mover.Init(controller,
                    character.Animator, runner);
            }
            else
            {
                var mover = transferTrigger.GetComponent<Mover>();
                runner = new Runner(character, spawnColor.GetColor(), character.transform, mover);
                mover.Init(controller, character.Animator);
            }

            _handlerPlayers.Add(runner, transferTrigger);
            transferTrigger.transform.position = character.transform.position;
        }

        private void SubscribeEventGameMode()
        {
            _presenter.OnChangeScene += OnChangeScene;
            _landGrabbing.OnScore += _presenter.OnScoreChange;
            _landGrabbing.OnSurvive += _presenter.OnShowScreenGameOver;

            foreach (var handler in _handlerPlayers)
            {
                handler.Value.OnTrigger += handler.Key.OnTrigger;
                handler.Key.OnLand += _landGrabbing.CheckLand;
                _landGrabbing.Start(handler.Key.Start(), handler.Key);
            }
        }

        private void UnsubscribeEventGameMode()
        {
            _landGrabbing.OnScore -= _presenter.OnScoreChange;
            _landGrabbing.OnSurvive -= _presenter.OnShowScreenGameOver;

            foreach (var handler in _handlerPlayers)
            {
                handler.Value.OnTrigger -= handler.Key.OnTrigger;
                handler.Key.OnLand -= _landGrabbing.CheckLand;
            }

            _presenter.OnChangeScene -= OnChangeScene;
        }
    }
}