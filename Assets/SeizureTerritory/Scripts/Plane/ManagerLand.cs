using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ManagerLand
{
    private readonly Map _map;
    private Dictionary<Land, IDeathHandler> _buffers;
    private Dictionary<Land, IDeathHandler> _owner;
    private List<IDeathHandler> _deathList;

    private float _radius = 3f;
    private float _distance = 1f;

    public ManagerLand(Map map, List<IDeathHandler> characters)
    {
        _map = map;
        _buffers = new Dictionary<Land, IDeathHandler>();
        _owner = new Dictionary<Land, IDeathHandler>();
        _deathList = new List<IDeathHandler>();

        foreach (var character in characters)
        {
            character.OnLand += CheckLand;
            character.OnSpawn += Spawn;

            if (character is Bot)
            {
                (character as Bot).OnMinimumDistance += GetMinDistance;
            }
        }
    }

    private void CheckLand(IDeathHandler character, Land land)
    {
        if (_owner.ContainsKey(land))
        {
            _owner.TryGetValue(land, out var player);
            
            if (player == character)
            {
                if (_deathList.Contains(character))
                {
                    KillCharacter(character);
                    return;
                }

                if (_buffers.ContainsValue(character))
                {
                    ConquerInside(character);
                }
            }
        }
        else if (_buffers.ContainsKey(land))
        {
            _buffers.TryGetValue(land, out var player);
            
            if (player != character)
            {
                _buffers.Remove(land);
                WriteItDown(land, character);
                _deathList.Add(player);
            }
        }
        else
        {
            WriteItDown(land, character);
        }
    }

    private void KillCharacter(IDeathHandler character)
    {
        character.OnLand -= CheckLand;
        character.OnSpawn -= Spawn;
        _deathList.Remove(character);
        
        if (character is Bot)
        {
            (character as Bot).OnMinimumDistance -= GetMinDistance;
        }

        DeleteLand(_owner, character);
        DeleteLand(_buffers, character);
        character.HandleDeath();
    }

    private void DeleteLand(Dictionary<Land, IDeathHandler> lands, IDeathHandler character)
    {
        foreach (var land in lands.ToList())
        {
            if (land.Value == character)
            {
                _buffers.Remove(land.Key);
                _owner.Remove(land.Key);
                
                if (land.Key.IsNotDefaultMaterial(character.Render.material) == false)
                {
                    _map.SetDefaultMaterial(land.Key);
                }
                
                land.Key.DeactivationOutline();
            }
        }
    }

    private void ConquerInside(IDeathHandler character)
    {
        var lands = _buffers.Where(x => x.Value == character).Select(x => x.Key).ToList();

        AddOwner(lands, character);
        RemoveBuffers(lands);

        lands.AddRange(_owner.Where(x => x.Value == character).Select(x => x.Key).ToList());

        var landsInside = _map.TakeLands(lands);

        RemoveBuffers(landsInside);
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
                land.SetMaterial(character.Render.material);
            }
        }
    }

    private void WriteItDown(Land land, IDeathHandler character)
    {
        _buffers.Add(land, character);
        land.SetMaterial(character.Render.material);
        land.ActivationOutline();
    }

    private void RemoveBuffers(List<Land> lands)
    {
        foreach (var land in lands)
        {
            _buffers.Remove(land);
        }
    }
}