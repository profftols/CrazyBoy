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

    public static Queue<Vector3> GetCoordinates(List<Land> buffer, List<Land> lands)
    {
        Queue<Vector3> positions = new Queue<Vector3>();


        return positions;
    }
}