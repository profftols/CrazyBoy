using System;
using UnityEngine;
using UnityEngine.UI;

namespace _ProjectBoy.Scripts.Cheats
{
    public class GiveMoney : MonoBehaviour
    {
        private readonly float _score = 249;
        [field: SerializeField] public Button getMoney { get; private set; }

        private void OnEnable()
        {
            getMoney.onClick.AddListener(() => { OnMoney?.Invoke(_score); });
        }

        private void OnDisable()
        {
            getMoney.onClick.RemoveListener(() => { OnMoney?.Invoke(_score); });
        }

        public static event Action<float> OnMoney;
    }
}