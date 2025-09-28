using System.Globalization;
using _ProjectBoy.Scripts.UI.Gameplay.Runner.View;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _ProjectBoy.Scripts.UI.Gameplay.Fighter.View
{
    public class PlayerHealthView : GameScreenView<IPresenterHealthPlayer>, IVisibilityGame
    {
        private float _maxHP;
        [SerializeField] private TMP_Text _namePlayer;

        private IPresenterHealthPlayer _presenter;
        [SerializeField] private Slider _sliderHP;
        [SerializeField] private TMP_Text _textHP;

        public override ScreenEndGameType ScreenType =>
            ScreenEndGameType.Gameplay;

        public override void SetPresenterHandler(IPresenterHealthPlayer handler)
        {
            _presenter = handler;
        }

        public void UpdateHealth(float value)
        {
            _textHP.text = value.ToString(CultureInfo.InvariantCulture);
            _sliderHP.value = Mathf.InverseLerp(0, _maxHP, value);
        }

        public void SetPlayerInfo(float maxHP, string name)
        {
            _maxHP = maxHP;
            _namePlayer.text = name;
            _textHP.text = maxHP.ToString(CultureInfo.InvariantCulture);
            _sliderHP.value = 1f;
        }
    }
}