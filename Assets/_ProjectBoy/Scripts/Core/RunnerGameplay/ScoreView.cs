using _ProjectBoy.Scripts.UI;
using _ProjectBoy.Scripts.UI.Gameplay;
using _ProjectBoy.Scripts.UI.Gameplay.Runner.View;
using TMPro;
using UnityEngine;

namespace _ProjectBoy.Scripts.Core.RunnerGameplay
{
    public class ScoreView : GameScreenView<IPresenterScoreScreen>, IVisibilityGame
    {
        private IPresenterScoreScreen _presenter;
        private float _scoreValue;
        [SerializeField] private TMP_Text _textScore;

        public override ScreenEndGameType ScreenType =>
            ScreenEndGameType.Gameplay;

        public void SetScore(float score)
        {
            _textScore.text = $" {_scoreValue += score:F2}";
        }

        public override void SetPresenterHandler(IPresenterScoreScreen handler)
        {
            _presenter = handler;
        }
    }
}