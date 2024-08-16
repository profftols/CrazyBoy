using System.Collections.Generic;
using UnityEngine;

public class Colouring
{
    private Map _map;
    private Renderer _textureMaterial;
    private float _radius = 3f;
    private float _distance = 1f;

    public Colouring(Renderer textureMaterial)
    {
        _textureMaterial = textureMaterial;
    }

    public bool IsConquerLands(List<Land> buffer) => buffer != null && buffer.Count >= 1;

    public void AddBuffer(Land land, List<Land> buffer) => buffer.Add(land);

    public bool IsChangeLandMaterial(Land land, List<Land> lands)
    {
        if (lands?.Contains(land) == false)
        {
            land.SetMaterial(_textureMaterial.material);
        }
        else
        {
            return false;
        }

        return true;
    }

    public bool IsColorNotCorrect(List<Land> buffers)
    {
        foreach (var land in buffers)
        {
            if (land.IsNotValidMaterial(_textureMaterial.material))
            {
                return true;
            }
        }

        return false;
    }

    public void PaintInside(List<Land> lands)
    {
        foreach (var variaLand in lands)
        {
            variaLand.SetMaterial(_textureMaterial.material);
        }
    }

    public List<Land> Spawn(Transform transform)
    {
        var lands = new List<Land>();

        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        RaycastHit[] hits = Physics.SphereCastAll(origin, _radius, direction, _distance);

        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.TryGetComponent(out Land land))
            {
                land.SetMaterial(_textureMaterial.material);
                lands.Add(land);
            }
        }

        return lands;
    }

    public void SetMap(Map map)
    {
        _map = map;
    }
}