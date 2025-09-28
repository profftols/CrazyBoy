using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _ProjectBoy.Scripts.UI.MainMenu.View
{
    public class GameSelectionMenuView : WindowView<IPresenterGamemode>, IVisibilityMainMenu
    {
        private const string Runner = "Running";
        private const string Fight = "Fighting";

        private const float Duration = 0.5f;
        private const float HideFightX = 16.71296f;
        private const float HideRunnerX = -16.71296f;
        private const float ShowFightX = 1.169908f;
        private const float ShowRunnerX = -1.169908f;

        [SerializeField] private Button _closeWindowButton;

        [SerializeField] private RectTransform _fightRect;
        [SerializeField] private Button _fightStartButton;
        private IPresenterGamemode _handlerGame;
        [SerializeField] private RectTransform _runnerRect;
        [SerializeField] private Button _runnerStartButton;
        [SerializeField] private RectTransform _vsTextRect;

        public override ScreenMainType ScreenType => ScreenMainType.GameplayMenu;

        public new void Show()
        {
            base.Show();
            DeactivateButton();
            _fightRect.transform.DOMoveX(ShowFightX, Duration);
            _runnerRect.transform.DOMoveX(ShowRunnerX, Duration);
            _vsTextRect.DOScale(0.25f, Duration);
            DOTween.Sequence()
                .AppendInterval(Duration)
                .AppendCallback(ActiveButton);
        }

        public new void Hide()
        {
            _fightRect.transform.DOMoveX(HideFightX, Duration);
            _runnerRect.transform.DOMoveX(HideRunnerX, Duration);
            _vsTextRect.DOScale(0, Duration);
            DOTween.Sequence()
                .AppendInterval(Duration)
                .AppendCallback(() => base.Hide());
        }

        private void OnEnable()
        {
            _closeWindowButton.onClick.AddListener(() =>
            {
                _handlerGame.OnOpenWindows(ScreenMainType.MainMenu);
                Hide();
            });
            _fightStartButton.onClick.AddListener(() => _handlerGame.ChangeScene(Fight));
            _runnerStartButton.onClick.AddListener(() => _handlerGame.ChangeScene(Runner));
        }

        private void OnDisable()
        {
            _closeWindowButton.onClick.RemoveListener(() =>
            {
                _handlerGame.OnOpenWindows(ScreenMainType.MainMenu);
                Hide();
            });
            _fightStartButton.onClick.RemoveListener(() => _handlerGame.ChangeScene(Fight));
            _runnerStartButton.onClick.RemoveListener(() => _handlerGame.ChangeScene(Runner));
        }

        public override void SetPresenterHandler(IPresenterGamemode handler)
        {
            _handlerGame = handler;
        }

        private void ActiveButton()
        {
            _fightStartButton.gameObject.SetActive(true);
            _runnerStartButton.gameObject.SetActive(true);
        }

        private void DeactivateButton()
        {
            _fightStartButton.gameObject.SetActive(false);
            _runnerStartButton.gameObject.SetActive(false);
        }
    }
}