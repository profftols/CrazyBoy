using UnityEngine;

namespace _ProjectBoy.Scripts.Infostructure.AssetManagement
{
    public class AssetProvider : IAssets
    {
        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string path, Transform pos)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, pos.position, pos.rotation);
        }

        public GameObject InstantiateInParent(string path, Transform parent)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, prefab.transform.position, parent.rotation, parent);
        }
    }
}