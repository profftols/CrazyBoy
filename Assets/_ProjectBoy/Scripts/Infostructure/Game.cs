using _ProjectBoy.Scripts.Infostructure.Services;
using _ProjectBoy.Scripts.Infostructure.States;

namespace _ProjectBoy.Scripts.Infostructure
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game(SceneLoader sceneLoader, LoadingCurtain curtain)
        {
            StateMachine = new GameStateMachine(sceneLoader, curtain, AllServices.Container);
        }
    }
}