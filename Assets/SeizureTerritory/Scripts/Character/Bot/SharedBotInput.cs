using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

[Serializable]
public class SharedBotInput : SharedVariable<Bot>
{
    public static implicit operator SharedBotInput(Bot value) => new SharedBotInput { Value = value };
}
