using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.BossFight.Scripts
{
    public class Steps : MonoBehaviour
    {
        private const float WaitTime = 3.0f;

        [SerializeField] private Boss _boss;
        [SerializeField] private Button _battle;
        [SerializeField] private Button[] _buttons;

        private Queue<Actions> _actions;
        private Coroutine _fightSteps;

        private void OnEnable()
        {
            _battle.onClick.AddListener(Play);
            EventBus.OnVictoryGame += StopBattle;
            EventBus.OnDefeatGame += StopBattle;
        }

        private void Start()
        {
            _actions = new Queue<Actions>();
        }

        private void OnDisable()
        {
            _battle.onClick.RemoveListener(Play);
            EventBus.OnVictoryGame -= StopBattle;
            EventBus.OnDefeatGame -= StopBattle;
        }

        private void StopBattle(float obj)
        {
            if (_fightSteps != null)
            {
                StopCoroutine(_fightSteps);
            }
        }

        public void Add(Actions action)
        {
            _actions.Enqueue(action);
            
            if (_actions.Count == 3)
            {
                _boss.ActEnemy();
                return;
            }

            if (_actions.Count == 6)
            {
                Play();
            }
        }

        private void Play()
        {
            HigeButton();
            _fightSteps = StartCoroutine(LaunchActions());
        }

        private IEnumerator LaunchActions()
        {
            var waiter = new WaitForSecondsRealtime(WaitTime);

            while (_actions.Count > 0)
            {
                _actions.Dequeue().Step();
                yield return waiter;
            }
            
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