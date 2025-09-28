namespace _ProjectBoy.Scripts.UI.MainMenu
{
    public interface IPresenterMenu
    {
        void OnOpenWindows(ScreenMainType mainType);
    }

    public interface IPresenterLanguage : IPresenterMenu
    {
        void SetLanguage(string language);
    }

    public interface IPresenterSkin : IPresenterMenu
    {
        int IndexSkin { get; }
        void OnBuy();
        void OnSelect();
        void OnNextSkin();
        void OnBackSkin();
    }

    public interface IPresenterGamemode : IPresenterMenu
    {
        void ChangeScene(string sceneName);
    }

    public interface IPresenterSupport : IPresenterMenu
    {
        void GetBonus();
    }

    public interface IPresenterLeaderboard : IPresenterMenu
    {
    }

    public interface IPresenterMainMenu : IPresenterMenu
    {
    }

    public interface IPresenterSetting : IPresenterMenu
    {
    }

    public interface IPresenterCoin : IPresenterMenu
    {
    }
}