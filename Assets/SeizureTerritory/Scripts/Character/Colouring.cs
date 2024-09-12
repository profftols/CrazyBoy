using System.Collections.Generic;
using UnityEngine;

public class Colouring
{
    private Character _character;
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

    public bool IsEnemyColor(Land land)
    {
        return land.IsNotDefaultMaterial(_render.material);
    }
}