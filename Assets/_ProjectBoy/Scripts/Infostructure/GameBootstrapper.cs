using _ProjectBoy.Scripts.Infostructure.States;
using UnityEngine;

namespace _ProjectBoy.Scripts.Infostructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;
        public LoadingCurtain Curtain;

        private void Awake()
        {
            var sceneLoader = new SceneLoader(this);
            _game = new Game(sceneLoader, Curtain);
            _game.StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }
    }
}