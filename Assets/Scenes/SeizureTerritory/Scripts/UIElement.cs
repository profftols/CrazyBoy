using System.Linq;
using System.Text;
using UnityEngine;

public class UIElement : MonoBehaviour
{
    [SerializeField] private Panel[] _panels;
    
    private readonly string _textViewScore = "Score: ";
    private readonly string _textViewEndScreen = "Your score: ";
    private StringBuilder _sb;
    private UIEndGame _endGame;
    private UIScore _score;

    private void OnEnable()
    {
        EventBus.OnScore += AddScore;
        EventBus.OnGameOver += ShowEndScreen;
    }
    
    private void Start()
    {
        _sb = new StringBuilder();
        _endGame = _panels.OfType<UIEndGame>().FirstOrDefault();
        _score = _panels.OfType<UIScore>().FirstOrDefault();
    }

    private void OnDisable()
    {
        EventBus.OnScore -= AddScore;
        EventBus.OnGameOver -= ShowEndScreen;
    }

    private void ShowEndScreen(float score)
    {
        _score.Hide();
        _endGame.Show();
        _endGame.TextView.text = CombineString(_textViewEndScreen, score);
    }

    private void AddScore(float score)
    {
        var result = float.Parse(_score.TextView.text.Replace(_textViewScore, ""));
        _score.TextView.text = CombineString(_textViewScore, score + result);
    }

    private string CombineString(string text, float score)
    {
        _sb.Clear();
        _sb.Append(text);
        _sb.Append(score.ToString("F2"));
        return _sb.ToString();
    }
}