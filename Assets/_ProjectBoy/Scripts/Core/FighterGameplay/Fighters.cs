using System;
using _ProjectBoy.Scripts.Core.CharacterSc;
using _ProjectBoy.Scripts.Core.FighterGameplay.Punch;

namespace _ProjectBoy.Scripts.Core.FighterGameplay
{
    public class Fighters : IDamageable
    {
        private const int MaxActions = 3;

        private readonly IDeathHandler _deathHandler;
        private int _actions;

        protected Fighters(Character character)
        {
            Character = character;
            _deathHandler = character;
        }

        public Character Character { get; }
        public Health Health { get; protected set; }

        protected Defence defence { get; set; }
        protected Attack attack { get; set; }

        public void TakeDamage(float damage)
        {
            _actions = 0;
            var value = Health.TakeDamage(damage, Character.Animator);

            if (value > 0) OnHpChange?.Invoke(value);
        }

        public void Defence()
        {
            _actions = 0;
            Health.Defence();
        }

        public event Action<Actions> OnActionSteps;
        public event Action<float> OnHpChange;

        public void DefenceAction()
        {
            if (MaxActions == _actions) return;

            _actions++;
            OnActionSteps?.Invoke(defence);
        }

        public void AttackAction()
        {
            if (MaxActions == _actions) return;

            _actions++;
            OnActionSteps?.Invoke(attack);
        }

        protected void Dead()
        {
            Health.OnDead -= Dead;
            _deathHandler.HandleDeath();
        }
    }
}