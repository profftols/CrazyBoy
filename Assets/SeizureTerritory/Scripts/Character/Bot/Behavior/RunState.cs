using System.Collections.Generic;
using UnityEngine;

namespace SeizureTerritory.Scripts.Behavior
{
    public class RunState : StateBot
    {
        private readonly Queue<Vector2> _directions;
        private readonly CharacterController _controller;
        private Vector2 _positionTarget;
        private float _speed;

        public RunState(Queue<Vector2> directions, Bot bot) : base(bot)
        {
            _directions = new Queue<Vector2>(directions);
            _controller = bot.ControllerCharacter;
            _positionTarget = _directions.Dequeue();
        }

        public override void Update()
        {
            var position = _bot.transform.position;
            
            CheckPosition(position);
            
            var movement = new Vector3(_positionTarget.x - position.x, 0f, _positionTarget.y - position.z);
            _controller.SimpleMove(movement.normalized * _speed);

            if (_directions.Count == 0)
            {
                if (movement.sqrMagnitude < 0.2f)
                {
                    _bot.ChangeState(new ScanState(_bot));
                }
            }
        }

        public void SetSpeed(float speed, float bonusSpeed)
        {
            _speed = speed + bonusSpeed;
        }

        private void CheckPosition(Vector3 position)
        {
            if (_directions.Count > 0)
            {
                var wayPoint = new Vector3(_positionTarget.x - position.x, 0, _positionTarget.y - position.z);
                
                if (wayPoint.sqrMagnitude < 0.1f)
                {
                    _positionTarget = _directions.Dequeue();
                }
            }
        }
    }
}