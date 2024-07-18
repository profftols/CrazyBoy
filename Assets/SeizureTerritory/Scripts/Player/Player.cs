using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : Character
{
    [SerializeField] private float _speedMove;
    
    private MovementController _movement;
    
    private void Awake()
    {
        _movement = new MovementController(GetComponent<CharacterController>(), _speedMove);
    }

    private void Update()
    {
        _movement.Move();
    }

    private void OnDisable()
    {
        //GameOver.Invoke();
    }
}