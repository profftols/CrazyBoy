using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _leaderboardButton;
    [SerializeField] private Button _supportButton;
    [SerializeField] private MenuActivity _activityMenu;

    private void Start()
    {
        _settingButton.onClick.AddListener(OnSettingMenu);
    }
    
    private void OnButton()
    {
        
    }
    
    private void OnStartButtonClick()
    {
        
    }

    private void OnSettingMenu()
    {
        _activityMenu.ShowAudio();
    }
}