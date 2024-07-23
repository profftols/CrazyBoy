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
    private int _minNumberFields = 5;

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

    
}