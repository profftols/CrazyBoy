using System.Collections.Generic;
using UnityEngine;

namespace SeizureTerritory.Scripts.Behavior
{
    public class ScanState : StateBot
    {
        private readonly Queue<Vector2> _directions;
        private readonly float _distance = 1f;
        private float _radius = 7.5f;

        public ScanState(Bot bot) : base(bot)
        {
            _directions = new Queue<Vector2>();
        }

        public override void Enter()
        {
            FindPositions();
            
            _bot.ChangeState(new RunState(_directions, _bot));
        }

        private void FindPositions()
        {
            var transform = _bot.transform;
            var position = transform.position;
            RaycastHit[] hits = Physics.SphereCastAll(position, _radius, transform.forward, _distance);
            List<Land> lands = new List<Land>();

            foreach (var hit in hits)
            {
                if (hit.collider.gameObject.TryGetComponent(out Land land))
                {
                    if (land.IsNotDefaultMaterial(_bot.Render.material))
                    {
                        lands.Add(land);
                    }
                }
            }

            if (lands.Count == 0)
            {
                _radius++;
                FindPositions(); //УБрать погнать еще раз увелич радиусом
            }
            else
            {
                var newPosition = lands[Random.Range(0, lands.Count)].transform.position;
                _directions.Enqueue(new Vector2(newPosition.x, newPosition.z));
            }

            var lastPosition = lands[Random.Range(0, lands.Count)].transform.position;
            _directions.Enqueue(new Vector2(lastPosition.x, lastPosition.z));
            Vector3 homePosition = _bot.OnMinimumDistanceInvoke();
            _directions.Enqueue(new Vector2(homePosition.x, homePosition.z));
        }
    }
}