using System;
using StartMenu.Scripts.MainMenu;
using UnityEngine;
using UnityEngine.UI;
using EventType = StartMenu.Scripts.MainMenu.EventType;

namespace StartMenu.Scripts.Settings.Language
{
    public class LanguageSetting : MenuActivity, IButtonActivity
    {
        [SerializeField] private global::Language _language;
        [SerializeField] private UnityEngine.UI.Button _buttonClose;
        [SerializeField] private Image _eng;
        [SerializeField] private Image _rus;
        [SerializeField] private Image _tur;

        public EventType Type => EventType.LanguageMenu; 
    
        private void OnEnable()
        {
            _language.ChangingLanguage += ChangeLang;
            _buttonClose.onClick.AddListener(MenuSettings);
        }

        private void OnDisable()
        {
            _language.ChangingLanguage -= ChangeLang;
            _buttonClose.onClick.RemoveListener(MenuSettings);
        }

        public event Action<EventType> OnClick;
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    
        private void ChangeLang()
        {
            if(global::Language.Instance.CurrentLanguage == _language.English)
            {
                _eng.gameObject.SetActive(true);
                _rus.gameObject.SetActive(false);
                _tur.gameObject.SetActive(false);
            }
            else if(global::Language.Instance.CurrentLanguage == _language.Russian)
            {
                _eng.gameObject.SetActive(false);
                _rus.gameObject.SetActive(true);
                _tur.gameObject.SetActive(false);
            }
            else if(global::Language.Instance.CurrentLanguage == _language.Turkish)
            {
                _eng.gameObject.SetActive(false);
                _rus.gameObject.SetActive(false);
                _tur.gameObject.SetActive(true);
            }
            else
            {
                _eng.gameObject.SetActive(true);
                _rus.gameObject.SetActive(false);
                _tur.gameObject.SetActive(false);
            }
        }
    
        private void MenuSettings()
        {
            OnClick?.Invoke(EventType.SettingMenu);
        }
    }
}
