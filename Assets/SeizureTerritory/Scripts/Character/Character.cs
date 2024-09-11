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
    
    private AreaLand _areaLand;
    private float _radius = 3f;
    private float _distance = 1f;
    
    protected virtual void Start()
    {
        ControllerCharacter = GetComponent<CharacterController>();
        Spawn();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Land land))
        {
            OnLand?.Invoke(this, land);
            
            if (_areaLand != null)
            {
                _areaLand.AddLand(land);
            }
        }

        if (other.gameObject.TryGetComponent(out Item item))
        {
            item.OnPickUp(this);
        }
    }
    
    public Vector3 GetMinimumDistance(Vector3 position) => _areaLand.GetMinimumDistance(position);

    public void HandleDeath()
    {
        if (isInvulnerable == false)
        {
            _areaLand.Clear();
            gameObject.SetActive(false);
        }
    }

    public Character GetMe() => this;

    private void Spawn()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.forward;

        RaycastHit[] hits = Physics.SphereCastAll(origin, _radius, direction, _distance);

        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.TryGetComponent(out Land land))
            {
                OnLand?.Invoke(this, land);
            }
        }
    }
}