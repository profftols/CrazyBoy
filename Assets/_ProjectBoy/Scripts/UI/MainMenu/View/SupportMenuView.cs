using UnityEngine;
using UnityEngine.UI;

namespace _ProjectBoy.Scripts.UI.MainMenu.View
{
    public class SupportMenuView : WindowView<IPresenterSupport>, IVisibilityMainMenu
    {
        [SerializeField] private Button _closeWindow;

        private IPresenterSupport _presenter;

        public override ScreenMainType ScreenType { get; } = ScreenMainType.SupportMenu;

        private void OnEnable()
        {
            _presenter.GetBonus();
            _closeWindow.onClick.AddListener(() =>
            {
                _presenter.OnOpenWindows(ScreenMainType.MainMenu);
                Hide();
            });
        }

        private void OnDisable()
        {
            _closeWindow.onClick.RemoveListener(() =>
            {
                _presenter.OnOpenWindows(ScreenMainType.MainMenu);
                Hide();
            });
        }

        public override void SetPresenterHandler(IPresenterSupport handler)
        {
            _presenter = handler;
        }
    }
}