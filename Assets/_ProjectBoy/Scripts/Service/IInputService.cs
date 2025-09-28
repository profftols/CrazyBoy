using _ProjectBoy.Scripts.Infostructure.Services;
using UnityEngine;

namespace _ProjectBoy.Scripts.Service
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
    }
}