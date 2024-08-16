using UnityEngine;

public class MenuActivity : MonoBehaviour, IView
{
    [SerializeField] private MainMenuView _main;
    [SerializeField] private MenuSettings _menu;

    public void ShowAudio()
    {
        if (_main.isActiveAndEnabled)
        {
            _main.gameObject.SetActive(false);
            _menu.gameObject.SetActive(true);
        }
    }

    public void ShowMain()
    {
        if (_menu.isActiveAndEnabled)
        {
            _menu.gameObject.SetActive(false);
            _main.gameObject.SetActive(true);
        }
    }
}