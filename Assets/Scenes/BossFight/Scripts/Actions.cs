using UnityEngine;
using UnityEngine.UI;

namespace Scenes.BossFight.Scripts
{
    public abstract class Actions : MonoBehaviour
    {
        [SerializeField] protected Button button;
        [SerializeField] protected Steps steps;
        
        public float Power { get; private set; } = 10.0f;

        private void OnEnable()
        {
            button.onClick.AddListener(Action);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(Action);
        }

        protected abstract void Action();
    }
}
