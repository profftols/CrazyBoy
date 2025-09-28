using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _ProjectBoy.Scripts.Core.RunnerGameplay.Plane
{
    public class Map : MonoBehaviour
    {
        private Renderer _defaultLand;
        [SerializeField] private Land _land;
        private Land[,] _lands;

        [SerializeField] private Renderer _renderer;

        [field: SerializeField] public int SizeX { get; private set; }
        [field: SerializeField] public int SizeY { get; private set; }
        public static Map Instance { get; private set; }

        public int Size => SizeY * SizeY;

        private void Awake()
        {
            _lands = new Land[SizeX, SizeY];

            for (var x = 0; x < SizeX; x++)
            for (var y = 0; y < SizeY; y++)
            {
                var spawnPosition = new Vector3(x, 0, y);
                var land = Instantiate(_land, spawnPosition, Quaternion.identity);
                _lands[x, y] = land;
                land.transform.SetParent(gameObject.transform);
            }

            _defaultLand = _lands[0, 0].GetComponent<Renderer>();
            Instance = this;
        }

        public void SetDefaultMaterial(Land land)
        {
            land.SetMaterial(_defaultLand.material);
        }

        public List<Land> TakeLands(List<Land> lands)
        {
            var temp = new List<Land>();
            var visited = new List<Land>();

            var minPointX = (int)lands.Min(l => l.transform.position.x);
            var maxPointX = (int)lands.Max(l => l.transform.position.x);
            var minPointZ = (int)lands.Min(l => l.transform.position.z);
            var maxPointZ = (int)lands.Max(l => l.transform.position.z);

            minPointX = minPointX > 0 ? minPointX -= 1 : minPointX = 0;
            maxPointX = maxPointX < SizeX - 1 ? maxPointX += 1 : maxPointX;
            minPointZ = minPointZ > 0 ? minPointZ -= 1 : minPointZ = 0;
            maxPointZ = maxPointZ < SizeY - 1 ? maxPointZ += 1 : maxPointZ;

            var directions = new Vector3Int[]
            {
                new(1, 0, 0),
                new(-1, 0, 0),
                new(0, 0, 1),
                new(0, 0, -1)
            };

            for (var x = minPointX; x < maxPointX; x++)
            for (var y = minPointZ; y < maxPointZ; y++)
                if (x == minPointX || x == maxPointX || y == minPointZ || y == maxPointZ)
                    if (TryGetLand(lands, (uint)x, (uint)y))
                        visited.Add(_lands[x, y]);

            for (var i = 0; i < visited.Count; i++)
                foreach (var direction in directions)
                {
                    var position =
                        new Vector3Int((int)visited[i].transform.position.x, 0, (int)visited[i].transform.position.z) +
                        direction;

                    if ((position.x < maxPointX && position.z < maxPointZ)
                        || (position.x > minPointX && position.z > minPointZ))
                        if (TryGetLand(lands, (uint)position.x, (uint)position.z) &&
                            TryGetLand(visited, (uint)position.x, (uint)position.z))
                            visited.Add(_lands[position.x, position.z]);
                }

            for (var x = minPointX; x < maxPointX; x++)
            for (var y = minPointZ; y < maxPointZ; y++)
                if (!lands.Contains(_lands[x, y]) && !visited.Contains(_lands[x, y]))
                {
                    temp.Add(_lands[x, y]);
                    _lands[x, y].ActivationOutline();
                }

            return temp;
        }

        public Land GetLandAt(int x, int y)
        {
            return _lands[x, y];
        }

        private bool TryGetLand(List<Land> lands, uint x, uint y)
        {
            if (x >= SizeX || y >= SizeY) return false;

            if (lands.Contains(_lands[x, y])) return false;

            return true;
        }
    }
}