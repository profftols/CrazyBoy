using System;
using UnityEngine;
using UnityEngine.UI;

namespace StartMenu.Scripts.MainMenu
{
    public class MenuSettings : MenuActivity, IButtonActivity
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _buttonLanguage;

        public EventType Type => EventType.SettingMenu;
        public event Action<EventType> OnClick;
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

        private void OnEnable()
        {
            _backButton.onClick.AddListener(OnMainMenu);
            _buttonLanguage.onClick.AddListener(OnLanguageWindow);
        }

        private void OnDisable()
        {
            _backButton.onClick.RemoveListener(OnMainMenu);
            _buttonLanguage.onClick.RemoveListener(OnLanguageWindow);
        }

        private void OnLanguageWindow()
        {
            OnClick?.Invoke(EventType.LanguageMenu);
        }

        private void OnMainMenu()
        {
            OnClick?.Invoke(EventType.MainMenu);
        }
    }
}