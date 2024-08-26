using System.Collections.Generic;
using UnityEngine;

namespace SeizureTerritory.Scripts.Behavior
{
    public class ScanState : State
    {
        private Bot _bot;
        private Queue<Vector2> _directions;
        private float _radius = 7.5f;
        private float _distance = 1f;

        public ScanState(Bot bot)
        {
            _directions = new Queue<Vector2>();
            _bot = bot;
        }

        public override void Enter()
        {
            FindPosition();
        }

        private void FindPosition()
        {
            var transform = _bot.transform;
            var position = transform.position;
            RaycastHit[] hits = Physics.SphereCastAll(position, _radius, transform.forward, _distance);
            List<Land> lands = new List<Land>();
            
            foreach (var hit in hits)
            {
                if (hit.collider.gameObject.TryGetComponent(out Land land))
                {
                    if (land.IsNotValidMaterial(_bot.Colouring.TextureMaterial.material))
                    {
                        lands.Add(land);
                    }
                }
            }
            
            if (lands.Count == 0)
            {
                _radius++;
                FindPosition();
            }
            else
            {
                var newPosition = lands[Random.Range(0, lands.Count)].transform.position;
                _directions.Enqueue(new Vector2(newPosition.x - position.x, newPosition.z - position.z));
            }

            var lastPosition = lands[Random.Range(0, lands.Count)].transform.position;
            _directions.Enqueue(new Vector2(lastPosition.x - position.x, lastPosition.z - position.z));
            _directions.Enqueue(FindClosetPosition(hits, lastPosition));
        }

        private Vector2 FindClosetPosition(RaycastHit[] hits, Vector3 lastPosition)
        {
            float minDistance = Mathf.Infinity;
            Vector3 closestDistance = new Vector3();
            
            foreach (var hit in hits)
            {
                if (hit.collider.gameObject.TryGetComponent(out Land land))
                {
                    if (!land.IsNotValidMaterial(_bot.Colouring.TextureMaterial.material))
                    {
                        float distance = Vector3.Distance(land.transform.position, lastPosition);
                        
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            closestDistance = land.transform.position;
                        }
                    }
                }
            }
            
            return new Vector2(closestDistance.x, closestDistance.z);
        }
    }
}