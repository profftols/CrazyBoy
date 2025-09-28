using System;
using System.Collections.Generic;
using Lean.Transition.Method;
using UnityEngine;
using UnityEngine.Serialization;
using TARGET = UnityEngine.CanvasGroup;

namespace Lean.Transition.Method
{
    /// <summary>This component allows you to transition the CanvasGroup's interactable value.</summary>
    [HelpURL(LeanTransition.HelpUrlPrefix + "LeanCanvasGroupInteractable")]
    [AddComponentMenu(LeanTransition.MethodsMenuPrefix + "CanvasGroup/CanvasGroup.interactable" +
                      LeanTransition.MethodsMenuSuffix + "(LeanCanvasGroupInteractable)")]
    public class LeanCanvasGroupInteractable : LeanMethodWithStateAndTarget
    {
        public override Type GetTargetType()
        {
            return typeof(TARGET);
        }

        public override void Register()
        {
            PreviousState = Register(GetAliasedTarget(Data.Target), Data.Value, Data.Duration);
        }

        public static LeanState Register(TARGET target, bool value, float duration)
        {
            var state = LeanTransition.SpawnWithTarget(State.Pool, target);

            state.Value = value;

            return LeanTransition.Register(state, duration);
        }

        [Serializable]
        public class State : LeanStateWithTarget<TARGET>
        {
            [Tooltip("The interactable value will transition to this.")] [FormerlySerializedAs("Interactable")]
            public bool Value = true;

            public override int CanFill => Target != null && Target.interactable != Value ? 1 : 0;

            public override void FillWithTarget()
            {
                Value = Target.interactable;
            }

            public override void BeginWithTarget()
            {
            }

            public override void UpdateWithTarget(float progress)
            {
                if (progress == 1.0f) Target.interactable = Value;
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
        public static TARGET interactableTransition(this TARGET target, bool value, float duration)
        {
            LeanCanvasGroupInteractable.Register(target, value, duration);
            return target;
        }
    }
}