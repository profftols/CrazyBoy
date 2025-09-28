using System.Collections.Generic;
using UnityEngine;

namespace _ProjectBoy.Scripts.Core.RunnerGameplay.Bonus.BonusObject
{
    public class BonusPool<T> where T : MonoBehaviour
    {
        private readonly Queue<T> _pool;
        private readonly T _prefab;

        public BonusPool(T prefab, int count)
        {
            _prefab = prefab;
            _pool = new Queue<T>(count);

            for (var i = 0; i < count; i++)
            {
                var obj = Object.Instantiate(_prefab);
                obj.gameObject.SetActive(false);
                _pool.Enqueue(obj);
            }
        }

        public T TakeBonus()
        {
            if (_pool.TryDequeue(out var obj))
            {
                obj.gameObject.SetActive(true);
                return obj;
            }

            return Object.Instantiate(_prefab);
            ;
        }

        public void PutBonus(T obj)
        {
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }
    }
}