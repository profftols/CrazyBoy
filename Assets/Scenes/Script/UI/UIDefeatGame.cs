public class UIDefeatGame : UIPanel
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ViewScreen(float value)
    {
        TextView.text = CombineString(TextView.text, value);
    }
}