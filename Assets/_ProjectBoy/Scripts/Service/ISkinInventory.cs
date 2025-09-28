using _ProjectBoy.Scripts.Core.CharacterSc;
using _ProjectBoy.Scripts.Infostructure.Services;

namespace _ProjectBoy.Scripts.Service
{
    public interface ISkinInventory : IService
    {
        Skin CurrentSkin { get; }
        bool TryBuySkin(Skin skinObject, out float score);
        void SetSkin(Skin skinObject);
        bool HasSkin(Skin skinObject);
    }
}