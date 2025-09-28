using System;
using System.Collections.Generic;
using _ProjectBoy.Scripts.Audio;
using _ProjectBoy.Scripts.Core.CharacterSc;
using _ProjectBoy.Scripts.Core.RunnerGameplay.Bonus.BonusObject;
using _ProjectBoy.Scripts.Core.RunnerGameplay.Plane;
using UnityEngine;

namespace _ProjectBoy.Scripts.Core.RunnerGameplay
{
    public class Runner
    {
        private readonly float _distance = 1f;

        private readonly float _radius = 3f;
        private readonly Transform _startPosition;

        public Runner(IDeathHandler character, Material material, Transform transform, Mover mover)
        {
            Character = character;
            Material = material;
            _startPosition = transform;
            Mover = mover;
        }

        public Mover Mover { get; private set; }
        public IDeathHandler Character { get; }
        public Material Material { get; }
        public event Action<Runner, Land> OnLand;

        public List<Land> Start()
        {
            var lands = new List<Land>();
            var origin = _startPosition.position;
            var direction = _startPosition.forward;

            var hits = Physics.SphereCastAll(origin, _radius, direction, _distance);

            foreach (var hit in hits)
                if (hit.collider.gameObject.TryGetComponent(out Land land))
                {
                    lands.Add(land);
                    land.SetMaterial(Material);
                }

            return lands;
        }

        public void OnTrigger(Collider collider)
        {
            if (collider.gameObject.TryGetComponent(out Land land)) OnLand?.Invoke(this, land);

            if (collider.gameObject.TryGetComponent(out Item bonus)) bonus.OnPickUp(this);
        }

        public void OnDead()
        {
            Character.HandleDeath();
            MasterSoundSettings.Instance.soundEffects.PlayDeath();
        }
    }
}