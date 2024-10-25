using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.BossFight.Scripts
{
    public class Steps : MonoBehaviour
    {
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
            _actions.Enqueue(action);
        }

        private void Play()
        {
            StartCoroutine(LaunchActions());
        }

        private IEnumerator LaunchActions()
        {
            var waiter = new WaitForSecondsRealtime(3);

            while (_actions.Count > 0)
            {
                Debug.Log("Step" + " " + _actions.Count);
                _actions.Dequeue();
                yield return waiter;
            }
            
            Debug.Log("Defeat");
        }
    }
}