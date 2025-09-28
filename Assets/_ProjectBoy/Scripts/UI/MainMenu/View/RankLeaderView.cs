using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _ProjectBoy.Scripts.UI.MainMenu.View
{
    public class RankLeaderView : MonoBehaviour
    {
        [SerializeField] private Sprite _bronzeSprite;

        [Header("Rank Sprites")] [SerializeField]
        private Sprite _goldSprite;

        [Header("UI Elements")] [SerializeField]
        private Image _imageRank;

        [SerializeField] private Sprite _silverSprite;

        [SerializeField] private TextMeshProUGUI _textRank;

        public void Start()
        {
            Sprite targetSprite = null;
            int.TryParse(_textRank.text, out var rank);

            switch (rank)
            {
                case 1:
                    targetSprite = _goldSprite;
                    break;
                case 2:
                    targetSprite = _silverSprite;
                    break;
                case 3:
                    targetSprite = _bronzeSprite;
                    break;
            }

            if (targetSprite != null)
            {
                _imageRank.gameObject.SetActive(true);
                _imageRank.sprite = targetSprite;
                _imageRank.SetNativeSize();
                _textRank.gameObject.SetActive(false);
            }
            else
            {
                _imageRank.gameObject.SetActive(false);
                _textRank.gameObject.SetActive(true);
            }
        }
    }
}