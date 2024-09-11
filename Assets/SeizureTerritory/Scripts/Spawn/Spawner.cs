using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpeedBonus _speedBonus;
    [SerializeField] private InvulnerabilityBonus _invulnerabilityBonus;
    [SerializeField] private CameraFollow _camera;
    [SerializeField] private Transform _pointBonus;
    [SerializeField] private Character[] _players;
    [SerializeField] private List<Material> _materials;

    private List<Transform> _points;
    private List<Transform> _bonusPoints;
    private BonusPool<Item> _speedPool;
    private BonusPool<Item> _invulnerabilityPool;
    private Random _random;
    private readonly float _timeSpawn = 5f;
    private readonly int _countBonus = 3;

    private void OnEnable()
    {
        EventBus.OnComebackItem += ComebackItem;
    }

    private void OnDisable()
    {
        EventBus.OnComebackItem -= ComebackItem;
    }

    private void Start()
    {
        _invulnerabilityPool = new BonusPool<Item>(_invulnerabilityBonus, _countBonus);
        _speedPool = new BonusPool<Item>(_speedBonus, _countBonus);
        _bonusPoints = new List<Transform>(_pointBonus.childCount);
        _points = new List<Transform>(transform.childCount);
        _random = new Random();

        for (int i = 0; i < transform.childCount; i++)
        {
            _points.Add(transform.GetChild(i));
        }

        for (int i = 0; i < _pointBonus.childCount; i++)
        {
            _bonusPoints.Add(_pointBonus.GetChild(i));
        }

        for (int i = 0; i < _players.Length; i++)
        {
            int count = _random.Next(0, _points.Count - 1);
            var character = Instantiate(_players[i], _points[count].position, Quaternion.identity, null);
            
            if (character is Player)
            {
                Instantiate(_camera).SetTarget(character.transform);
            }

            _points.RemoveAt(count);
        }

        StartCoroutine(AddBonus());
    }

    private IEnumerator AddBonus()
    {
        var wait = new WaitForSeconds(_timeSpawn);

        while (enabled)
        {
            var index = _random.Next(0, _bonusPoints.Count);
            var bonus = _random.Next(0, 4) > 0 ? _speedPool.TakeBonus() : _invulnerabilityPool.TakeBonus();
            bonus.transform.position = _bonusPoints[index].position;
            yield return wait;
        }
    }
    
    private void ComebackItem(Item obj)
    {
        if (obj is SpeedBonus)
        {
            _speedPool.PutBonus(obj);
        }

        if (obj is InvulnerabilityBonus)
        {
            _invulnerabilityPool.PutBonus(obj);
        }
    }
}