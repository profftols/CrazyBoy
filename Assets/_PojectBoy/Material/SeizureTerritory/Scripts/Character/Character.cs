using System;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(CharacterController))]
public class Character : MonoBehaviour, IDeathHandler
{
    protected const float Speed = 4f;
    
    public CharacterController ControllerCharacter { get; private set; }
    
    [HideInInspector] public float bonusSpeed;
    [HideInInspector] public bool isInvulnerable;

    public Renderer Render { get; private set; }
    public event Action<IDeathHandler, Land> OnLand;
    public event Action<IDeathHandler> OnSpawn;

    private void Awake()
    {
        Render = GetComponent<Renderer>();
        ControllerCharacter = GetComponent<CharacterController>();
    }

    protected virtual void Start()
    {
        OnSpawn?.Invoke(this);
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