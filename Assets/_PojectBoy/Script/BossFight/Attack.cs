namespace Scenes.BossFight.Scripts
{
    public class Attack : Actions
    {
        public override void Step()
        {
            Character.TakeDamage(Power);
        }

        public Attack(Fighters character, float power) : base(character, power)
        {
        }
    }
}
