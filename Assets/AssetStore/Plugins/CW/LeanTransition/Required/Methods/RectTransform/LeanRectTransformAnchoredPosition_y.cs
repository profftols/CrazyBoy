using System;
using System.Collections.Generic;
using Lean.Transition.Method;
using UnityEngine;
using UnityEngine.Serialization;
using TARGET = UnityEngine.RectTransform;

namespace Lean.Transition.Method
{
    /// <summary>This component allows you to transition the RectTransform's anchoredPosition.y value.</summary>
    [HelpURL(LeanTransition.HelpUrlPrefix + "LeanRectTransformAnchoredPosition_y")]
    [AddComponentMenu(LeanTransition.MethodsMenuPrefix + "RectTransform/RectTransform.anchoredPosition.y" +
                      LeanTransition.MethodsMenuSuffix + "(LeanRectTransformAnchoredPosition_y)")]
    public class LeanRectTransformAnchoredPosition_y : LeanMethodWithStateAndTarget
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
            [Tooltip("The anchoredPosition value will transition to this.")] [FormerlySerializedAs("Position")]
            public float Value;

            [Tooltip("This allows you to control how the transition will look.")]
            public LeanEase Ease = LeanEase.Smooth;

            [NonSerialized] private float oldValue;

            public override int CanFill => Target != null && Target.anchoredPosition.y != Value ? 1 : 0;

            public override void FillWithTarget()
            {
                Value = Target.anchoredPosition.y;
            }

            public override void BeginWithTarget()
            {
                oldValue = Target.anchoredPosition.y;
            }

            public override void UpdateWithTarget(float progress)
            {
                var vector = Target.anchoredPosition;

                vector.y = Mathf.LerpUnclamped(oldValue, Value, Smooth(Ease, progress));

                Target.anchoredPosition = vector;
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
        public static TARGET anchoredPositionTransition_y(this TARGET target, float value, float duration,
            LeanEase ease = LeanEase.Smooth)
        {
            LeanRectTransformAnchoredPosition_y.Register(target, value, duration, ease);
            return target;
        }
    }
}