using System;
using System.Collections.Generic;
using Lean.Transition.Method;
using UnityEngine;
using UnityEngine.Serialization;
using TARGET = UnityEngine.AudioSource;

namespace Lean.Transition.Method
{
    /// <summary>This component allows you to transition the AudioSource's volume value.</summary>
    [HelpURL(LeanTransition.HelpUrlPrefix + "LeanAudioSourceVolume")]
    [AddComponentMenu(LeanTransition.MethodsMenuPrefix + "AudioSource/AudioSource.volume" +
                      LeanTransition.MethodsMenuSuffix + "(LeanAudioSourceVolume)")]
    public class LeanAudioSourceVolume : LeanMethodWithStateAndTarget
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
            [Tooltip("The volume value will transition to this.")] [FormerlySerializedAs("Volume")] [Range(0.0f, 1.0f)]
            public float Value = 1.0f;

            [Tooltip("This allows you to control how the transition will look.")]
            public LeanEase Ease = LeanEase.Smooth;

            [NonSerialized] private float oldValue;

            public override int CanFill => Target != null && Target.volume != Value ? 1 : 0;

            public override void FillWithTarget()
            {
                Value = Target.volume;
            }

            public override void BeginWithTarget()
            {
                oldValue = Target.volume;
            }

            public override void UpdateWithTarget(float progress)
            {
                Target.volume = Mathf.LerpUnclamped(oldValue, Value, Smooth(Ease, progress));
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
        public static TARGET volumeTransition(this TARGET target, float value, float duration,
            LeanEase ease = LeanEase.Smooth)
        {
            LeanAudioSourceVolume.Register(target, value, duration, ease);
            return target;
        }
    }
}