using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.BossFight.Scripts
{
    public class Steps : MonoBehaviour
    {
        private const float WaitTime = 3.0f;

        [SerializeField] private Button _button;

        private Queue<Actions> _actions;

        private void OnEnable()
        {
            _button.onClick.AddListener(Play);
        }

        private void Start()
        {
            _actions = new Queue<Actions>();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Play);
        }

        public void Add(Actions action)
        {
            if (_actions.Count >= 3)
            {
                Play();
                return;
            }
            
            _actions.Enqueue(action);
        }

        private void Play()
        {
            StartCoroutine(LaunchActions());
        }

        private IEnumerator LaunchActions()
        {
            var waiter = new WaitForSecondsRealtime(WaitTime);

            while (_actions.Count > 0)
            {
                _actions.Dequeue().Step();
                yield return waiter;
            }

            Debug.Log("Defeat");
        }
    }
}