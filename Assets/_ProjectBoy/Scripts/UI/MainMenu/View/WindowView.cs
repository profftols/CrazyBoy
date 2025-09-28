using UnityEngine;

namespace _ProjectBoy.Scripts.UI.MainMenu.View
{
    public abstract class WindowView<T> : MonoBehaviour, IVisibility where T : IPresenterMenu
    {
        public abstract ScreenMainType ScreenType { get; }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public abstract void SetPresenterHandler(T handler);
    }
}