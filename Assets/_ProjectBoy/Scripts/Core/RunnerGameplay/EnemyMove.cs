using _ProjectBoy.Scripts.Core.RunnerGameplay.Plane;
using UnityEngine;

namespace _ProjectBoy.Scripts.Core.RunnerGameplay
{
    public class EnemyMove : Mover
    {
        private EnemyBrains _brains;
        [SerializeField] private float _radiusScan = 15f;

        private void Update()
        {
            if (_brains == null)
                return;

            _brains.Tick();
            Direction = _brains.GetDirection();
            Move();
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _radiusScan);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.2f);
        }
#endif

        public void Init(CharacterController controller, Animator animator, Runner runner)
        {
            base.Init(controller, animator);
            _brains = new EnemyBrains(transform, runner, LandGrabbing.Instance, _radiusScan);
        }
    }
}