using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colouring
{
    private List<Land> _lands;
    private List<Land> _buffer;
    private Map _map;
    private Renderer _textureMaterial;
    private float _radius = 3f;
    private float _distance = 1f;
    private int _minNumberFields = 5;

    public Colouring(Renderer textureMaterial)
    {
        _textureMaterial = textureMaterial;
    }

    public bool IsConquerLands() => _buffer != null && _buffer.Count >= 1;
    
    public void AddBuffer(Land land) => _buffer.Add(land);
    
    public bool ChangeLandMaterial(Land land)
    {
        if (_lands?.Contains(land) == false)
        {
            land.SetMaterial(_textureMaterial.material);
        }
        else
        {
            return false;
        }

        return true;
    }

    private bool IsColorNotCorrect()
    {
        foreach (var land in _buffer)
        {
            if (land.IsNotValidMaterial(_textureMaterial.material))
            {
                return true;
            }
        }

        return false;
    }

    public bool PaintInside()
    {
        if (IsColorNotCorrect())
        {
            return false;
        }

        if (_buffer.Count > _minNumberFields)
        {
            List<Land> lands = new List<Land>();
            Vector3[] positions = new Vector3[_buffer.Count];

            for (int i = 0; i < _buffer.Count; i++)
            {
                positions[i] = _buffer[i].transform.position;
            }

            var minPoint = Calculate.FindMinPoint(positions);
            var maxPoint = Calculate.FindMaxPoint(positions);
            var centerPoint = Calculate.FindCenter(minPoint, maxPoint);

            _buffer.AddRange(_map.TakeLands(ref lands, _textureMaterial.material, centerPoint));

            foreach (var variaLand in lands)
            {
                variaLand.SetMaterial(_textureMaterial.material);
            }

            positions = null;
            lands = null;
        }

        _lands.AddRange(_buffer);
        _buffer.Clear();
        return true;
    }

    public void Clean()
    {
        _lands.AddRange(_buffer);

        foreach (var land in _lands)
        {
            _map.SetDefaultMaterial(land);
        }

        _lands = null;
        _buffer = null;
    }
    
    public void Spawn(Transform transform)
    {
        _lands = new List<Land>();
        _buffer = new List<Land>();

        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        RaycastHit[] hits = Physics.SphereCastAll(origin, _radius, direction, _distance);

        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.TryGetComponent(out Land land))
            {
                land.SetMaterial(_textureMaterial.material);
                _lands.Add(land);
            }
        }
    }

    public void SetMap(Map map)
    {
        _map = map;
    }
}
