using UnityEngine;

namespace _ProjectBoy.Scripts.UI.Gameplay.Runner.View
{
    public abstract class GameScreenView<T> : MonoBehaviour, IVisibility where T : IPresenterGame
    {
        public abstract ScreenEndGameType ScreenType { get; }

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