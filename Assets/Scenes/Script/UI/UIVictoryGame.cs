public class UIVictoryGame : UIPanel
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