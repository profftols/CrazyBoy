using System.Collections.Generic;
using SeizureTerritory.Scripts.Character;
using UnityEngine;

namespace SeizureTerritory.Scripts.Plane
{
    public class ManagerLand : MonoBehaviour
    {
        [SerializeField] private Map _map;
        
        private Dictionary<IDeathHandler, Land> _buffers;
        private Dictionary<IDeathHandler, Land> _owner;

        private global::Character _character;

        private void OnEnable()
        {
            _character.OnLand += AddBuffer;
        }

        private void OnDisable()
        {
            _character.OnLand -= AddBuffer;
        }
        
        private void AddBuffer(IDeathHandler deathHandler, Land land)
        {
            _buffers.Add(deathHandler, land);
        }
    }
}