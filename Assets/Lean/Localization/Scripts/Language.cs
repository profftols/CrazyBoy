using System;
using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.UI;

public class Language : MonoBehaviour
{
    [HideInInspector] public string CurrentLanguage;
    
    [SerializeField] private Button _english;
    [SerializeField] private Button _russian;
    [SerializeField] private Button _turkish;
    
    public static Language Instance;
    
    public string English { get; private set; } = "en";
    public string Russian { get; private set; } = "ru";
    public string Turkish { get; private set; } = "tr";

    public event Action ChangingLanguage;
    
#if UNITY_WEBGL && !UNITY_EDITOR
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            CurrentLanguage = YandexGamesSdk.Environment.i18n.lang;
        }
        else
        {
            Destroy(gameObject);
        }
    }
#endif
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            CurrentLanguage = "en"; // tr  en  ru
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        _english.onClick.AddListener(ChangeEnglish);
        _russian.onClick.AddListener(ChangeRussian);
        _turkish.onClick.AddListener(ChangeTurkish);
    }

    private void OnDisable()
    {
        _english.onClick.RemoveListener(ChangeEnglish);
        _russian.onClick.RemoveListener(ChangeRussian);
        _turkish.onClick.RemoveListener(ChangeTurkish);
    }

    private void ChangeEnglish()
    {
        CurrentLanguage = English;
        ChangingLanguage.Invoke();
    }

    private void ChangeRussian()
    {
        CurrentLanguage = Russian;
        ChangingLanguage.Invoke();
    }

    private void ChangeTurkish()
    {
        CurrentLanguage = Turkish;
        ChangingLanguage.Invoke();
    }
}
