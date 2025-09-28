using System;
using System.Collections.Generic;
using Lean.Transition.Method;
using UnityEngine;
using TARGET = UnityEngine.RectTransform;

namespace Lean.Transition.Method
{
    /// <summary>This component allows you to transition the RectTransform's offsetMin.y value.</summary>
    [HelpURL(LeanTransition.HelpUrlPrefix + "LeanRectTransformOffsetMin_y")]
    [AddComponentMenu(LeanTransition.MethodsMenuPrefix + "RectTransform/RectTransform.offsetMin.y" +
                      LeanTransition.MethodsMenuSuffix + "(LeanRectTransformOffsetMin_y)")]
    public class LeanRectTransformOffsetMin_y : LeanMethodWithStateAndTarget
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
            [Tooltip("The offsetMin value will transition to this.")]
            public float Value;

            [Tooltip("This allows you to control how the transition will look.")]
            public LeanEase Ease = LeanEase.Smooth;

            [NonSerialized] private float oldValue;

            public override int CanFill => Target != null && Target.offsetMin.y != Value ? 1 : 0;

            public override void FillWithTarget()
            {
                Value = Target.offsetMin.y;
            }

            public override void BeginWithTarget()
            {
                oldValue = Target.offsetMin.y;
            }

            public override void UpdateWithTarget(float progress)
            {
                var vector = Target.offsetMin;

                vector.y = Mathf.LerpUnclamped(oldValue, Value, Smooth(Ease, progress));

                Target.offsetMin = vector;
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
        public static TARGET offsetMinTransition_y(this TARGET target, float value, float duration,
            LeanEase ease = LeanEase.Smooth)
        {
            LeanRectTransformOffsetMin_y.Register(target, value, duration, ease);
            return target;
        }
    }
}