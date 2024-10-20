using System;
using StartMenu.Scripts;
using StartMenu.Scripts.MainMenu;
using UnityEngine;
using UnityEngine.UI;
using EventType = StartMenu.Scripts.MainMenu.EventType;

public class Support : MenuActivity, IButtonActivity
{
    [SerializeField] private Button _mainButton;

    public EventType Type => EventType.SupportMenu;

    public event Action<EventType> OnClick;
    public void Show() => gameObject.SetActive(true);
    public void Hide() => gameObject.SetActive(false);

    private void OnEnable()
    {
        _mainButton.onClick.AddListener(OnMainMenu);
    }

    private void OnDisable()
    {
        _mainButton.onClick.RemoveListener(OnMainMenu);
    }

    private void OnMainMenu()
    {
        OnClick?.Invoke(EventType.MainMenu);
    }
}
