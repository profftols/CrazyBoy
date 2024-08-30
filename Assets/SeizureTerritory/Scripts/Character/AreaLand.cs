using System.Collections.Generic;
using UnityEngine;

namespace SeizureTerritory.Scripts.Character
{
    public class AreaLand
    {
        private readonly Colouring _colouring;
        private List<Land> _lands;
        private List<Land> _buffer;
        private Map _map;

        public AreaLand(Map map, Colouring colouring, Transform transform)
        {
            _lands = new List<Land>();
            _buffer = new List<Land>();
            _colouring = colouring;
            _map = map;
            _lands.AddRange(_colouring.Spawn(transform));
        }

        public Vector3 GetMinimumDistance(Vector3 position)
        {
            int minDistance = int.MaxValue;
            Vector3 closestDistance = new Vector3();

            foreach (var land in _lands)
            {
                int distance = (int)Vector3.Distance(land.transform.position, position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestDistance = land.transform.position;
                }
            }

            return closestDistance;
        }

        public bool TryAddLand(Land land)
        {
            if (_colouring.IsChangeLandMaterial(land, _lands))
            {
                _buffer.Add(land);
            }
            else if (Calculation.IsValidLands(_buffer))
            {
                if (_colouring.IsColorNotCorrect(_buffer))
                {
                    return false;
                }

                CompleteLand();
            }

            return true;
        }

        public void Clear()
        {
            _lands.AddRange(_buffer);

            foreach (var land in _lands)
            {
                _map.SetDefaultMaterial(land);
            }

            _lands = null;
            _buffer = null;
        }

        private void CompleteLand()
        {
            _lands.AddRange(_buffer);
            var lands = _map.TakeLands(_lands);
            _colouring.PaintInside(lands);
            _lands.AddRange(lands);
            _buffer.Clear();
        }
    }
}