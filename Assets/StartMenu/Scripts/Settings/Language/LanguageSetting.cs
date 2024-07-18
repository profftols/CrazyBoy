using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSetting : MonoBehaviour
{
    [SerializeField] private Language _language;
    [SerializeField] private Button _buttonClose;
    [SerializeField] private Image _eng;
    [SerializeField] private Image _rus;
    [SerializeField] private Image _tur;

    private void OnDisable()
    {
        _language.ChangingLanguage -= ChangeLang;
        _buttonClose.onClick.RemoveListener(CloseWindow);
    }

    private void OnEnable()
    {
        _language.ChangingLanguage += ChangeLang;
        _buttonClose.onClick.AddListener(CloseWindow);
    }

    private void CloseWindow()
    {
        gameObject.SetActive(false);
    }
    
    private void ChangeLang()
    {
        if(Language.Instance.CurrentLanguage == _language.English)
        {
            _eng.gameObject.SetActive(true);
            _rus.gameObject.SetActive(false);
            _tur.gameObject.SetActive(false);
        }
        else if(Language.Instance.CurrentLanguage == _language.Russian)
        {
            _eng.gameObject.SetActive(false);
            _rus.gameObject.SetActive(true);
            _tur.gameObject.SetActive(false);
        }
        else if(Language.Instance.CurrentLanguage == _language.Turkish)
        {
            _eng.gameObject.SetActive(false);
            _rus.gameObject.SetActive(false);
            _tur.gameObject.SetActive(true);
        }
        else
        {
            _eng.gameObject.SetActive(true);
            _rus.gameObject.SetActive(false);
            _tur.gameObject.SetActive(false);
        }
    }
}
