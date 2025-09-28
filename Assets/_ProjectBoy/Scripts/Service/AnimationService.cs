using UnityEngine;

namespace _ProjectBoy.Scripts.Service
{
    public static class AnimationService
    {
        private static readonly int MovementSpeed = Animator.StringToHash("movementSpeed");
        private static readonly int GetHit = Animator.StringToHash("GetHit");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Block = Animator.StringToHash("Block");
        private static readonly int Defeat = Animator.StringToHash("Defeat");
        private static readonly int Victory = Animator.StringToHash("Victory");

        public static void SetHitAnim(Animator animator)
        {
            animator.SetTrigger(GetHit);
        }

        public static void SetAttackAnim(Animator animator)
        {
            animator.SetTrigger(Attack);
        }

        public static void SetBlockAnim(Animator animator)
        {
            animator.SetTrigger(Block);
        }

        public static void SetDefeatAnim(Animator animator)
        {
            animator.SetTrigger(Defeat);
        }

        public static void SetVictoryAnim(Animator animator)
        {
            animator.SetTrigger(Victory);
        }

        public static void SetSpeedAnim(Animator animator, float speed)
        {
            animator.SetFloat(MovementSpeed, speed);
        }
    }
}