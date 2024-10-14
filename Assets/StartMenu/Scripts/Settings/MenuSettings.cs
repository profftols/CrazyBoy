using System;
using StartMenu.Scripts.MainMenu;
using UnityEngine;
using EventType = StartMenu.Scripts.MainMenu.EventType;

namespace StartMenu.Scripts.Settings
{
    public class MenuSettings : MenuActivity, IButtonActivity
    {
        [SerializeField] private UnityEngine.UI.Button _backButton;
        [SerializeField] private UnityEngine.UI.Button _buttonLanguage;

        public EventType Type => EventType.SettingMenu; 
    
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

        public event Action<EventType> OnClick;
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

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