using System;
using _ProjectBoy.Scripts.Core.RunnerGameplay;
using _ProjectBoy.Scripts.Service;
using _ProjectBoy.Scripts.UI.Gameplay.Runner.View;

namespace _ProjectBoy.Scripts.UI.Gameplay.Runner
{
    public class GameRunnerPresenter : Presenter<WindowsRunnerView>,
        IPresenterGameOverScreen,
        IPresenterScoreScreen
    {
        private readonly IPlayerProgress _progress;
        private readonly ScoreRunnerModel _score;
        public Action<string> OnChangeScene;

        public GameRunnerPresenter(WindowsRunnerView view, ScoreRunnerModel score, IPlayerProgress progress) :
            base(view)
        {
            _score = score;
            _progress = progress;
        }

        public void ChangeScene(string name)
        {
            OnChangeScene?.Invoke(name);
        }

        public void OnShowScreenGameOver(bool isWin)
        {
            if (isWin)
            {
                View.WinScreenView.Show();
                _progress.AddScore(_score.Score);
            }
            else
            {
                View.DefeatScreenView.Show();
            }
        }

        public void OnScoreChange(float value)
        {
            _score.AddScore(value);
            View.ScoreView.SetScore(_score.Score);
        }

        public void Initialize()
        {
            View.DefeatScreenView.SetPresenterHandler(this);
            View.WinScreenView.SetPresenterHandler(this);
            View.ScoreView.SetPresenterHandler(this);
            View.DefeatScreenView.gameObject.SetActive(false);
            View.WinScreenView.gameObject.SetActive(false);
        }
    }
}