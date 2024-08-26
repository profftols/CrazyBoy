using UnityEngine;

namespace SeizureTerritory.Scripts.Behavior
{
    public class ScanState : State
    {
        private Transform _transform;
        private float _radius = 4f;
        private float _distance = 0f;
        private Vector3 direction;
        
        public ScanState(Transform transform)
        {
            _transform = transform;
        }
        
        public override void Enter()
        {
            RaycastHit[] hits = Physics.SphereCastAll(_transform.position, _radius, direction, _distance);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}