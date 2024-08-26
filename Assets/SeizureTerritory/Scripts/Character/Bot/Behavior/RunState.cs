using System.Collections.Generic;
using UnityEngine;

namespace SeizureTerritory.Scripts.Behavior
{
    public class RunState : State
    {
        private Queue<Vector2> _directions;
        private Vector2 _direction;
        private readonly CharacterController _controller;
        private float _speed;

        public RunState(Queue<Vector2> directions, CharacterController controller)
        {
            _directions = new Queue<Vector2>(directions);
            _directions = directions;
            _controller = controller;
        }

        public override void Enter()
        {
            _direction = _directions.Dequeue();
        }

        public override void Exit()
        {
            /*var movement = new Vector3(_inputSource.MovementInput.x, 0f, _inputSource.MovementInput.y);
            movement *= Speed + BonusSpeed;
            ControllerCharacter.SimpleMove(movement);*/
        }

        public override void Update()
        {
            var position = _controller.transform.position;
            var movement = new Vector3(_direction.x - position.x, 0f, _direction.y - position.z);
            _controller.SimpleMove(movement.normalized * _speed);
        }

        public void SetSpeed(float speed, float bonusSpeed)
        {
            _speed = speed + bonusSpeed;
        }
    }
}