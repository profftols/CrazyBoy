using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public List<Land> TakeLands(List<Land> lands)
    {
        var temp = new List<Land>();
        var visited = new List<Land>();

        var minPointX = (int)lands.Min(l => l.transform.position.x);
        var maxPointX = (int)lands.Max(l => l.transform.position.x);
        var minPointZ = (int)lands.Min(l => l.transform.position.z);
        var maxPointZ = (int)lands.Max(l => l.transform.position.z);

        minPointX = minPointX > 0 ? minPointX -= 1 : minPointX = 0;
        maxPointX = maxPointX < _sizeX - 1 ? maxPointX += 1 : maxPointX;
        minPointZ = minPointZ > 0 ? minPointZ -= 1 : minPointZ = 0;
        maxPointZ = maxPointZ < _sizeY - 1 ? maxPointZ += 1 : maxPointZ;

        Vector3Int[] directions = new Vector3Int[]
        {
            new(1, 0, 0),
            new(-1, 0, 0),
            new(0, 0, 1),
            new(0, 0, -1)
        };

        for (int x = minPointX; x < maxPointX; x++)
        {
            for (int y = minPointZ; y < maxPointZ; y++)
            {
                if (x == minPointX || x == maxPointX || y == minPointZ || y == maxPointZ)
                {
                    if (TryGetLand(lands, (uint)x, (uint)y))
                    {
                        visited.Add(_lands[x, y]);
                    }
                }
            }
        }

        for (int i = 0; i < visited.Count; i++)
        {
            foreach (var direction in directions)
            {
                Vector3Int position =
                    new Vector3Int((int)visited[i].transform.position.x, 0, (int)visited[i].transform.position.z) +
                    direction;

                if (position.x < maxPointX && position.z < maxPointZ
                    || position.x > minPointX && position.z > minPointZ)
                {
                    if (TryGetLand(lands, (uint)position.x, (uint)position.z) &&
                        TryGetLand(visited, (uint)position.x, (uint)position.z))
                    {
                        visited.Add(_lands[position.x, position.z]);
                    }
                }
            }
        }

        for (int x = minPointX; x < maxPointX; x++)
        {
            for (int y = minPointZ; y < maxPointZ; y++)
            {
                if (lands.Contains(_lands[x, y]) == false && visited.Contains(_lands[x, y]) == false)
                {
                    temp.Add(_lands[x, y]);
                }
            }
        }

        return temp;
    }

    private bool IsCorrectLand(uint x, uint z, List<Land> lands)
    {
        if (x >= _sizeX || z >= _sizeY)
        {
            return false;
        }

        if (lands.Contains(_lands[x, z]) == false)
        {
            return true;
        }

        return false;
    }

    private bool TryGetLand(List<Land> lands, uint x, uint y)
    {
        if (x >= _sizeX || y >= _sizeY)
        {
            return false;
        }

        if (lands.Contains(_lands[x, y]))
        {
            return false;
        }

        return true;
    }

    private void CheckLand(ref List<Land> lands, Land land)
    {
        if (lands.Contains(land) == false)
        {
            lands.Add(land);
        }
    }
}