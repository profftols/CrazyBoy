using System;
using System.Linq;
using System.Text;
using UnityEngine;

public class UIScore : UIPanel
{
    private readonly string _textViewScore = "Score: ";
    private UIScore _score;
    
    protected virtual void OnEnable()
    {
        EventBus.OnScore += AddScore;
    }

    private void Start()
    {
        _score = this;
    }

    protected virtual void OnDisable()
    {
        EventBus.OnScore -= AddScore;
    }
    
    private void AddScore(float score)
    {
        var result = float.Parse(_score.TextView.text.Replace(_textViewScore, ""));
        TextView.text = CombineString(_textViewScore, score + result);
    }
}