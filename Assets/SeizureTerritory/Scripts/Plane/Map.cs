using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private Land _land;
    [SerializeField] private int _sizeX;
    [SerializeField] private int _sizeY;

    private Renderer _defaultLand;
    private Land[,] _lands;

    private void Awake()
    {
        _lands = new Land[_sizeX, _sizeY];

        for (int x = 0; x < _sizeX; x++)
        {
            for (int y = 0; y < _sizeY; y++)
            {
                Vector3 spawnPosition = new Vector3(x, 0, y);
                var land = Instantiate(_land, spawnPosition, Quaternion.identity);
                _lands[x, y] = land;
                land.transform.SetParent(gameObject.transform);
            }
        }

        _defaultLand = _lands[0, 0].GetComponent<Renderer>();
    }

    public void SetDefaultMaterial(Land land)
    {
        land.SetMaterial(_defaultLand.material);
    }

    public Land TakeLand(int x, int z)
    {
        if (x >= _sizeX || z >= _sizeY)
        {
            return null;
        }

        return _lands[x, z];
    }

    public List<Land> TakeLands(ref List<Land> lands, Material material, Vector3 start)
    {
        lands.Add(_lands[(int)start.x, (int)start.z]);

        Vector3Int[] directions = new Vector3Int[]
        {
            new(1, 0, 0),
            new(-1, 0, 0),
            new(0, 0, 1),
            new(0, 0, -1)
        };

        for (int i = 0; i < lands.Count; i++)
        {
            Vector3Int position = new Vector3Int((int)lands[i].transform.position.x, (int)lands[i].transform.position.y,
                (int)lands[i].transform.position.z);

            foreach (var direction in directions)
            {
                Vector3Int newPosition = position + direction;

                if (TryGetLand(ref lands, material, (uint)newPosition.x, (uint)newPosition.z, out Land newLand))
                {
                    CheckLand(ref lands, newLand);
                }
            }
        }

        return lands;
    }

    private bool TryGetLand(ref List<Land> lands, Material material, uint x, uint z, out Land land)
    {
        if (x >= _sizeX || z >= _sizeY)
        {
            land = null;
            return false;
        }

        if (lands.Contains(_lands[x, z]) == false && material.color != _lands[x, z].Texture.material.color)
        {
            land = _lands[x, z];
            return true;
        }

        land = null;
        return false;
    }

    private void CheckLand(ref List<Land> lands, Land land)
    {
        if (lands.Contains(land) == false)
        {
            lands.Add(land);
        }
    }
}