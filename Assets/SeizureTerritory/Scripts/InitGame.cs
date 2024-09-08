using UnityEngine;

namespace SeizureTerritory.Scripts
{
    public class InitGame : MonoBehaviour
    {
        [SerializeField] private GameObject[] _prefabs;
        
        private void Awake()
        {
            foreach (var prefab in _prefabs)
            {
                Instantiate(prefab);
            }
        }
    }
}