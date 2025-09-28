using UnityEngine;
using UnityEngine.UI;
using YG;

namespace _ProjectBoy.Scripts.UI.MainMenu.View
{
    public class LanguageMenuView : WindowView<IPresenterLanguage>, IVisibilityMainMenu
    {
        [SerializeField] private Button _closeButton;

        [SerializeField] private Button _engButton;

        [SerializeField] private Image _engImage;

        private IPresenterLanguage _presenterLanguage;
        [SerializeField] private Button _rusButton;
        [SerializeField] private Image _rusImage;
        [SerializeField] private Button _turButton;
        [SerializeField] private Image _turImage;

        public override ScreenMainType ScreenType { get; } = ScreenMainType.LanguageMenu;

        private void OnEnable()
        {
            SetFlag(YG2.lang);

            _closeButton.onClick.AddListener(() =>
            {
                _presenterLanguage.OnOpenWindows(ScreenMainType.SettingMenu);
                Hide();
            });
            _engButton.onClick.AddListener(() => _presenterLanguage.SetLanguage(SetFlag(Constants.English)));
            _rusButton.onClick.AddListener(() => _presenterLanguage.SetLanguage(SetFlag(Constants.Russian)));
            _turButton.onClick.AddListener(() => _presenterLanguage.SetLanguage(SetFlag(Constants.Turkish)));
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(() =>
            {
                _presenterLanguage.OnOpenWindows(ScreenMainType.SettingMenu);
                Hide();
            });
            _engButton.onClick.RemoveListener(() => _presenterLanguage.SetLanguage(SetFlag(Constants.English)));
            _rusButton.onClick.RemoveListener(() => _presenterLanguage.SetLanguage(SetFlag(Constants.Russian)));
            _turButton.onClick.RemoveListener(() => _presenterLanguage.SetLanguage(SetFlag(Constants.Turkish)));
        }

        public override void SetPresenterHandler(IPresenterLanguage handler)
        {
            _presenterLanguage = handler;
        }

        private string SetFlag(string language)
        {
            switch (language)
            {
                case Constants.English:
                    _engImage.gameObject.SetActive(true);
                    _rusImage.gameObject.SetActive(false);
                    _turImage.gameObject.SetActive(false);
                    break;
                case Constants.Russian:
                    _engImage.gameObject.SetActive(false);
                    _rusImage.gameObject.SetActive(true);
                    _turImage.gameObject.SetActive(false);
                    break;
                case Constants.Turkish:
                    _engImage.gameObject.SetActive(false);
                    _rusImage.gameObject.SetActive(false);
                    _turImage.gameObject.SetActive(true);
                    break;
            }

            return language;
        }
    }
}