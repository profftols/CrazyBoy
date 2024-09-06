using System.Collections.Generic;
using UnityEngine;

public class Colouring
{
    private readonly Renderer _render;
    private float _radius = 3f;
    private float _distance = 1f;

    public Colouring(Renderer render)
    {
        _render = render;
    }

    public void ChangeMaterial(Land land)
    {
        land.SetMaterial(_render.material);
        land.ActivationOutline();
    }

    public bool IsColorNotCorrect(List<Land> lands)
    {
        foreach (var land in lands)
        {
            if (land.IsNotDefaultMaterial(_render.material))
            {
                return true;
            }
        }

        return false;
    }

    public void PaintInside(List<Land> lands)
    {
        foreach (var land in lands)
        {
            land.SetMaterial(_render.material);
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
                land.SetMaterial(_render.material);
                lands.Add(land);
            }
        }

        return lands;
    }

    public bool IsEnemyColor(Land land)
    {
        return land.IsNotDefaultMaterial(_render.material);
    }
}