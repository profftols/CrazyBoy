using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTransitionMainMenu : MonoBehaviour
{
    private const int MainMenu = 0;
    
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(ChangeScene);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ChangeScene);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(MainMenu);
    }
}