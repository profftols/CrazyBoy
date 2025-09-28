using System;
using System.Collections.Generic;
using Lean.Transition.Method;
using UnityEngine;
using UnityEngine.Serialization;
using TARGET = UnityEngine.Transform;

namespace Lean.Transition.Method
{
    /// <summary>This component allows you to transition the Transform's localPosition.x value.</summary>
    [HelpURL(LeanTransition.HelpUrlPrefix + "LeanTransformLocalPosition_x")]
    [AddComponentMenu(LeanTransition.MethodsMenuPrefix + "Transform/Transform.localPosition.x" +
                      LeanTransition.MethodsMenuSuffix + "(LeanTransformLocalPosition_x)")]
    public class LeanTransformLocalPosition_x : LeanMethodWithStateAndTarget
    {
        public override Type GetTargetType()
        {
            return typeof(TARGET);
        }

        public override void Register()
        {
            PreviousState = Register(GetAliasedTarget(Data.Target), Data.Value, Data.Duration, Data.Ease);
        }

        public static LeanState Register(TARGET target, float value, float duration, LeanEase ease = LeanEase.Smooth)
        {
            var state = LeanTransition.SpawnWithTarget(State.Pool, target);

            state.Value = value;

            state.Ease = ease;

            return LeanTransition.Register(state, duration);
        }

        [Serializable]
        public class State : LeanStateWithTarget<TARGET>
        {
            [Tooltip("The localPosition value will transition to this.")] [FormerlySerializedAs("Position")]
            public float Value;

            [Tooltip("This allows you to control how the transition will look.")]
            public LeanEase Ease = LeanEase.Smooth;

            [NonSerialized] private float oldValue;

            public override int CanFill => Target != null && Target.localPosition.x != Value ? 1 : 0;

            public override void FillWithTarget()
            {
                Value = Target.localPosition.x;
            }

            public override void BeginWithTarget()
            {
                oldValue = Target.localPosition.x;
            }

            public override void UpdateWithTarget(float progress)
            {
                var vector = Target.localPosition;

                vector.x = Mathf.LerpUnclamped(oldValue, Value, Smooth(Ease, progress));

                Target.localPosition = vector;
            }

            public static Stack<State> Pool = new();

            public override void Despawn()
            {
                Pool.Push(this);
            }
        }

        public State Data;
    }
}

namespace Lean.Transition
{
    public static partial class LeanExtensions
    {
        public static TARGET localPositionTransition_x(this TARGET target, float value, float duration,
            LeanEase ease = LeanEase.Smooth)
        {
            LeanTransformLocalPosition_x.Register(target, value, duration, ease);
            return target;
        }
    }
}