using _ProjectBoy.Scripts.Core.CharacterSc;
using _ProjectBoy.Scripts.Infostructure.Services;

namespace _ProjectBoy.Scripts.Core.MainMenu
{
    public interface ISkinDataContainer : IService
    {
        public int Count { get; }
        public Skin GetSkin(int index);
    }
}