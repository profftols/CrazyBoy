using UnityEngine;

namespace _ProjectBoy.Scripts.Service
{
    public class MobileInputService : InputService
    {
        public override Vector2 Axis => SimpleInputAxis();
    }
}