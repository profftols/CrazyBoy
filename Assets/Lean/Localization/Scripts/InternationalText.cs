using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class InternationalText : MonoBehaviour
{
    [SerializeField] private Language _language;
    [SerializeField] private string _en;
    [SerializeField] private string _ru;
    [SerializeField] private string _tr;

    private TextMeshProUGUI _mesh;

    private void Start()
    {
        _mesh = GetComponent<TextMeshProUGUI>();
        ChangeLanguage();
    }

    private void OnEnable()
    {
        _language.ChangingLanguage += OnChanger;
    }

    private void OnDisable()
    {
        _language.ChangingLanguage -= OnChanger;
    }

    private void OnChanger()
    {
        ChangeLanguage();
    }

    private void ChangeLanguage()
    {
        if (gameObject.activeSelf)
        {
            if (Language.Instance.CurrentLanguage == _language.English)
            {
                _mesh.text = _en;
            }
            else if (Language.Instance.CurrentLanguage == _language.Russian)
            {
                _mesh.text = _ru;
            }
            else if (Language.Instance.CurrentLanguage == _language.Turkish)
            {
                _mesh.text = _tr;
            }
            else
            {
                _mesh.text = _en;
            }
        }
    }
}