using System;
using System.Collections.Generic;
using SeizureTerritory.Scripts.Character;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(CharacterController))]
public class Character : MonoBehaviour, IDeathHandler
{
    protected const float Speed = 4f;
    
    public CharacterController ControllerCharacter { get; private set; }
    
    [HideInInspector] public float bonusSpeed;
    [HideInInspector] public bool isInvulnerable;
    
    public Action<IDeathHandler, Land> OnLand;
    
    private AreaLand _areaLand;
    
    protected virtual void Start()
    {
        ControllerCharacter = GetComponent<CharacterController>();
        var map = FindObjectOfType<Map>();
        _areaLand = new AreaLand(map, new Colouring(GetComponent<Renderer>()), transform);
        _areaLand.SetDeathHandler(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Land land))
        {
            //OnLand?.Invoke(this, land);
            
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
}