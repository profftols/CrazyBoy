using System.Collections.Generic;
using UnityEngine;

public class Colouring
{
    public Renderer TextureMaterial { get; }
    
    private Map _map;
    private float _radius = 3f;
    private float _distance = 1f;

    public Colouring(Renderer textureMaterial)
    {
        TextureMaterial = textureMaterial;
    }

    public bool IsChangeLandMaterial(Land land, List<Land> lands)
    {
        if (lands?.Contains(land) == false)
        {
            land.SetMaterial(TextureMaterial.material);
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
            if (land.IsNotValidMaterial(TextureMaterial.material))
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
            variaLand.SetMaterial(TextureMaterial.material);
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
                land.SetMaterial(TextureMaterial.material);
                lands.Add(land);
            }
        }

        return lands;
    }
}