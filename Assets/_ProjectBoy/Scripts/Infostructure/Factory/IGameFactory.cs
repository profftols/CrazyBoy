using _ProjectBoy.Scripts.Environment;
using _ProjectBoy.Scripts.Infostructure.Services;
using UnityEngine;

namespace _ProjectBoy.Scripts.Infostructure.Factory
{
    public interface IGameFactory : IService
    {
        GameObject CreateCharacter(string path, SpawnPoint at);
        void CreateHub(string path);
        GameObject CreateGameElement(string path, Transform transform);
    }
}