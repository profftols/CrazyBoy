using System.Collections.Generic;
using UnityEngine;

namespace SeizureTerritory.Scripts
{
    public class InitGame : MonoBehaviour
    {
        [SerializeField] private GameObject[] _prefabs;
        
        private void Start()
        {
            foreach (var prefab in _prefabs)
            {
                var obj = Instantiate(prefab);
            }
        }
    }
}