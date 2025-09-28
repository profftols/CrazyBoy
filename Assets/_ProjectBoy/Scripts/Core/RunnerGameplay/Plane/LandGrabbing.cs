using System;
using System.Collections.Generic;
using System.Linq;
using _ProjectBoy.Scripts.Core.CharacterSc;

namespace _ProjectBoy.Scripts.Core.RunnerGameplay.Plane
{
    public class LandGrabbing
    {
        private readonly Dictionary<Land, Runner> _buffers;
        private readonly List<Runner> _deathList;
        private readonly int _lastPlayer = 1;

        private readonly Map _map;
        private readonly Dictionary<Land, Runner> _owner;

        public LandGrabbing(Map map)
        {
            _map = map;
            _buffers = new Dictionary<Land, Runner>();
            _owner = new Dictionary<Land, Runner>();
            _deathList = new List<Runner>();
            Instance = this;
        }

        public static LandGrabbing Instance { get; private set; }

        public event Action<float> OnScore;
        public event Action<bool> OnSurvive;

        public void Start(List<Land> lands, Runner runner)
        {
            foreach (var land in lands) _owner.Add(land, runner);
        }

        public void CheckLand(Runner runner, Land land)
        {
            if (_owner.ContainsKey(land))
            {
                _owner.TryGetValue(land, out var player);

                if (player == runner)
                {
                    if (_deathList.Contains(runner))
                    {
                        KillCharacter(runner);
                        return;
                    }

                    if (_buffers.ContainsValue(runner)) ConquerInside(runner);
                }
                else
                {
                    WriteItDown(land, runner);
                }
            }
            else if (_buffers.ContainsKey(land))
            {
                _buffers.TryGetValue(land, out var player);

                if (player != runner)
                {
                    _buffers.Remove(land);
                    WriteItDown(land, runner);
                    _deathList.Add(player);
                }
            }
            else
            {
                WriteItDown(land, runner);
            }
        }

        public bool IsLandUnclaimed(Land land)
        {
            return !_owner.ContainsKey(land) && !_buffers.ContainsKey(land);
        }

        public bool IsMyLand(Land land, Runner runner)
        {
            if (_owner.TryGetValue(land, out var owner)) return owner == runner;

            return false;
        }

        public List<Land> GetOwnedLands(Runner runner)
        {
            return _owner.Where(kvp => kvp.Value == runner)
                .Select(kvp => kvp.Key)
                .ToList();
        }

        private void KillCharacter(Runner runner)
        {
            runner.OnLand -= CheckLand;
            _deathList.Remove(runner);
            DeleteLand(_owner, runner);
            DeleteLand(_buffers, runner);

            if (runner.Character.GetType() == typeof(Player))
                OnSurvive?.Invoke(false);
            else if (_owner.Values.Distinct().Count() == _lastPlayer)
                OnSurvive?.Invoke(true);

            runner.OnDead();
        }

        private void DeleteLand(Dictionary<Land, Runner> lands, Runner runner)
        {
            foreach (var land in lands.ToList())
                if (land.Value == runner)
                {
                    _buffers.Remove(land.Key);
                    _owner.Remove(land.Key);

                    if (!land.Key.IsPainted(runner.Material)) _map.SetDefaultMaterial(land.Key);

                    land.Key.DeactivationOutline();
                }
        }

        private void ConquerInside(Runner runner)
        {
            var lands = _buffers.Where(x =>
                x.Value == runner).Select(x =>
                x.Key).ToList();

            float score = lands.Count;

            AddOwner(lands, runner);
            RemoveBuffers(lands);

            lands.AddRange(_owner.Where(x =>
                x.Value == runner).Select(x =>
                x.Key).ToList());

            var landsInside = _map.TakeLands(lands);

            if (runner.Character.GetType() == typeof(Player))
            {
                score += landsInside.Count;
                var value = (float)Math.Round(score / _map.Size * 100f, 2);
                OnScore?.Invoke(value);
            }

            RemoveBuffers(landsInside);
            AddOwner(landsInside, runner);
        }

        private void AddOwner(List<Land> lands, Runner runner)
        {
            foreach (var land in lands)
            {
                if (_owner.ContainsKey(land)) _owner.Remove(land);

                _owner.Add(land, runner);
                land.DeactivationOutline();
                land.SetMaterial(runner.Material);
            }
        }

        private void WriteItDown(Land land, Runner runner)
        {
            _buffers.Add(land, runner);

            if (_owner.ContainsKey(land)) _owner.Remove(land);

            land.SetMaterial(runner.Material);
            land.ActivationOutline();
        }

        private void RemoveBuffers(List<Land> lands)
        {
            foreach (var land in lands) _buffers.Remove(land);
        }
    }
}