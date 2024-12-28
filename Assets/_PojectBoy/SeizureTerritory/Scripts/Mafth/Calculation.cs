using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Calculation
{
    public static bool IsValidLands(List<Land> lands)
    {
        if (lands.Count < 2) return false;

        var xMin = (int)lands[0].transform.position.x;
        var zMin = (int)lands[0].transform.position.z;
        var xMax = (int)lands[^1].transform.position.x;
        var zMax = (int)lands[^1].transform.position.z;

        return Math.Abs(xMax - xMin) > 1 || Math.Abs(zMax - zMin) > 1;
    }
}