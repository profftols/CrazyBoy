using _ProjectBoy.Scripts.Audio;
using _ProjectBoy.Scripts.Service;
using UnityEngine;

namespace _ProjectBoy.Scripts.Core.FighterGameplay.Punch
{
    public class Attack : Actions
    {
        public Attack(Animator animator, IDamageable fighter, float power) : base(animator, fighter, power)
        {
        }

        public override void Step()
        {
            AnimationService.SetAttackAnim(Animator);
            MasterSoundSettings.Instance.soundEffects.PlayPunch();
            Fighter.TakeDamage(Power);
        }
    }
}