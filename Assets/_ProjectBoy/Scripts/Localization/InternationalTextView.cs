using TMPro;
using UnityEngine;
using YG;

namespace _ProjectBoy.Scripts.Localization
{
    public class InternationalTextView : MonoBehaviour
    {
        [SerializeField] private string _en;
        [SerializeField] private TextMeshProUGUI _mesh;
        [SerializeField] private string _ru;
        [SerializeField] private string _tr;

        private void OnEnable()
        {
            YG2.onSwitchLang += ChangeLanguage;
            ChangeLanguage(YG2.lang);
        }

        private void OnDisable()
        {
            YG2.onSwitchLang -= ChangeLanguage;
        }

        private void ChangeLanguage(string currentLang)
        {
            _mesh.text = currentLang switch
            {
                Constants.English => _en,
                Constants.Russian => _ru,
                Constants.Turkish => _tr,
                _ => _mesh.text
            };
        }
    }
}