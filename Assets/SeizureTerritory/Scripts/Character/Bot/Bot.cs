using System;
using System.Collections.Generic;
using SeizureTerritory.Scripts.Behavior;
using UnityEngine;
using Random = UnityEngine.Random;
using StateMachine = SeizureTerritory.Scripts.Behavior.StateMachine;

public class Bot : Character
{
    [SerializeField] private List<Material> _materials;

    private StateMachine _stateMachine;
    private ScanState _scanState;

    public event Func<IDeathHandler, Vector3> OnMinimumDistance;

    protected override void Start()
    {
        base.Start();
        SetMaterial();
        _stateMachine = new StateMachine();
        _scanState = new ScanState(this);
        _stateMachine.Initialize(_scanState);
    }

    private void Update()
    {
        if (_stateMachine?.CurrentStateBot != null)
        {
            (_stateMachine.CurrentStateBot as RunState)?.SetSpeed(Speed, bonusSpeed);
            _stateMachine.CurrentStateBot.Update();
        }
    }

    public Vector3 OnMinimumDistanceInvoke() => OnMinimumDistance?.Invoke(this) ?? transform.position;

    private void SetMaterial()
    {
        Render = GetComponent<Renderer>();
        var material = _materials[Random.Range(0, _materials.Count - 1)];
        Render.material = material;
        _materials.Remove(material);
    }

    public void ChangeState(StateBot newStateBot) => _stateMachine.ChangeState(newStateBot);
}