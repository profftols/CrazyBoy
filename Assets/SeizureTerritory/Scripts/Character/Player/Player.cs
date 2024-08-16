using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Unity.VisualScripting.Dependencies.Sqlite;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Player : Character
{
    private void OnDestroy()
    {
    }

    private void LateUpdate()
    {
        Debug.Log(Random.insideUnitCircle.normalized);
    }
}