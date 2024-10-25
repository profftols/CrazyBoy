using UnityEngine;

namespace Scenes.BossFight.Scripts
{
    public class Defence : Actions
    {
        protected override void Action()
        {
            steps.Add(this);
        }
    }
}
