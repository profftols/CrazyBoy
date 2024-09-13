using System.Collections.Generic;
using System.Linq;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityAudioSource;
using UnityEngine;

public class ManagerLand
{
    private Map _map;
    private Dictionary<Land, IDeathHandler> _buffers;
    private Dictionary<Land, IDeathHandler> _owner;
    private List<IDeathHandler> _deathHandlers;

    private float _radius = 3f;
    private float _distance = 1f;
    
    public ManagerLand(Map map, List<IDeathHandler> characters)
    {
        _map = map;
        _buffers = new Dictionary<Land, IDeathHandler>();
        _owner = new Dictionary<Land, IDeathHandler>();
        _deathHandlers = new List<IDeathHandler>();

        foreach (var character in characters)
        {
            character.OnLand += AddBuffer;

            if (character is Bot)
            {
                (character as Bot).OnMinimumDistance += GetMinDistance;
            }
            
            Spawn(character);
        }
    }
    
    private void AddBuffer(IDeathHandler character, Land land)
    {
        if (_owner.ContainsKey(land))
        {
            if (_owner.ContainsValue(character))
            {
                if (_deathHandlers.Contains(character))
                {
                    KillCharacter(character);
                    return;
                }

                ConquerInside(character);
            }
        }
        else if (_buffers.ContainsKey(land))
        {
            if (_buffers.ContainsValue(character) == false)
            {
                _buffers.TryGetValue(land, out IDeathHandler enemy);
                _deathHandlers.Add(enemy);
            }
        }
        else
        {
            _buffers.Add(land, character);
            //land.SetMaterial(character.Render.material);
            land.ActivationOutline();
        }
    }

    private void KillCharacter(IDeathHandler deathHandler)
    {
        deathHandler.OnLand -= AddBuffer;
        
        if (deathHandler is Bot)
        {
            (deathHandler as Bot).OnMinimumDistance -= GetMinDistance;
        }
        
        deathHandler.HandleDeath();
    }

    private void ConquerInside(IDeathHandler character)
    {
        var lands = _buffers.Where(x => x.Value == character).Select(x => x.Key).ToList();
        
        AddOwner(lands, character);
        
        foreach (var land in lands)
        {
            _buffers.Remove(land);
        }
        
        lands.AddRange(_owner.Where(x => x.Value == character).Select(x => x.Key).ToList());

        var landsInside = _map.TakeLands(lands);

        AddOwner(landsInside, character);
    }

    private void AddOwner(List<Land> lands, IDeathHandler character)
    {
        for (int i = 0; i < lands.Count; i++)
        {
            _owner.Add(lands[i], character);
            lands[i].DeactivationOutline();
            lands[i].SetMaterial(character.Render.material);
        }
    }

    private Vector3 GetMinDistance(IDeathHandler character)
    {
        int minDistance = int.MaxValue;
        Vector3 closestDistance = new Vector3();
        var lands = _owner.Where(x => x.Value == character).Select(x => x.Key).ToList();
        
        foreach (var land in lands)
        {
            int distance = (int)Vector3.Distance(land.transform.position, character.GetTransform().position);

            if (distance < minDistance)
            {
                minDistance = distance;
                closestDistance = land.transform.position;
            }
        }

        return closestDistance;
    }
    
    private void Spawn(IDeathHandler character)
    {
        Vector3 origin = character.GetTransform().position;
        Vector3 direction = character.GetTransform().forward;

        RaycastHit[] hits = Physics.SphereCastAll(origin, _radius, direction, _distance);

        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.TryGetComponent(out Land land))
            {
                _owner.Add(land, character);
            }
        }
    }
}