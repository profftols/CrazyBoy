using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _ProjectBoy.Scripts.Environment
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private Transform _point;

        private List<Transform> _spawnPoints;

        private void Start()
        {
            _spawnPoints = new List<Transform>(_point.childCount);

            for (var i = 0; i < _point.childCount; i++) _spawnPoints.Add(_point.GetChild(i));
        }

        public int GetCountSpawnPoints()
        {
            return _spawnPoints.Count;
        }

        public Transform GetPointSpawn()
        {
            var randomPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
            _spawnPoints.Remove(randomPoint);
            return randomPoint;
        }
    }
}