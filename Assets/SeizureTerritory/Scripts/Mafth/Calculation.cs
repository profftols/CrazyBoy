using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Calculation
{
    public static Vector3 GetInsideLands(List<Land> lands)
    {
        Land startLand = lands.OrderBy(land => land.transform.position.x).ThenBy(land => land.transform.position.z)
            .First();
        return startLand.transform.position;
    }
    
    public static bool IsValidLands(List<Land> lands)
    {
        if (lands.Count < 2) return false;

        var xMin = (int)lands[0].transform.position.x;
        var zMin = (int)lands[0].transform.position.z;
        var xMax = (int)lands[lands.Count - 1].transform.position.x;
        var zMax = (int)lands[lands.Count - 1].transform.position.z;

        return Math.Abs(xMax - xMin) > 1 || Math.Abs(zMax - zMin) > 1;
    }

    public static Vector3[] GetPerimeter(List<Land> lands)
    {
        var minPointX = lands.Min(l => l.transform.position.x);
        var maxPointX = lands.Max(l => l.transform.position.x);
        var minPointZ = lands.Min(l => l.transform.position.z);
        var maxPointZ = lands.Max(l => l.transform.position.z);
        
        var squareArea = new Vector3[4];
        squareArea[0] = new Vector3(minPointX, 0, minPointZ);
        squareArea[1] = new Vector3(maxPointX, 0, maxPointZ);
        squareArea[2] = new Vector3(maxPointX, 0, minPointZ);
        squareArea[3] = new Vector3(minPointX, 0, maxPointZ);
        
        return squareArea;
    }
}