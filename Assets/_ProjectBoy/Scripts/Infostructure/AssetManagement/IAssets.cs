using _ProjectBoy.Scripts.Infostructure.Services;
using UnityEngine;

namespace _ProjectBoy.Scripts.Infostructure.AssetManagement
{
    public interface IAssets : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Transform at);
        GameObject InstantiateInParent(string path, Transform parent);
    }
}