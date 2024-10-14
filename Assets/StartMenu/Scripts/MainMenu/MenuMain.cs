using System;
using UnityEngine;

namespace StartMenu.Scripts.MainMenu
{
    public class MenuMain : MenuActivity, IButtonActivity
    {
        [SerializeField] private UnityEngine.UI.Button _startButton;
        [SerializeField] private UnityEngine.UI.Button _settingButton;
        [SerializeField] private UnityEngine.UI.Button _leaderboardButton;
        [SerializeField] private UnityEngine.UI.Button _supportButton;
    
        public EventType Type => EventType.MainMenu;
    
        private void OnEnable()
        {
            _startButton.onClick.AddListener(OnStartGame);
            _settingButton.onClick.AddListener(OnSettingMenu);
            _leaderboardButton.onClick.AddListener(OnLeaderboard);
            _supportButton.onClick.AddListener(OnSupport);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(OnStartGame);
            _settingButton.onClick.RemoveListener(OnSettingMenu);
            _leaderboardButton.onClick.RemoveListener(OnLeaderboard);
            _supportButton.onClick.RemoveListener(OnSupport);
        }
    
        public event Action<EventType> OnClick;
        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    
        private void OnStartGame()
        {
            OnClick?.Invoke(EventType.StartGameMenu);
        }

        private void OnSettingMenu()
        {
            OnClick?.Invoke(EventType.SettingMenu);
        }
    
        private void OnLeaderboard()
        {
            OnClick?.Invoke(EventType.LeaderboardMenu);
        }
    
        private void OnSupport()
        {
            OnClick?.Invoke(EventType.SupportMenu);
        }
    }
}