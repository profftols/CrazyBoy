using System.Collections.Generic;
using UnityEngine;

namespace _ProjectBoy.Scripts.Core.RunnerGameplay
{
    public class SpawnColor : MonoBehaviour
    {
        private List<Material> _spawnPoints;
        [SerializeField] private MeshRenderer meshRenderer;

        private void Start()
        {
            _spawnPoints = new List<Material>(meshRenderer.materials.Length);

            for (var i = 0; i < meshRenderer.materials.Length; i++) _spawnPoints.Add(meshRenderer.materials[i]);
        }

        public int GetCountMaterials()
        {
            return _spawnPoints.Count;
        }

        public Material GetColor()
        {
            var randomMaterial = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
            _spawnPoints.Remove(randomMaterial);
            return randomMaterial;
        }
    }
}