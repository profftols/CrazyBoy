using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace _ProjectBoy.Scripts.Core.RunnerGameplay.Bonus.BonusObject
{
    public abstract class Item : MonoBehaviour
    {
        [SerializeField] private float _floatDuration = 1f;
        [SerializeField] private float _floatStrength = 0.5f;

        private Tween _floatTween;
        private Vector3 _initialPosition;
        protected virtual float Timer => 10f;
        protected virtual float TimeBonusEffect => 3f;

        private void OnEnable()
        {
            StartCoroutine(StatTimer());
            Show();
        }

        private void OnDisable()
        {
            StopCoroutine(StatTimer());
            Hide();
        }

        public event Action<Item> OnGiveContainer;

        public abstract void OnPickUp(Runner character);

        private void Show()
        {
            _floatTween = transform.DOLocalMoveY(_initialPosition.y + _floatStrength, _floatDuration)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void Hide()
        {
            OnGiveContainer?.Invoke(this);
            _floatTween?.Kill();
        }

        private IEnumerator StatTimer()
        {
            var wait = new WaitForSeconds(Timer);
            yield return wait;
            gameObject.SetActive(false);
        }
    }
}