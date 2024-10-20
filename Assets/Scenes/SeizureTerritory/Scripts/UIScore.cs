public class UIScore : Panel
{
    public override void Show() => gameObject.SetActive(true);
    public override void Hide() => gameObject.SetActive(false);
}