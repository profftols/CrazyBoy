using UnityEngine;

namespace _ProjectBoy.Scripts.UI.MainMenu.View
{
    public class WindowsScreenView : MonoBehaviour, IView
    {
        public IVisibilityMainMenu[] WindowsView;
        [field: SerializeField] public GameSelectionMenuView GameSelectionMenuMenu { get; private set; }
        [field: SerializeField] public LanguageMenuView LanguageMenu { get; private set; }
        [field: SerializeField] public LeaderboardMenuView LeaderboardMenu { get; private set; }
        [field: SerializeField] public MainMenuView MainMenu { get; private set; }
        [field: SerializeField] public SettingMenuView SettingMenu { get; private set; }
        [field: SerializeField] public SkinMenuView SkinMenu { get; private set; }
        [field: SerializeField] public SupportMenuView SupportMenu { get; private set; }
        [field: SerializeField] public CoinView CoinView { get; private set; }

        private void Awake()
        {
            WindowsView = new IVisibilityMainMenu[]
            {
                CoinView,
                GameSelectionMenuMenu,
                LanguageMenu,
                LeaderboardMenu,
                MainMenu,
                SettingMenu,
                SkinMenu,
                SupportMenu
            };
        }
    }
}