using UnityEngine;
using UnityEngine.UI;

public class MenuSettings : MonoBehaviour
{
    [SerializeField] private Button BackButton;
    [SerializeField] private Button _buttonLanguage;
    [SerializeField] private LanguageSetting _languageSetting;
    [SerializeField] private MenuActivity _activityMenu;

    private void OnDisable()
    {
        BackButton.onClick.RemoveListener(BackMenu);
        _buttonLanguage.onClick.RemoveListener(OpenWindow);
    }

    private void OnEnable()
    {
        BackButton.onClick.AddListener(BackMenu);
        _buttonLanguage.onClick.AddListener(OpenWindow);
    }

    private void OpenWindow()
    {
        _languageSetting.gameObject.SetActive(true);
    }

    private void BackMenu()
    {
        _activityMenu.ShowMain();
    }
}