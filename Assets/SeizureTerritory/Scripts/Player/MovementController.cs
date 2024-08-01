using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private CharacterController _controller;
    private Vector3 _moveDirection;

    public void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        _moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        _moveDirection.Normalize();
        
        Vector3 movement = _moveDirection * (_moveSpeed * Time.deltaTime);
        _controller.Move(movement);
    }
}
