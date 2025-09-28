using UnityEngine;
using UnityEngine.UI;

namespace _ProjectBoy.Scripts.UI.MainMenu.View
{
    public class MainMenuView : WindowView<IPresenterMainMenu>, IVisibilityMainMenu
    {
        private IPresenterMainMenu _presenterMainMenu;
        [field: SerializeField] public Button Start { get; private set; }
        [field: SerializeField] public Button Leaderboard { get; private set; }
        [field: SerializeField] public Button Settings { get; private set; }
        [field: SerializeField] public Button Skins { get; private set; }
        [field: SerializeField] public Button Support { get; private set; }

        public override ScreenMainType ScreenType { get; } = ScreenMainType.MainMenu;

        private void OnEnable()
        {
            Subscribe(Start, ScreenMainType.GameplayMenu);
            Subscribe(Leaderboard, ScreenMainType.LeaderboardMenu);
            Subscribe(Settings, ScreenMainType.SettingMenu);
            Subscribe(Skins, ScreenMainType.SkinsMenu);
            Subscribe(Support, ScreenMainType.SupportMenu);
        }

        private void OnDisable()
        {
            Unsubscribe(Start, ScreenMainType.GameplayMenu);
            Unsubscribe(Leaderboard, ScreenMainType.LeaderboardMenu);
            Unsubscribe(Settings, ScreenMainType.SettingMenu);
            Unsubscribe(Skins, ScreenMainType.SkinsMenu);
            Unsubscribe(Support, ScreenMainType.SupportMenu);
        }

        public override void SetPresenterHandler(IPresenterMainMenu handler)
        {
            _presenterMainMenu = handler;
        }

        private void Subscribe(Button button, ScreenMainType type)
        {
            button.onClick.AddListener(() =>
            {
                _presenterMainMenu.OnOpenWindows(type);
                Hide();
            });
        }

        private void Unsubscribe(Button button, ScreenMainType type)
        {
            button.onClick.RemoveListener(() =>
            {
                _presenterMainMenu.OnOpenWindows(type);
                Hide();
            });
        }
    }
}