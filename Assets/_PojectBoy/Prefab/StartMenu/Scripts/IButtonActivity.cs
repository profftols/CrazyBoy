using System;
using StartMenu.Scripts.MainMenu;

namespace StartMenu.Scripts
{
    public interface IButtonActivity
    {
        public EventType Type { get; }
        public event Action<EventType> OnClick;
        public void Hide();
        public void Show();
    }
}