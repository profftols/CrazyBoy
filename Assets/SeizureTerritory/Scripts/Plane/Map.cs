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

    public List<Land> TakeLands(List<Land> lands)
    {
        var temp = new List<Land>();
        var visited = new List<Vector3>();

        var minPointX = (int)lands.Min(l => l.transform.position.x);
        var maxPointX = (int)lands.Max(l => l.transform.position.x);
        var minPointZ = (int)lands.Min(l => l.transform.position.z);
        var maxPointZ = (int)lands.Max(l => l.transform.position.z);

        minPointX -= 1 > 0 ? minPointX -= 1 : minPointX = 0;
        maxPointX += 1 < _sizeX ? maxPointX += 1 : maxPointX = _sizeX;
        minPointZ -= 1 > 0 ? minPointZ -= 1 : minPointZ = 0;
        maxPointZ += 1 < _sizeY ? maxPointZ += 1 : maxPointZ = _sizeY;

        Vector3Int[] directions = new Vector3Int[]
        {
            new(1, 0, 0),
            new(-1, 0, 0),
            new(0, 0, 1),
            new(0, 0, -1)
        };

        // Ниже код предоставила нейросеть говорит так короче. Надо протестить.
        for (int x = minPointX; x <= maxPointX; x++)
        {
            for (int y = minPointZ; y <= maxPointZ; y++)
            {
                if (x == minPointX || x == maxPointX || y == minPointZ || y == maxPointZ)
                {
                    foreach (var direction in directions)
                    {
                        Vector3Int newPosition = new Vector3Int(x, 0, y) + direction;
                        
                        if (!lands.Contains(_lands[x, y]))
                        {
                            visited.Add(_lands[x, y].transform.position);
                        }
                    }
                }
            }
        }

        /*for (int x = minPointX; x < maxPointX; x++)
        {
            foreach (var direction in directions)
            {
                Vector3Int newPosition = new Vector3Int(x, 0, minPointZ) + direction;

                if (lands.Contains(_lands[x, minPointZ]) == false)
                {
                    visited.Add(_lands[x, minPointZ].transform.position);
                }
            }
        }

        for (int y = minPointZ; y < maxPointZ; y++)
        {
            foreach (var direction in directions)
            {
                Vector3Int newPosition = new Vector3Int(minPointX, 0, y) + direction;

                if (lands.Contains(_lands[minPointX, y]) == false)
                {
                    visited.Add(_lands[minPointX, y].transform.position);
                }
            }
        }

        for (int x = minPointX; x < maxPointX; x++)
        {
            foreach (var direction in directions)
            {
                Vector3Int newPosition = new Vector3Int(x, 0, maxPointZ) + direction;

                if (lands.Contains(_lands[x, maxPointZ]) == false)
                {
                    visited.Add(_lands[x, maxPointZ].transform.position);
                }
            }
        }

        for (int y = minPointZ; y < maxPointZ; y++)
        {
            foreach (var direction in directions)
            {
                Vector3Int newPosition = new Vector3Int(maxPointX, 0, y) + direction;

                if (lands.Contains(_lands[maxPointX, y]) == false)
                {
                    visited.Add(_lands[maxPointX, y].transform.position);
                }
            }
        }*/

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