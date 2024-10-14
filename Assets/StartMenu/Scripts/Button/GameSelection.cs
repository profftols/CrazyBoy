using System;
using StartMenu.Scripts.MainMenu;
using UnityEngine;
using EventType = StartMenu.Scripts.MainMenu.EventType;

namespace StartMenu.Scripts.Button
{
    public class GameSelection : MenuActivity, IButtonActivity
    {
        [SerializeField] private UnityEngine.UI.Button _mainButton;
        [SerializeField] private Animation _animation;

        public EventType Type => EventType.StartGameMenu; 
        public  event Action<EventType> OnClick;
    
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

        private void OnEnable()
        {
            _mainButton.onClick.AddListener(OnMainMenu);
        }

        private void OnDisable()
        {
            _mainButton.onClick.AddListener(OnMainMenu);
        }

        private void OnMainMenu()
        {
            OnClick?.Invoke(EventType.MainMenu);
        }
    }
}
