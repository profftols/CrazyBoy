using UnityEngine;
using UnityEngine.UI;

namespace _ProjectBoy.Scripts.UI.MainMenu.View
{
    public class SettingMenuView : WindowView<IPresenterSetting>, IVisibilityMainMenu
    {
        [SerializeField] private Button _closeWindowButton;
        [SerializeField] private Button _languageOpenButton;

        private IPresenterSetting _presenterSetting;

        public override ScreenMainType ScreenType { get; } = ScreenMainType.SettingMenu;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _closeWindowButton.onClick.AddListener(() =>
            {
                _presenterSetting.OnOpenWindows(ScreenMainType.MainMenu);
                Hide();
            });
            _languageOpenButton.onClick.AddListener(() => _presenterSetting.OnOpenWindows(ScreenMainType.LanguageMenu));
        }

        private void OnDisable()
        {
            _closeWindowButton.onClick.RemoveListener(() =>
            {
                _presenterSetting.OnOpenWindows(ScreenMainType.MainMenu);
                Hide();
            });
            _languageOpenButton.onClick.RemoveListener(() =>
                _presenterSetting.OnOpenWindows(ScreenMainType.LanguageMenu));
        }

        public override void SetPresenterHandler(IPresenterSetting handler)
        {
            _presenterSetting = handler;
        }
    }
}