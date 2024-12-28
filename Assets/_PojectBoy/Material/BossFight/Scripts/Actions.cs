namespace Scenes.BossFight.Scripts
{
    public abstract class Actions
    {
        protected readonly Fighters Character;
        protected readonly float Power;

        protected Actions(Fighters character, float power)
        {
            Character = character;
            Power = power;
        }

        public abstract void Step();
    }
}
