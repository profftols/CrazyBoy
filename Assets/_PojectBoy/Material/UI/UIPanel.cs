using System.Text;
using TMPro;
using UnityEngine;

public class UIPanel : MonoBehaviour, IVisible
{
    [SerializeField] protected TMP_Text _textView;

    protected TMP_Text TextView { get; private set; }
    private StringBuilder _sb;

    private void Awake()
    {
        TextView = _textView;
        _sb = new StringBuilder();
    }
    
    public void Show() => gameObject.SetActive(true);
    public void Hide() => gameObject.SetActive(false);
    
    protected string CombineString(string text, float score)
    {
        _sb.Clear();
        _sb.Append(text);
        _sb.Append(score.ToString("F2"));
        return _sb.ToString();
    }
}