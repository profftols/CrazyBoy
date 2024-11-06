using System;
using UnityEngine;

namespace Scenes.BossFight.Scripts
{
    public class Boss : Fighters
    {
        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            //EventBus.OnVictoryGame?.Invoke(this);
        }
    }
}