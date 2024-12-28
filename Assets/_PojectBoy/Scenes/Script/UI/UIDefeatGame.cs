public class UIDefeatGame : UIPanel
{
    public void ViewScreen(float value)
    {
        if (value > 0)
        {
            TextView.text = CombineString(TextView.text, value);
        }
    }
}