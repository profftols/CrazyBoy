using UnityEngine;
using UnityEngine.UI;

namespace _ProjectBoy.Scripts.UI.MainMenu.View
{
    public class LeaderboardMenuView : WindowView<IPresenterLeaderboard>, IVisibilityMainMenu
    {
        [SerializeField] private Button _closeWindowButton;

        private IPresenterLeaderboard _presenterLeaderboard;

        public override ScreenMainType ScreenType { get; } = ScreenMainType.LeaderboardMenu;

        private void OnEnable()
        {
            _closeWindowButton.onClick.AddListener(() =>
            {
                _presenterLeaderboard.OnOpenWindows(ScreenMainType.MainMenu);
                Hide();
            });
        }

        private void OnDisable()
        {
            _closeWindowButton.onClick.RemoveListener(() =>
            {
                _presenterLeaderboard.OnOpenWindows(ScreenMainType.MainMenu);
                Hide();
            });
        }

        public override void SetPresenterHandler(IPresenterLeaderboard handler)
        {
            _presenterLeaderboard = handler;
        }
    }
}