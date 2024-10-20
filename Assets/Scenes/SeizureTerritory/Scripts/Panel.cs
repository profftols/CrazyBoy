using TMPro;
using UnityEngine;

public abstract class Panel : MonoBehaviour
{
    [SerializeField] protected TMP_Text _textView;
    
    public TMP_Text TextView { get; private set; }

    private void Awake()
    {
        TextView = _textView;
    }
    
    public abstract void Show();
    public abstract void Hide();
}