using System.Collections.Generic;
using System.Linq;
using _ProjectBoy.Scripts.Core.RunnerGameplay.Plane;
using UnityEngine;

namespace _ProjectBoy.Scripts.Core.RunnerGameplay
{
    public class EnemyBrains
    {
        private const float ArriveRadius = 1.0f;
        private const int PathLengthForCapture = 2;
        private readonly LandGrabbing _landGrabbing;

        private readonly List<Land> _route = new();
        private readonly Runner _runner;
        private readonly float _scanRadius;

        private readonly Transform _transform;
        private int _currentRouteIndex;

        private AiState _currentState = AiState.SearchingForPath;

        public EnemyBrains(Transform transform, Runner runner, LandGrabbing landGrabbing, float scanRadius)
        {
            _transform = transform;
            _runner = runner;
            _landGrabbing = landGrabbing;
            _scanRadius = scanRadius;
        }

        public void Tick()
        {
            if (_route.Count > 0 && _route[_currentRouteIndex] == null) ClearRouteAndSearch();

            switch (_currentState)
            {
                case AiState.SearchingForPath:
                    FindPathToUnclaimedLands();
                    break;
                case AiState.MovingOnPath:
                    FollowCaptureRoute();
                    break;
                case AiState.ReturningToBase:
                    FollowReturnRoute();
                    break;
            }
        }

        public Vector3 GetDirection()
        {
            if (_route.Count > 0 && _currentRouteIndex < _route.Count)
            {
                var target = _route[_currentRouteIndex];
                if (target != null) return (target.transform.position - _transform.position).normalized;
            }

            return Vector3.zero;
        }

        private void FindPathToUnclaimedLands()
        {
            _route.Clear();
            _currentRouteIndex = 0;

            var currentPosition = _transform.position;

            for (var i = 0; i < PathLengthForCapture; i++)
            {
                var nearestLand = FindNearestUnclaimedLand(currentPosition, _route);
                if (nearestLand != null)
                {
                    _route.Add(nearestLand);
                    currentPosition = nearestLand.transform.position;
                }
                else
                {
                    break;
                }
            }

            if (_route.Count > 0)
                _currentState = AiState.MovingOnPath;
            else
                SetRouteToRandomOwnedLand();
        }

        private void FollowCaptureRoute()
        {
            if (_route.Count == 0)
            {
                ClearRouteAndSearch();
                return;
            }

            var currentTarget = _route[_currentRouteIndex];
            if (Vector3.Distance(_transform.position, currentTarget.transform.position) <= ArriveRadius)
            {
                _currentRouteIndex++;

                if (_currentRouteIndex >= _route.Count)
                {
                    SetRouteToNearestOwnedLand();
                    _currentState = AiState.ReturningToBase;
                }
            }
        }

        private void FollowReturnRoute()
        {
            if (_route.Count == 0)
            {
                ClearRouteAndSearch();
                return;
            }

            var currentTarget = _route[_currentRouteIndex];

            if (Vector3.Distance(_transform.position, currentTarget.transform.position) <= ArriveRadius)
                ClearRouteAndSearch();
        }

        private Land FindNearestUnclaimedLand(Vector3 fromPosition, List<Land> excludeList)
        {
            return Physics.OverlapSphere(fromPosition, _scanRadius)
                .Select(hit => hit.GetComponent<Land>())
                .Where(land => land != null && _landGrabbing.IsLandUnclaimed(land) && !excludeList.Contains(land))
                .OrderBy(land => Vector3.Distance(fromPosition, land.transform.position))
                .FirstOrDefault();
        }

        private void SetRouteToNearestOwnedLand()
        {
            var ownedLands = _landGrabbing.GetOwnedLands(_runner);

            if (ownedLands.Count > 0)
            {
                var nearestOwned = ownedLands
                    .OrderBy(l => Vector3.Distance(_transform.position, l.transform.position))
                    .FirstOrDefault();

                _route.Clear();
                _route.Add(nearestOwned);
                _currentRouteIndex = 0;
            }
            else
            {
                ClearRouteAndSearch();
            }
        }

        private void SetRouteToRandomOwnedLand()
        {
            var ownedLands = _landGrabbing.GetOwnedLands(_runner);

            if (ownedLands.Count > 1)
            {
                var otherLands = ownedLands
                    .Where(l => Vector3.Distance(_transform.position, l.transform.position) > ArriveRadius)
                    .ToList();

                if (otherLands.Count > 0)
                {
                    _route.Clear();
                    _route.Add(otherLands[Random.Range(0, otherLands.Count)]);
                    _currentRouteIndex = 0;
                    _currentState = AiState.ReturningToBase;
                    return;
                }
            }

            ClearRouteAndSearch();
        }

        private void ClearRouteAndSearch()
        {
            _route.Clear();
            _currentRouteIndex = 0;
            _currentState = AiState.SearchingForPath;
        }

        private enum AiState
        {
            SearchingForPath,
            MovingOnPath,
            ReturningToBase
        }
    }
}