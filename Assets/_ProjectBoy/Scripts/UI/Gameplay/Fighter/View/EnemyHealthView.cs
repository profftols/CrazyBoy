using System.Globalization;
using _ProjectBoy.Scripts.UI.Gameplay.Runner.View;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _ProjectBoy.Scripts.UI.Gameplay.Fighter.View
{
    public class EnemyHealthView : GameScreenView<IPresenterHealthEnemy>, IVisibilityGame
    {
        private float _maxHP;
        [SerializeField] private TMP_Text _namePlayer;

        private IPresenterHealthEnemy _presenter;
        [SerializeField] private Slider _sliderHP;
        [SerializeField] private TMP_Text _textHP;

        public override ScreenEndGameType ScreenType =>
            ScreenEndGameType.Gameplay;

        public override void SetPresenterHandler(IPresenterHealthEnemy handler)
        {
            _presenter = handler;
        }

        public void UpdateHealth(float value)
        {
            _textHP.text = value.ToString(CultureInfo.InvariantCulture);
            _sliderHP.value = value / _maxHP;
        }

        public void SetEnemyInfo(float maxHP, string name)
        {
            _maxHP = maxHP;
            _namePlayer.text = name;
            _textHP.text = maxHP.ToString(CultureInfo.InvariantCulture);
            _sliderHP.value = 1f;
        }
    }
}