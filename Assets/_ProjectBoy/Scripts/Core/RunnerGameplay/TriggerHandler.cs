using System;
using UnityEngine;

namespace _ProjectBoy.Scripts.Core.RunnerGameplay
{
    public class TriggerHandler : MonoBehaviour
    {
        private void OnTriggerEnter(Collider item)
        {
            OnTrigger?.Invoke(item);
        }

        public event Action<Collider> OnTrigger;
    }
}