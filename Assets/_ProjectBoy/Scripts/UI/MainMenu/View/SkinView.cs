using System.Collections.Generic;
using System.Globalization;
using _ProjectBoy.Scripts.Core.CharacterSc;
using TMPro;
using UnityEngine;

namespace _ProjectBoy.Scripts.UI.MainMenu.View
{
    public class SkinView : MonoBehaviour
    {
        private readonly List<Skin> _skins = new();
        private Skin _currentSkin;
        [SerializeField] private TMP_Text _price;

        public void SetViewSkins(Skin skinPrefab)
        {
            var skin = Instantiate(skinPrefab, transform);

            if (skin.Price == 0) _currentSkin = skin;

            _skins.Add(skin);
            skin.gameObject.SetActive(false);
            _currentSkin.gameObject.SetActive(true);
        }

        public void SwitchSkin(int index)
        {
            _currentSkin.gameObject.SetActive(false);
            _currentSkin = _skins[index];
            _currentSkin.gameObject.SetActive(true);
            _price.text = _skins[index].Price.ToString("N0", CultureInfo.InvariantCulture);
        }
    }
}