using _ProjectBoy.Scripts.UI.Gameplay;
using _ProjectBoy.Scripts.UI.MainMenu;

namespace _ProjectBoy.Scripts.UI
{
    public interface IVisibility
    {
        void Show();
        void Hide();
    }

    public interface IVisibilityMainMenu : IVisibility
    {
        ScreenMainType ScreenType { get; }
    }

    public interface IVisibilityGame : IVisibility
    {
        ScreenEndGameType ScreenType { get; }
    }
}