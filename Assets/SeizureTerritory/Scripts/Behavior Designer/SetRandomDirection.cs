using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class SetRandomDirection : Action
{
    public SharedVector2 Direction;

    private Vector2 _direction;

    public override void OnStart()
    {
        DefineDirection();
    }

    public override TaskStatus OnUpdate()
    {
        Direction.Value = _direction;
        return TaskStatus.Success;
    }

    private void DefineDirection()
    {
        // Написать логику которая бы определяла в какую сторону двигаться
    }
}