using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private Land _land;
    [SerializeField] private int _sizeX;
    [SerializeField] private int _sizeY;

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
    }

    public bool TakeLand(Vector3 minPosition, Vector3 maxPosition, Renderer renderer, out Land land)
    {
        //принимает координаты которые не закрашенные из буфера
        //Написать циклы которые бы проходили в диапазоне от макс до миним
        //Проверять закрашен в наш цвет или нет

        for (int x = (int)minPosition.x; x < (int)maxPosition.x; x++)
        {
            for (int y = (int)minPosition.z; y < (int)maxPosition.z; y++)
            {
                if (_lands[x, y].TextureRender.material.color != renderer.material.color)
                {
                    land = _lands[x, y];
                    return true;
                }
            }
        }

        land = null;
        return false;
    }

    public List<Land> TakeLands(Vector3 start, Vector3 end, bool isStepY)
    {
        List<Land> lands = new();

        if (isStepY)
        {
            if ((int)start.z < (int)end.z)
            {
                for (int y = (int)start.z; y < (int)end.z; y++)
                {
                    lands.Add(_lands[(int)start.x, y]);
                }
            }
            else if ((int)start.z > (int)end.z)
            {
                for (int y = (int)start.z; y > (int)end.z; y--)
                {
                    lands.Add(_lands[(int)start.x, y]);
                }
            }

            return lands;
        }

        if ((int)start.x < (int)end.x)
        {
            for (int x = (int)start.x; x < (int)end.x; x++)
            {
                lands.Add(_lands[x, (int)start.z]);
            }
        }
        else if ((int)start.x > (int)end.x)
        {
            for (int x = (int)start.x; x > (int)end.x; x--)
            {
                lands.Add(_lands[x, (int)start.z]);
            }
        }

        return lands;
    }
}