using System;
using _ProjectBoy.Scripts.Audio;
using _ProjectBoy.Scripts.Service;
using UnityEngine;

namespace _ProjectBoy.Scripts.Core.FighterGameplay
{
    public class Health
    {
        private const float DefenceCoefficient = 0.8f;
        private float _defenceCoefficient = DefenceCoefficient;
        private bool _isDefence;

        public Health(float currentHp)
        {
            CurrentHp = currentHp;
            MaxHp = currentHp;
        }

        public float MaxHp { get; private set; }

        private float CurrentHp { get; set; }

        public event Action OnDead;

        public float TakeDamage(float damage, Animator animator)
        {
            if (_isDefence)
            {
                CurrentHp -= damage * _defenceCoefficient;
                _isDefence = false;
                _defenceCoefficient = DefenceCoefficient;
                AnimationService.SetBlockAnim(animator);
                MasterSoundSettings.Instance.soundEffects.PlayDefence();
            }
            else
            {
                CurrentHp -= damage;
                AnimationService.SetHitAnim(animator);
                MasterSoundSettings.Instance.soundEffects.PlayGetHit();
            }

            if (!(CurrentHp <= 0))
                return CurrentHp;

            AnimationService.SetDefeatAnim(animator);
            OnDead?.Invoke();
            return 0;
        }

        public void Defence()
        {
            if (_isDefence) _defenceCoefficient *= _defenceCoefficient;

            _isDefence = true;
        }
    }
}