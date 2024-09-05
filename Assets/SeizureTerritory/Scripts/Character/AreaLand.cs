﻿using System.Collections.Generic;
using UnityEngine;

namespace SeizureTerritory.Scripts.Character
{
    public class AreaLand
    {
        private readonly Colouring _colouring;
        private readonly Map _map;
        private IDeathHandler _deathHandler;
        private List<Land> _buffer;
        private List<Land> _lands;

        public AreaLand(Map map, Colouring colouring, Transform transform)
        {
            _lands = new List<Land>();
            _buffer = new List<Land>();
            _colouring = colouring;
            _map = map;
            _lands.AddRange(_colouring.Spawn(transform));
            EventBus.OnEnemyLand += Die;
        }
        
        public void SetDeathHandler(IDeathHandler deathHandler)
        {
            _deathHandler = deathHandler;
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

        public void AddLand(Land land)
        {
            if (_lands.Contains(land) == false)
            {
                _buffer.Add(land);
                _colouring.ChangeMaterial(land);
            }
            else if (Calculation.IsValidLands(_buffer))
            {
                if (_colouring.IsColorNotCorrect(_buffer))
                {
                    return;
                }

                CompleteLand();
            }
        }

        public void Clear()
        {
            _lands.AddRange(_buffer);

            foreach (var land in _lands)
            {
                _map.SetDefaultMaterial(land);
            }

            for (int i = 0; i < _lands.Count; i++)
            {
                _lands.Remove(_lands[i]);
            }

            _lands = null;
            _buffer = null;
            EventBus.OnEnemyLand -= Die;
        }

        private void CompleteLand()
        {
            foreach (var land in _buffer)
            {
                land.DeactOutline();
            }
            
            _lands.AddRange(_buffer);
            _buffer.Clear();
            var lands = _map.TakeLands(_lands);
            _colouring.PaintInside(lands);
            _lands.AddRange(lands);
            KillEnemy();
        }

        private void Die(Land land)
        {
            if (_buffer.Contains(land))
            {
                _deathHandler.HandleDeath();
            }
        }

        private void KillEnemy()
        {
            foreach (var land in _lands)
            {
                EventBus.OnEnemyLand?.Invoke(land);
            }
        }
    }
}