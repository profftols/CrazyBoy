using _ProjectBoy.Scripts.Audio;
using _ProjectBoy.Scripts.Core.CharacterSc;

namespace _ProjectBoy.Scripts.Core.RunnerGameplay.Bonus.BonusObject
{
    public class SpeedBonus : Item
    {
        private readonly float _modifierSpeed = 1.5f;
        protected override float Timer => 4.5f;
        protected override float TimeBonusEffect => 2.5f;

        public override void OnPickUp(Runner character)
        {
            if (character.Character is Player) MasterSoundSettings.Instance.soundEffects.PlayGetBonus();

            character.Mover.SetSpeedModifier(TimeBonusEffect, _modifierSpeed);
            gameObject.SetActive(false);
        }
    }
}