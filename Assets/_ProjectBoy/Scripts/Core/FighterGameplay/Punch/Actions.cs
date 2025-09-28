using UnityEngine;

namespace _ProjectBoy.Scripts.Core.FighterGameplay.Punch
{
    public abstract class Actions
    {
        protected readonly Animator Animator;
        protected readonly IDamageable Fighter;
        protected readonly float Power;

        protected Actions(Animator animator, IDamageable fighter, float power)
        {
            Animator = animator;
            Fighter = fighter;
            Power = power;
        }

        public abstract void Step();
    }
}