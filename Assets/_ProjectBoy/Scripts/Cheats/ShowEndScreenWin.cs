using System;
using UnityEngine;
using UnityEngine.UI;

namespace _ProjectBoy.Scripts.Cheats
{
    public class ShowEndScreenWin : MonoBehaviour
    {
        [SerializeField] private Button KillEnemy;

        private void OnEnable()
        {
            KillEnemy.onClick.AddListener(() => OnWin?.Invoke(true));
        }

        private void OnDisable()
        {
            KillEnemy.onClick.RemoveListener(() => OnWin?.Invoke(true));
        }

        public static event Action<bool> OnWin;
    }
}