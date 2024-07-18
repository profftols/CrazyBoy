using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class ColoringPlayer
{
    private List<Land> _lands;
    private List<Land> _bufferLands;
    private Renderer _renderer;
    private Map _map;

    private float _radius = 3f;
    private float _distance = 1f;

    public ColoringPlayer(Renderer renderer, Map map)
    {
        _lands = new List<Land>();
        _bufferLands = new List<Land>();
        _renderer = renderer;
        _map = map;
    }

    public void AssignLend(Transform transform)
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        RaycastHit[] hits = Physics.SphereCastAll(origin, _radius, direction, _distance);

        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.TryGetComponent(out Land land))
            {
                land.SetMaterial(_renderer.material);
                _lands.Add(land);
            }
        }
    }

    public bool ChangeLandMaterial(Land land)
    {
        if (_lands.Contains(land) == false)
        {
            land.SetMaterial(_renderer.material);
        }
        else
        {
            return false;
        }

        return true;
    }

    public void AddBufferLand(Land land)
    {
        _bufferLands.Add(land);
    }

    public bool IsConquerLands() => _bufferLands != null && _bufferLands.Count >= 1;

    public void ConquerLand()
    {
        PaintInside();
        
        _lands.AddRange(_bufferLands);
        _bufferLands.Clear();
    }

    private bool IsColorCorrect() // Метод который проверяет захваченные цвета все совпадают если нет то смерть
    {
        foreach (var land in _bufferLands)
        {
            if (land.IsValidColor(_renderer.material.color))
            {
                return false;
            }
        }

        return true;
    }

    private void PaintInside()
    {
        
        Vector3[] positions = new Vector3[_bufferLands.Count];
        ///*
        List<Land> lands = new();


        for (int i = 0; i < _bufferLands.Count; i++)
        {
            positions[i] = _bufferLands[i].transform.position;
        }

        for (int start = 0; start < positions.Length; start++)
        {
            for (int end = positions.Length-1; end > 0; end--)
            {
                if ((int)positions[start].x == (int)positions[end].x)
                {
                    lands.AddRange(_map.TakeLands(positions[start], positions[end], true));
                }

                if ((int)positions[start].z == (int)positions[end].z)
                {
                    lands.AddRange(_map.TakeLands(positions[start], positions[end], false));
                }
            }
        }

        foreach (var variaLand in lands)
        {
            variaLand.SetMaterial(_renderer.material);
        }
    }
}