using _ProjectBoy.Scripts.Environment;
using _ProjectBoy.Scripts.Infostructure.AssetManagement;
using UnityEngine;

namespace _ProjectBoy.Scripts.Infostructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;

        public GameFactory(IAssets assets)
        {
            _assets = assets;
        }

        public GameObject CreateCharacter(string path, SpawnPoint at)
        {
            return _assets.Instantiate(path, at.GetPointSpawn());
        }

        public void CreateHub(string path)
        {
            _assets.Instantiate(path);
        }

        public GameObject CreateGameElement(string path, Transform transform)
        {
            return _assets.InstantiateInParent(path, transform);
        }
    }
}