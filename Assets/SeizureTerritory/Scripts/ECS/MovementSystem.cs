using Leopotam.Ecs;
using Unity.VisualScripting;

namespace SeizureTerritory.Scripts.ECS
{
    sealed class MovementSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<MovebleComponent> _movebleFilter = null;
        
        public void Run()
        {
            
        }
    }
}