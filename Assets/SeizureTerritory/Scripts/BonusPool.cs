using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPool<T> where T : MonoBehaviour
{
    private Queue<T> _pool;
    private T _prefab;
    
    public BonusPool(T prefab, int count)
    {
        _prefab = prefab;
        _pool = new Queue<T>(count);
        
        for (int i = 0; i < count; i++)
        {
            var obj = GameObject.Instantiate(_prefab);
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }
    }

    public T TakeBonus()
    {
        if (_pool.TryDequeue(out T obj))
        {
            obj.gameObject.SetActive(true);
            return obj;
        }

        return Object.Instantiate(_prefab);;
    }
    
    public void PutBonus(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }
}
