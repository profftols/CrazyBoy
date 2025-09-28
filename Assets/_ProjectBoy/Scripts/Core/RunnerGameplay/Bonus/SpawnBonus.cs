using System.Collections;
using System.Collections.Generic;
using _ProjectBoy.Scripts.Core.RunnerGameplay.Bonus.BonusObject;
using UnityEngine;
using Random = System.Random;

namespace _ProjectBoy.Scripts.Core.RunnerGameplay.Bonus
{
    public class SpawnBonus : MonoBehaviour
    {
        private readonly int _countBonus = 3;
        private readonly float _timeSpawn = 5f;

        private List<Transform> _bonusPoints;
        [SerializeField] private Transform _pointBonus;
        private Random _random;
        [SerializeField] private SpeedBonus _speedBonus;
        private BonusPool<Item> _speedPool;

        private void Start()
        {
            _speedPool = new BonusPool<Item>(_speedBonus, _countBonus);
            _bonusPoints = new List<Transform>(_pointBonus.childCount);
            _random = new Random();

            for (var i = 0; i < _pointBonus.childCount; i++) _bonusPoints.Add(_pointBonus.GetChild(i));

            StartCoroutine(AddBonus());
        }

        private IEnumerator AddBonus()
        {
            var wait = new WaitForSeconds(_timeSpawn);

            while (enabled)
            {
                var index = _random.Next(0, _bonusPoints.Count);
                var bonus = _speedPool.TakeBonus();
                bonus.transform.position = _bonusPoints[index].position;
                bonus.OnGiveContainer += ComebackItem;
                yield return wait;
            }
        }

        private void ComebackItem(Item obj)
        {
            if (obj is SpeedBonus) _speedPool.PutBonus(obj);

            obj.OnGiveContainer -= ComebackItem;
        }
    }
}