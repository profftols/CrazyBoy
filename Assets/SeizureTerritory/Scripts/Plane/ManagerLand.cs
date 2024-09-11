using System.Collections.Generic;
using SeizureTerritory.Scripts.Character;

public class ManagerLand
{
    private Map _map;
    private Dictionary<IDeathHandler, Land> _buffers;
    private Dictionary<IDeathHandler, Land> _owner;

    public ManagerLand(Map map, List<IDeathHandler> deathHandlers)
    {
        _map = map;
        _buffers = new Dictionary<IDeathHandler, Land>();
        _owner = new Dictionary<IDeathHandler, Land>();

        foreach (var deathHandler in deathHandlers)
        {
            deathHandler.OnLand += AddBuffer;
        }
    }
    
    /*private void AddLand(Land land)
    {
        if (_owner.Contains(land) == false)
        {
            _buffer.Add(land);
        }
        else if (Calculation.IsValidLands(_buffer))
        {
            if (_colouring.IsColorNotCorrect(_buffer))
            {
                return;
            }

            CompleteLand();
        }
    }*/

    private void AddBuffer(IDeathHandler deathHandler, Land land)
    {
        if (_owner.ContainsValue(land) == false)
        {
            _buffers.Add(deathHandler, land);
        }
    }

    private void KillCharacter(IDeathHandler deathHandler)
    {
        deathHandler.HandleDeath();
        deathHandler.OnLand -= AddBuffer;
    }
}