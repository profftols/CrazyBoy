using System;
using UnityEngine;

public class UIEndGame : Panel
{
    private void Start()
    {
        Hide();
    }

    public override void Show() => gameObject.SetActive(true);
    public override void Hide() => gameObject.SetActive(false);
}