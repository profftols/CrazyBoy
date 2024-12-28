using UnityEngine;

namespace SeizureTerritory.Scripts
{
    public interface ITrigger
    {
        void Triggers(Collider collider);
    }
}