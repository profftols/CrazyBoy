using System;
using UnityEngine;

namespace StartMenu.Scripts.MainMenu.Yandex.Scripts
{
    public class Leaderboard : MenuActivity, IButtonActivity
    {
        [SerializeField] private UnityEngine.UI.Button _mainButton;

        public EventType Type => EventType.LeaderboardMenu; 
        private const string LeaderboardName = "Leaderboard";
    
        public event Action<EventType> OnClick;
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
        private void OnEnable()
        {
            _mainButton.onClick.AddListener(OnMainMenu);
        }

        private void OnDisable()
        {
            _mainButton.onClick.RemoveListener(OnMainMenu);
        }

        private void OnMainMenu()
        {
            OnClick?.Invoke(EventType.MainMenu);
        }
    }
}