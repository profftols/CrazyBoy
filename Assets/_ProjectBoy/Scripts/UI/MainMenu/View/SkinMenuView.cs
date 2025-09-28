using UnityEngine;
using UnityEngine.UI;

namespace _ProjectBoy.Scripts.UI.MainMenu.View
{
    public class SkinMenuView : WindowView<IPresenterSkin>, IVisibilityMainMenu
    {
        private IPresenterSkin _presenterSkin;
        [field: SerializeField] public Button CloseWindow { get; private set; }
        [field: SerializeField] public Button RArrow { get; private set; }
        [field: SerializeField] public Button LArrow { get; private set; }
        [field: SerializeField] public Button Buy { get; private set; }
        [field: SerializeField] public Button Select { get; private set; }
        [field: SerializeField] public SkinView SkinView { get; private set; }

        public override ScreenMainType ScreenType { get; } = ScreenMainType.SkinsMenu;

        private void OnEnable()
        {
            CloseWindow.onClick.AddListener(() =>
            {
                _presenterSkin.OnOpenWindows(ScreenMainType.MainMenu);
                Hide();
            });
            RArrow.onClick.AddListener(_presenterSkin.OnNextSkin);
            LArrow.onClick.AddListener(_presenterSkin.OnBackSkin);
            Buy.onClick.AddListener(() => _presenterSkin.OnBuy());
            Select.onClick.AddListener(() => _presenterSkin.OnSelect());
        }

        private void OnDisable()
        {
            CloseWindow.onClick.RemoveListener(() =>
            {
                _presenterSkin.OnOpenWindows(ScreenMainType.MainMenu);
                Hide();
            });
            RArrow.onClick.RemoveListener(_presenterSkin.OnNextSkin);
            LArrow.onClick.RemoveListener(_presenterSkin.OnBackSkin);
            Buy.onClick.RemoveListener(() => _presenterSkin.OnBuy());
            Select.onClick.RemoveListener(() => _presenterSkin.OnSelect());
        }

        public override void SetPresenterHandler(IPresenterSkin handler)
        {
            _presenterSkin = handler;
        }
    }
}