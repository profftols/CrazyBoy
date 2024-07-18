using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using Object = UnityEngine.Object;

[RequireComponent(typeof(MeshRenderer))]
public class Character : MonoBehaviour
{
    private Map _map;
    private ColoringPlayer _coloring;

    private void Start()
    {
        _coloring = new ColoringPlayer(GetComponent<Renderer>(), _map);
        _coloring.AssignLend(transform);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Land land))
        {
            if (ReferenceEquals(_coloring, null) == false)
            {
                if (_coloring.ChangeLandMaterial(land))
                {
                    _coloring.AddBufferLand(land);
                }
                else if (_coloring.IsConquerLands())
                {
                    _coloring.ConquerLand();
                }
            }
        }
    }

    public void SetMap(Map map)
    {
        _map = map;
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}