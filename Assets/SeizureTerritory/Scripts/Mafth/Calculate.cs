using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Calculate
{
    private static int _multiplierSquare = 4;
    private static float _multiplier = 0.5f;

    public static int GetSquareArea(Vector3 minPoint, Vector3 maxPoint)
    {
        float result = (maxPoint.x - minPoint.x) + (maxPoint.z - minPoint.z) * _multiplierSquare;
        return (int)result;
    }

    public static Vector3 FindMinPoint(Vector3[] lands)
    {
        Vector3 minPoint = lands[0];

        for (int i = 0; i < lands.Length; i++)
        {
            minPoint = new Vector3(Mathf.Min(lands[i].x, minPoint.x),
                Mathf.Min(lands[i].y, minPoint.y),
                Mathf.Min(lands[i].z, minPoint.z));
        }

        return minPoint;
    }

    public static Vector3 FindMaxPoint(Vector3[] lands)
    {
        Vector3 maxPoint = lands[0];

        for (int i = 0; i < lands.Length; i++)
        {
            maxPoint = new Vector3(Mathf.Max(lands[i].x, maxPoint.x),
                Mathf.Max(lands[i].y, maxPoint.y),
                Mathf.Max(lands[i].z, maxPoint.z));
        }

        return maxPoint;
    }

    public static Vector3 FindCenter(Vector3 minPoint, Vector3 maxPoint) => (minPoint - maxPoint) * _multiplier;
}