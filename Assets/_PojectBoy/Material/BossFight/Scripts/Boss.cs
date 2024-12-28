using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scenes.BossFight.Scripts
{
    public class Boss : Fighters
    {
        private readonly int _minRandom = 0;
        private readonly int _maxRandom = 10;
        private int _probabilityDefence = 3;
        private int _stepsCount = 3;

        private void OnDisable()
        {
            EventBus.OnVictoryGame?.Invoke(0);
        }

        public void ActEnemy()
        {
            for (int i = 0; i < _stepsCount; i++)
            {
                if (Random.Range(_minRandom, _maxRandom) <= _probabilityDefence)
                {
                    Defences();
                }
                else
                {
                    Attacks();
                }
            }
        }
    }
}