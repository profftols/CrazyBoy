using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementController
{
    [SerializeField] private float _moveSpeed;

    private CharacterController _controller;
    private Vector3 _moveDirection;
    private float _frezeY;

    public MovementController(CharacterController controller, float speed)
    {
        _controller = controller;
        _moveSpeed = speed;
    }

    public void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        _moveDirection = new Vector3(horizontalInput, _frezeY, verticalInput);
        _moveDirection.Normalize();
        
        Vector3 movement = _moveDirection * (_moveSpeed * Time.deltaTime);
        _controller.Move(movement);
    }
}
