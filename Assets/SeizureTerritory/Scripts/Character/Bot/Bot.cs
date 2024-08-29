using System;
using System.Collections;
using System.Collections.Generic;
using SeizureTerritory.Scripts.Behavior;
using UnityEngine;

public class Bot : Character
{
    public float radius = 7.5f;
    
    private Renderer _renderMaterial;
    private StateMachine _stateMachine;
    private ScanState _scanState;

    private void Awake()
    {
        _renderMaterial = GetComponent<Renderer>();
    }

    protected override void Start()
    {
        base.Start();
        _stateMachine = new StateMachine();
        _scanState = new ScanState(this);
        _stateMachine.Initialize(_scanState);
    }
    
    private void Update()
    {
        if (_stateMachine.CurrentStateBot != null)
        {
            (_stateMachine.CurrentStateBot as RunState)?.SetSpeed(Speed, BonusSpeed);
            _stateMachine.CurrentStateBot.Update();
        }
    }

    public void SetMaterial(Material material) => _renderMaterial.material = material;
    
    public void ChangeState(StateBot newStateBot) => _stateMachine.ChangeState(newStateBot);
}
