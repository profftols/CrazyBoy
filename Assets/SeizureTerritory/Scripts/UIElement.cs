using System;
using System.Globalization;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIElement : MonoBehaviour
{
    [SerializeField] private TMP_Text _score;

    private readonly int _sizeMap = Map.Size;
    private string _text = "Score: ";
    private StringBuilder _sb;

    private void Awake()
    {
    }

    private void OnEnable()
    {
        EventBus.OnScore += AddScore;
    }
    
    private void Start()
    {
        _sb = new StringBuilder();
    }

    private void OnDisable()
    {
        EventBus.OnScore -= AddScore;
    }

    private void AddScore(int score)
    {
        var value = (float)score / _sizeMap * 100f;
        _sb.Append(_text);
        _sb.Append(value.ToString(CultureInfo.InvariantCulture));
        
        _score.text = _sb.ToString();
    }
}