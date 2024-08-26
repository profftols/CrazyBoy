using System;
using System.Collections;
using SeizureTerritory.Scripts.Behavior;
using UnityEngine;

public class Bot : Character
{
    public float radius = 3f;
    
    private Renderer _renderMaterial;
    private StateMachine _stateMachine;
    private RunState _runState;

    private void Awake()
    {
        _renderMaterial = GetComponent<Renderer>();
    }

    protected override void Start()
    {
        base.Start();
        _runState = new RunState(transform);
        _stateMachine = new StateMachine();
        _stateMachine.Initialize(_runState);
    }
    
    private void Update()
    {
        if (_stateMachine.CurrentState != null)
        {
            _stateMachine.CurrentState.Update();
        }
    }

    public void SetMaterial(Material material) => _renderMaterial.material = material;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
