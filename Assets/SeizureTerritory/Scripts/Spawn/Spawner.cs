using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private Map _map;
    [SerializeField] private Character[] _players;
    [SerializeField] private List<Material> _materials;

    private List<Transform> _points;
    private Random _random;

    private void Start()
    {
        _points = new List<Transform>(_path.childCount);
        _random = new Random();

        for (int i = 0; i < _path.childCount; i++)
        {
            _points.Add(_path.GetChild(i));
        }

        for (int i = 0; i < _players.Length; i++)
        {
            int count = _random.Next(0, _points.Count - 1);
            var character = Instantiate(_players[i], _points[count].position, Quaternion.identity, null);
            
            if (character is BotAI)
            {
                InstallColor(character as BotAI);
            }
            
            character.SetMap(_map);
            _points.RemoveAt(count);
        }
    }

    private void InstallColor(BotAI bot)
    {
        var material = _materials[_random.Next(0, _materials.Count - 1)];
        bot.SetMaterial(material);
        _materials.Remove(material);
    }
}
