using _ProjectBoy.Scripts.Audio;
using _ProjectBoy.Scripts.UI.MainMenu;

namespace _ProjectBoy.Scripts.Infostructure.States
{
    public class MainMenuState : IPayloadedState<MainMenuPresenter>
    {
        private readonly GameStateMachine _stateMachine;
        private MainMenuPresenter _mainMenuPresenter;

        public MainMenuState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter(MainMenuPresenter presenter)
        {
            _mainMenuPresenter = presenter;
            _mainMenuPresenter.Initialize();
            _mainMenuPresenter.OnChangeScene += OnChangeScene;
            MasterSoundSettings.Instance.soundMusic.ChangeMusicMenu();
        }

        public void Exit()
        {
            _mainMenuPresenter.OnChangeScene -= OnChangeScene;
        }

        private void OnChangeScene(string sceneName)
        {
            _stateMachine.Enter<LoadLevelState, string>(sceneName);
        }
    }
}