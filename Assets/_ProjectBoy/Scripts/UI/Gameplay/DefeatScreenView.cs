using _ProjectBoy.Scripts.UI.Gameplay.Runner.View;
using UnityEngine;
using UnityEngine.UI;

namespace _ProjectBoy.Scripts.UI.Gameplay
{
    public class DefeatScreenView : GameScreenView<IPresenterGameOverScreen>, IVisibilityGame
    {
        private const string Mainmenu = "Main";

        private IPresenterGameOverScreen _presenter;

        [SerializeField] private Button mainMenu;

        public override ScreenEndGameType ScreenType =>
            ScreenEndGameType.Defeat;

        private void OnEnable()
        {
            Time.timeScale = 0f;

            mainMenu.onClick.AddListener(() => { _presenter.ChangeScene(Mainmenu); });
        }

        private void OnDisable()
        {
            Time.timeScale = 1f;

            mainMenu.onClick.RemoveListener(() => { _presenter.ChangeScene(Mainmenu); });
        }

        public override void SetPresenterHandler(IPresenterGameOverScreen handler)
        {
            _presenter = handler;
        }
    }
}