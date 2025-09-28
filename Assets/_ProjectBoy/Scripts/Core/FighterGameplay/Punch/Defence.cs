using UnityEngine;

namespace _ProjectBoy.Scripts.Core.FighterGameplay.Punch
{
    public class Defence : Actions
    {
        public Defence(Animator animator, IDamageable fighter, float power) : base(animator, fighter, power)
        {
        }

        public override void Step()
        {
            //AnimationService.SetBlockAnim(Animator);
            Fighter.Defence();
        }
    }
}