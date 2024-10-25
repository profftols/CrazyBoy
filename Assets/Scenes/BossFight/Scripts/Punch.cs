using UnityEngine;

namespace Scenes.BossFight.Scripts
{
    public class Punch : Actions
    {
        protected override void Action()
        {
            steps.Add(this);
        }
    }
}
