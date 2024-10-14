using UnityEngine;

public class UIEndGame : Panel
{
    public override void Show() => gameObject.SetActive(true);
    public override void Hide() => gameObject.SetActive(false);
}