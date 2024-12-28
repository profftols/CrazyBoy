using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BossPower : MonoBehaviour
{
    [SerializeField] private Material[] _materials;

    private Random _random;
    private List<Boss> _bosses;
    private int _minPower = 20;
    private int _maxPower = 100;

    private void Start()
    {
        if (_bosses != null)
        {
            return;
        }

        _bosses = new List<Boss>();
        _random = new Random();

        for (int i = 0; i < _materials.Length; i++)
        {
            var power = _random.Next(_minPower, _maxPower);

            _bosses.Add(new Boss
            {
                Material = _materials[i],
                Power = 0 < i ? _bosses[i - 1].Power += power : power
            });
        }

        int a = 0;
        
        foreach (var VARIABLE in _bosses)
        {
            Debug.Log(VARIABLE.Power + " " + a++);
        }
    }
}