using System.Linq;
using UnityEngine;

namespace StartMenu.Scripts.MainMenu
{
    public enum EventType
    {
        StartGameMenu,
        MainMenu,
        SettingMenu,
        LeaderboardMenu,
        SupportMenu,
        LanguageMenu
    }

    public class MenuActivity : MonoBehaviour
    {
        [SerializeField] private MenuActivity[] _windows;

        private IButtonActivity[] ButtonActivities => _windows.Select(w => w.GetComponent<IButtonActivity>()).ToArray();
    
        private void OnEnable()
        {
            foreach (var window in ButtonActivities)
            {
                window.OnClick += OnButtonClick;
            }
        }
    
        private void OnDisable()
        {
            foreach (var window in ButtonActivities)
            {
                window.OnClick -= OnButtonClick;
            }
        }

        private void OnButtonClick(EventType type)
        {
            foreach (var menu in ButtonActivities)
            {
                menu.Hide();
            
                if (type == menu.Type)
                {
                    menu.Show();
                }
            }
        }
    }
}