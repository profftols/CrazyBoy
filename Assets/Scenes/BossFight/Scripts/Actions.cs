using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Actions : MonoBehaviour
{
    [SerializeField] protected Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(Action);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Action);
    }

    public abstract void Action();
}
