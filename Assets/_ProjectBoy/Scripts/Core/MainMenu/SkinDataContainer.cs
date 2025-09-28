using System.Linq;
using _ProjectBoy.Scripts.Core.CharacterSc;

namespace _ProjectBoy.Scripts.Core.MainMenu
{
    public class SkinDataContainer : ISkinDataContainer
    {
        private readonly Skin[] _skins;

        public SkinDataContainer(Skin[] skins)
        {
            _skins = skins.OrderBy(x => x.Price).ToArray();
        }

        public int Count => _skins.Length;

        public Skin GetSkin(int index)
        {
            if (_skins.Length <= index)
                return null;

            return 0 > index ? null : _skins[index];
        }
    }
}