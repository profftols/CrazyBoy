using System.Collections.Generic;
using System.Linq;
using SeizureTerritory.Scripts.Character;
using Unity.VisualScripting;

public class ManagerLand
{
    private Map _map;
    private Dictionary<Land, IDeathHandler> _buffers;
    private Dictionary<Land, IDeathHandler> _owner;
    private List<IDeathHandler> _deathHandlers;

    public ManagerLand(Map map, List<IDeathHandler> characters)
    {
        _map = map;
        _buffers = new Dictionary<Land, IDeathHandler>();
        _owner = new Dictionary<Land, IDeathHandler>();
        _deathHandlers = new List<IDeathHandler>();

        foreach (var character in characters)
        {
            character.OnLand += AddBuffer;
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
        }
    }

    private void KillCharacter(IDeathHandler deathHandler)
    {
        deathHandler.HandleDeath();
        deathHandler.OnLand -= AddBuffer;
    }

    private void ConquerInside(IDeathHandler character)
    {
        //Последовательность изменить, можнопеределать и оптимизировать
        
        
        /*List<Land> lands = _buffers.Where(x => x.Value == character).Select(x => x.Key).ToList();
        
        AddOwner(lands, character);
        RemoveBuffer(character);
        
        lands.AddRange(_owner.Where(x => x.Value == character).Select(x => x.Key).ToList());

        var landsInside = _map.TakeLands(lands);

        AddOwner(landsInside, character);*/
    }

    private void AddOwner(List<Land> lands, IDeathHandler character)
    {
        for (int i = 0; i < lands.Count; i++)
        {
            _owner.Add(lands[i], character);
        }
    }
    
    private void RemoveBuffer(IDeathHandler character)
    {
        foreach (var pair in _buffers.Where(x => x.Value == character).ToList())
        {
            _buffers.Remove(pair.Key);
        }
    }
}