using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace StartMenu.Scripts.MainMenu
{
    public class GameSelection : MenuActivity, IButtonActivity
    {
        [SerializeField] private Button _mainButton;
        [SerializeField] private RectTransform _blueRect;
        [SerializeField] private RectTransform _greenRect;
        [SerializeField] private RectTransform _frameRect;
        [SerializeField] private RectTransform _vsTextRect;

        private const float MultiplierHide = 6.69f;
        private const float MultiplierShow = 0.15f;
        private const float Duration = 0.5f;

        public EventType Type => EventType.StartGameMenu;
        public event Action<EventType> OnClick;

        private void OnEnable()
        {
            _mainButton.onClick.AddListener(OnMainMenu);
        }

        private void OnDisable()
        {
            _mainButton.onClick.AddListener(OnMainMenu);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _blueRect.transform.DOMoveX(_blueRect.transform.position.x * MultiplierShow, Duration);
            _greenRect.transform.DOMoveX(_greenRect.transform.position.x * MultiplierShow, Duration);
            _frameRect.DOScale(1, Duration);
            _vsTextRect.DOScale(0.25f, Duration);
        }

        public void Hide()
        {
            _blueRect.transform.DOMoveX(_blueRect.transform.position.x * MultiplierHide, Duration);
            _greenRect.transform.DOMoveX(_greenRect.transform.position.x * MultiplierHide, Duration);
            _frameRect.DOScale(0, Duration);
            _vsTextRect.DOScale(0, Duration);
            DOTween.Sequence()
                .AppendInterval(Duration)
                .AppendCallback(Conceal);
        }

        private void Conceal()
        {
            gameObject.SetActive(false);
        }

        private void OnMainMenu()
        {
            OnClick?.Invoke(EventType.MainMenu);
        }
    }
}