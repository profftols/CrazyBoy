﻿using UnityEngine;

public class Player : Character
{
    [SerializeField] private MonoBehaviour _inputSourceBehaviour;

    private ICharacterInputSource _inputSource;
    private float _score;

    private void OnEnable()
    {
        EventBus.OnScore += AddScore;
    }

    protected override void Start()
    {
        base.Start();
        _inputSource = (ICharacterInputSource)_inputSourceBehaviour;
    }

    private void Update()
    {
        var movement = new Vector3(_inputSource.MovementInput.x, 0f, _inputSource.MovementInput.y);
        movement *= Speed + bonusSpeed;
        ControllerCharacter.SimpleMove(movement);
    }

    private void OnDisable()
    {
        EventBus.OnScore -= AddScore;
        EventBus.OnDefeatGame?.Invoke(_score);
    }

    private void AddScore(float score)
    {
        _score += score;
    }
}