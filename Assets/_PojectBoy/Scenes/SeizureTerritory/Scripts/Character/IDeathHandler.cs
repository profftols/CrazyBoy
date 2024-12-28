using System;
using UnityEngine;

public interface IDeathHandler
{
    Renderer Render { get; }
    event Action<IDeathHandler, Land> OnLand;
    event Action<IDeathHandler> OnSpawn;
    void HandleDeath();
    Transform GetTransform();
}