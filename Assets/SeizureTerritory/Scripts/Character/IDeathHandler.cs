using System;
using UnityEngine;

public interface IDeathHandler
{
    Renderer Render { get; }
    event Action<IDeathHandler, Land> OnLand;
    void HandleDeath();
    Transform GetTransform();
}