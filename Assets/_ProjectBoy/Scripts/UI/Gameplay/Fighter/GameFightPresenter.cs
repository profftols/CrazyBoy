using System;
using _ProjectBoy.Scripts.Core.FighterGameplay;
using _ProjectBoy.Scripts.Service;
using _ProjectBoy.Scripts.UI.Gameplay.Fighter.View;

namespace _ProjectBoy.Scripts.UI.Gameplay.Fighter
{
    public class GameFightPresenter : Presenter<WindowsFightView>, IPresenterButtonAction, IPresenterHealthPlayer,
        IPresenterHealthEnemy, IPresenterGameOverScreen
    {
        private const float WinPrise = 80f;
        private const float LoseFine = 50f;
        private readonly IFighterMode _fighterMode;

        private readonly IPlayerProgress _progress;

        public Action<string> OnChangeScene;

        public GameFightPresenter(WindowsFightView view, IPlayerProgress progress) : base(view)
        {
            _progress = progress;
            _fighterMode = new FighterMode(_progress.Score);
        }

        public void ChangeScene(string name)
        {
            OnChangeScene?.Invoke(name);
        }

        public void OnDamageAction()
        {
            _fighterMode.Player.AttackAction();
        }

        public void OnDefenceAction()
        {
            _fighterMode.Player.DefenceAction();
        }

        public void OnBattleStart()
        {
            _fighterMode.StartFight();
        }

        public void OnShowScreenGameOver(bool isWin)
        {
            if (isWin)
            {
                View.WinScreenView.Show();
                _progress.AddScore(WinPrise);
            }
            else
            {
                View.DefeatScreenView.Show();
                _progress.RemoveScore(LoseFine);
            }

            UnsubscribeEvent();
        }

        public void UpdateHealthEnemy(float value)
        {
            View.EnemyView.UpdateHealth(value);
        }

        public void UpdateHealthPlayer(float value)
        {
            View.PlayerView.UpdateHealth(value);
        }

        public void Initialize()
        {
            _fighterMode.InitGame();
            View.EnemyView.SetPresenterHandler(this);
            View.PlayerView.SetPresenterHandler(this);
            View.ButtonActionView.SetPresenterHandler(this);
            View.DefeatScreenView.SetPresenterHandler(this);
            View.WinScreenView.SetPresenterHandler(this);
            View.PlayerView.SetPlayerInfo(_fighterMode.Player.Health.MaxHp, _fighterMode.Player.Name);
            View.EnemyView.SetEnemyInfo(_fighterMode.Enemy.Health.MaxHp, _fighterMode.Enemy.Name);
            SubscribeEvent();
        }

        public void PauseFight()
        {
            _fighterMode.StopFight();
        }

        private void SubscribeEvent()
        {
            _fighterMode.SubscribeEvent();
            _fighterMode.Player.OnHpChange += UpdateHealthPlayer;
            _fighterMode.Enemy.OnHpChange += UpdateHealthEnemy;
            _fighterMode.Player.Health.OnDead += () => OnShowScreenGameOver(false);
            _fighterMode.Enemy.Health.OnDead += () => OnShowScreenGameOver(true);
        }

        private void UnsubscribeEvent()
        {
            _fighterMode.UnsubscribeEvent();
            _fighterMode.Player.OnHpChange -= UpdateHealthPlayer;
            _fighterMode.Enemy.OnHpChange -= UpdateHealthEnemy;
            _fighterMode.Player.Health.OnDead -= () => OnShowScreenGameOver(false);
            _fighterMode.Enemy.Health.OnDead -= () => OnShowScreenGameOver(true);
        }
    }
}