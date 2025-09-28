using System;
using _ProjectBoy.Scripts.Core.CharacterSc;
using _ProjectBoy.Scripts.Core.MainMenu;
using _ProjectBoy.Scripts.Service;
using _ProjectBoy.Scripts.UI.MainMenu.View;
using UnityEngine;
using YG;

namespace _ProjectBoy.Scripts.UI.MainMenu
{
    public class MainMenuPresenter : Presenter<WindowsScreenView>,
        IPresenterMainMenu,
        IPresenterGamemode,
        IPresenterLanguage,
        IPresenterLeaderboard,
        IPresenterSetting,
        IPresenterSkin,
        IPresenterSupport,
        IPresenterCoin
    {
        private readonly IPlayerProgress _gamingProgress;
        private readonly ISkinDataContainer _skinData;
        private readonly IVisibilityMainMenu[] _windows;
        private float _scorePlayer;
        public Action<string> OnChangeScene;


        public MainMenuPresenter(WindowsScreenView view, ISkinDataContainer skinData,
            IPlayerProgress gamingProgress, float scorePlayer) : base(view)
        {
            _skinData = skinData;
            _gamingProgress = gamingProgress;
            _windows = view.WindowsView;
            _scorePlayer = scorePlayer;
        }

        public void ChangeScene(string sceneName)
        {
            OnChangeScene?.Invoke(sceneName);
        }

        public void SetLanguage(string language)
        {
            YG2.SwitchLanguage(language);
        }

        public void OnOpenWindows(ScreenMainType mainType)
        {
            foreach (var window in _windows)
                if (window.ScreenType == mainType)
                {
                    window.Show();

                    if (window.ScreenType == ScreenMainType.SkinsMenu) ShowSkinMenu();
                }
        }

        public int IndexSkin { get; private set; }

        public void OnBuy()
        {
            if (_gamingProgress.TryBuySkin(_skinData.GetSkin(IndexSkin), out var score))
            {
                View.SkinMenu.Buy.gameObject.SetActive(false);
                View.SkinMenu.Select.gameObject.SetActive(true);
                _scorePlayer = score;
                View.CoinView.SetCoinView(_scorePlayer);
            }
            else
            {
                Debug.LogError("you don't have enough progress");
            }
        }

        public void OnSelect()
        {
            var skin = _skinData.GetSkin(IndexSkin);

            if (skin == null)
            {
                Debug.LogError("skin is null");
            }
            else
            {
                _gamingProgress.SetSkin(skin);
                View.SkinMenu.Select.gameObject.SetActive(false);
            }
        }

        public void OnNextSkin()
        {
            IndexSkin++;
            ViewArrowInteractable();
            View.SkinMenu.SkinView.SwitchSkin(IndexSkin);
            CheckSkin(_skinData.GetSkin(IndexSkin));
        }

        public void OnBackSkin()
        {
            IndexSkin--;
            ViewArrowInteractable();
            View.SkinMenu.SkinView.SwitchSkin(IndexSkin);
            CheckSkin(_skinData.GetSkin(IndexSkin));
        }

        public void GetBonus()
        {
            var id = "coin";

            YG2.RewardedAdvShow(id, () =>
            {
                _gamingProgress.AddScore(10);
                View.CoinView.SetCoinView(_gamingProgress.Score);
            });
        }

        public void Initialize()
        {
            View.CoinView.SetCoinView(_scorePlayer);
            View.MainMenu.SetPresenterHandler(this);
            View.GameSelectionMenuMenu.SetPresenterHandler(this);
            View.LanguageMenu.SetPresenterHandler(this);
            View.LeaderboardMenu.SetPresenterHandler(this);
            View.SettingMenu.SetPresenterHandler(this);
            View.SkinMenu.SetPresenterHandler(this);
            View.SupportMenu.SetPresenterHandler(this);
            AddBaseSkinView();
        }

        private void ShowSkinMenu()
        {
            ViewArrowInteractable();
            CheckSkin(_skinData.GetSkin(IndexSkin));
        }

        private void CheckSkin(Skin skin)
        {
            var view = View.SkinMenu;

            if (_gamingProgress.HasSkin(skin))
            {
                view.Buy.gameObject.SetActive(false);
                view.Select.gameObject.SetActive(_gamingProgress.CurrentSkin != skin);
            }
            else
            {
                view.Buy.gameObject.SetActive(true);
                view.Select.gameObject.SetActive(false);
            }
        }

        private void AddBaseSkinView()
        {
            for (var i = 0; i < _skinData.Count; i++) View.SkinMenu.SkinView.SetViewSkins(_skinData.GetSkin(i));
        }

        private void ViewArrowInteractable()
        {
            if (0 >= IndexSkin)
                View.SkinMenu.LArrow.interactable = false;
            else if (IndexSkin >= _skinData.Count - 1)
                View.SkinMenu.RArrow.interactable = false;
            else
                View.SkinMenu.LArrow.interactable = View.SkinMenu.RArrow.interactable = true;
        }
    }
}