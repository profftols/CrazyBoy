using System;
using SeizureTerritory.Scripts.Character;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(CharacterController))]
public class Character : MonoBehaviour, IDeathHandler
{
    protected const float Speed = 4f;
    
    public CharacterController ControllerCharacter { get; private set; }
    
    [HideInInspector] public float bonusSpeed;
    [HideInInspector] public bool isInvulnerable;

    public Renderer Render { get; protected set; }
    public event Action<IDeathHandler, Land> OnLand;
    
    protected virtual void Start()
    {
        ControllerCharacter = GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Land land))
        {
            OnLand?.Invoke(this, land);
        }

        if (other.gameObject.TryGetComponent(out Item item))
        {
            item.OnPickUp(this);
        }
    }

    public Transform GetTransform() => transform;
    
    public void HandleDeath()
    {
        if (isInvulnerable == false)
        {
            gameObject.SetActive(false);
        }
    }
}