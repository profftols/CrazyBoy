using System.Globalization;
using TMPro;
using UnityEngine;

namespace _ProjectBoy.Scripts.UI.MainMenu.View
{
    public class CoinView : WindowView<IPresenterCoin>, IVisibilityMainMenu
    {
        [SerializeField] private TextMeshProUGUI _coin;

        private IPresenterCoin _handlerCoin;
        public override ScreenMainType ScreenType => ScreenMainType.CoinView;

        public override void SetPresenterHandler(IPresenterCoin handler)
        {
            _handlerCoin = handler;
        }

        public void SetCoinView(float value)
        {
            _coin.text = value.ToString(CultureInfo.InvariantCulture);
        }
    }
}