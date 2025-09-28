namespace _ProjectBoy.Scripts.UI.Gameplay
{
    public interface IPresenterGame
    {
        void ChangeScene(string name);
    }

    public interface IPresenterGameOverScreen : IPresenterGame
    {
        void OnShowScreenGameOver(bool isWin);
    }

    public interface IPresenterScoreScreen : IPresenterGame
    {
        void OnScoreChange(float value);
    }

    public interface IPresenterHealthPlayer : IPresenterGame
    {
        void UpdateHealthPlayer(float value);
    }

    public interface IPresenterHealthEnemy : IPresenterGame
    {
        void UpdateHealthEnemy(float value);
    }

    public interface IPresenterButtonAction : IPresenterGame
    {
        void OnDamageAction();
        void OnDefenceAction();
        void OnBattleStart();
    }
}