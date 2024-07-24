using Leopotam.Ecs;

namespace SeizureTerritory.Scripts.ECS
{
    sealed class PlayerInputSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTag, DirectionComponent> _directionFilter = null;
        
        public void Run()
        {
            foreach (var i in _directionFilter)
            {
                ref var _directionComponent = ref _directionFilter.Get2(i);
            }
        }
    }
}