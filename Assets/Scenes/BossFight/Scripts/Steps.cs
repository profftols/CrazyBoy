using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.BossFight.Scripts
{
    public class Steps : MonoBehaviour
    {
        private const float WaitTime = 3.0f;

        [SerializeField] private Button _battle;
        [SerializeField] private Button[] _buttons;

        private Queue<Actions> _actions;

        private void OnEnable()
        {
            _battle.onClick.AddListener(Play);
        }

        private void Start()
        {
            _actions = new Queue<Actions>();
        }

        private void OnDisable()
        {
            _battle.onClick.RemoveListener(Play);
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
            HigeButton();
            StartCoroutine(LaunchActions());
        }

        private IEnumerator LaunchActions()
        {
            var waiter = new WaitForSecondsRealtime(WaitTime);

            while (_actions.Count > 0)
            {
                _actions.Dequeue().Step();
                Debug.Log("Step:"+ " " + _actions.Count);
                yield return waiter;
            }

            Debug.Log("Defeat");
            ShowButton();
        }
        
        private void HigeButton()
        {
            foreach (var uiButton in _buttons)
            {
                uiButton.gameObject.SetActive(false);
            }
        }
        
        private void ShowButton()
        {
            foreach (var uiButton in _buttons)
            {
                uiButton.gameObject.SetActive(true);
            }
        }
    }
}