using System;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private MonoBehaviour _inputSourceBehaviour;
    
    public float radius = 8.5f;

    private CharacterController _characterController;
    private ICharacterInputSource _inputSource;
    
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _inputSource = (ICharacterInputSource)_inputSourceBehaviour;
    }

    private void Update()
    {
        var movement = new Vector3(_inputSource.MovementInput.x, 0f, _inputSource.MovementInput.y);
        movement *= Speed + BonusSpeed;
        _characterController.SimpleMove(movement);
    }
}