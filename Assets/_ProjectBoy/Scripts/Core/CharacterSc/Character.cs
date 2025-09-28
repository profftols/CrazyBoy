using System;
using UnityEngine;

namespace _ProjectBoy.Scripts.Core.CharacterSc
{
    public abstract class Character : MonoBehaviour, IDeathHandler
    {
        private Skin _skin;
        [SerializeField] protected SkinHandler skinHandler;
        public float Power { get; private set; }
        public string Name { get; private set; }

        public Animator Animator { get; protected set; }

        public void HandleDeath()
        {
            OnDead?.Invoke(_skin);
            gameObject.SetActive(false);
        }

        public event Action<Skin> OnDead;

        public void Init(Skin skin, string name)
        {
            _skin = skin;
            Power = skin.Price;
            skinHandler.SetSkin(skin);
            Name = name ?? skin.name;
            Animator = skinHandler.Skin.Animator;
        }
    }
}