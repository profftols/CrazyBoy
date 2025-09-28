using _ProjectBoy.Scripts.Audio;
using _ProjectBoy.Scripts.Core.FighterGameplay;
using _ProjectBoy.Scripts.UI.Gameplay.Fighter;

namespace _ProjectBoy.Scripts.Infostructure.States
{
    public class GameFightState : IPayloadedState<GameFightPresenter>
    {
        private readonly GameStateMachine _stateMachine;
        private IFighterMode _fighterMode;
        private GameFightPresenter _presenter;

        public GameFightState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter(GameFightPresenter presenter)
        {
            _presenter = presenter;
            _presenter.Initialize();
            _presenter.OnChangeScene += OnChange;
            MasterSoundSettings.Instance.soundMusic.ChangeMusicFighter();
        }

        public void Exit()
        {
            _presenter.OnChangeScene -= OnChange;
        }

        private void OnChange(string sceneName)
        {
            _stateMachine.Enter<LoadLevelState, string>(sceneName);
        }
    }
}