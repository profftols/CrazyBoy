using System.Net;

namespace Scenes.BossFight.Scripts
{
    public class Defence : Actions
    {
        public override void Step()
        {
            Character.TakeDefence();
        }
        
        public Defence(Fighters character, float power) : base(character, power)
        {
        }
    }
}
