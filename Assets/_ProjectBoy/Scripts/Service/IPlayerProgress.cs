using _ProjectBoy.Scripts.Core.CharacterSc;
using _ProjectBoy.Scripts.Infostructure.Services;

namespace _ProjectBoy.Scripts.Service
{
    public interface IPlayerProgress : IService
    {
        Skin CurrentSkin { get; }
        int Score { get; }
        void AddScore(float score);
        void RemoveScore(float score);
        bool TryBuySkin(Skin skinObject, out float score);
        void SetSkin(Skin skinObject);
        bool HasSkin(Skin skinObject);
    }
}