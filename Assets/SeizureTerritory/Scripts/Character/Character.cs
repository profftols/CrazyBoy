using System;
using System.Collections.Generic;
using SeizureTerritory.Scripts.Character;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(CharacterController))]
public class Character : MonoBehaviour
{
    protected const float Speed = 4f;
    
    public CharacterController ControllerCharacter { get; private set; }
    
    public float bonusSpeed;
    public bool isInvulnerable;

    private AreaLand _areaLand;
    private Map _map;
    
    protected virtual void Start()
    {
        ControllerCharacter = GetComponent<CharacterController>();
        var map = FindObjectOfType<Map>();
        _areaLand = new AreaLand(map, new Colouring(GetComponent<Renderer>()), transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Land land))
        {
            if (_areaLand != null)
            {
                if (_areaLand.TryAddLand(land))
                {
                    return;
                }
                else if (isInvulnerable == false)
                {
                    Die();
                }
            }
        }

        if (other.gameObject.TryGetComponent(out Item item))
        {
            item.OnPickUp(this);
        }
    }
    
    public Vector3 GetMinimumDistance(Vector3 position) => _areaLand.GetMinimumDistance(position);

    private void Die()
    {
        _areaLand.Clear();
        gameObject.SetActive(false);
    }
}