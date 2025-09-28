using _ProjectBoy.Scripts.UI.Gameplay.Runner.View;
using UnityEngine;
using UnityEngine.UI;

namespace _ProjectBoy.Scripts.UI.Gameplay.Fighter.View
{
    public class ButtonActionView : GameScreenView<IPresenterButtonAction>, IVisibilityGame
    {
        [SerializeField] private Button _attack;
        [SerializeField] private Button _battle;
        [SerializeField] private Button _defence;

        private IPresenterButtonAction _presenter;

        public override ScreenEndGameType ScreenType =>
            ScreenEndGameType.Gameplay;

        private void OnEnable()
        {
            _battle.onClick.AddListener(() => _presenter.OnBattleStart());
            _attack.onClick.AddListener(() => _presenter.OnDamageAction());
            _defence.onClick.AddListener(() => _presenter.OnDefenceAction());
        }

        private void OnDisable()
        {
            _battle.onClick.RemoveListener(() => _presenter.OnBattleStart());
            _attack.onClick.RemoveListener(() => _presenter.OnDamageAction());
            _defence.onClick.RemoveListener(() => _presenter.OnDefenceAction());
        }

        public override void SetPresenterHandler(IPresenterButtonAction handler)
        {
            _presenter = handler;
        }
    }
}