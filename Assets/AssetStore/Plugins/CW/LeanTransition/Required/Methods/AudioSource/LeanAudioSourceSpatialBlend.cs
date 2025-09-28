using System;
using System.Collections.Generic;
using Lean.Transition.Method;
using UnityEngine;
using TARGET = UnityEngine.AudioSource;

namespace Lean.Transition.Method
{
    /// <summary>This component allows you to transition the AudioSource's spatialBlend value.</summary>
    [HelpURL(LeanTransition.HelpUrlPrefix + "LeanAudioSourceSpatialBlend")]
    [AddComponentMenu(LeanTransition.MethodsMenuPrefix + "AudioSource/AudioSource.spatialBlend" +
                      LeanTransition.MethodsMenuSuffix + "(LeanAudioSourceSpatialBlend)")]
    public class LeanAudioSourceSpatialBlend : LeanMethodWithStateAndTarget
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
            [Tooltip("The spatialBlend value will transition to this.")] [Range(0.0f, 1.0f)]
            public float Value = 1.0f;

            [Tooltip("This allows you to control how the transition will look.")]
            public LeanEase Ease = LeanEase.Smooth;

            [NonSerialized] private float oldValue;

            public override int CanFill => Target != null && Target.spatialBlend != Value ? 1 : 0;

            public override void FillWithTarget()
            {
                Value = Target.spatialBlend;
            }

            public override void BeginWithTarget()
            {
                oldValue = Target.spatialBlend;
            }

            public override void UpdateWithTarget(float progress)
            {
                Target.spatialBlend = Mathf.LerpUnclamped(oldValue, Value, Smooth(Ease, progress));
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
        public static TARGET spatialBlendTransition(this TARGET target, float value, float duration,
            LeanEase ease = LeanEase.Smooth)
        {
            LeanAudioSourceSpatialBlend.Register(target, value, duration, ease);
            return target;
        }
    }
}