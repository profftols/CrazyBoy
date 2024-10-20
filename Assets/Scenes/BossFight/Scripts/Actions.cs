using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Actions : MonoBehaviour
{
    [SerializeField] protected Button _button;
    
    public abstract void Action();
}
