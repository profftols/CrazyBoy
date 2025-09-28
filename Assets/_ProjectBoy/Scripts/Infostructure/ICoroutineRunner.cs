using System.Collections;
using UnityEngine;

namespace _ProjectBoy.Scripts.Infostructure
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}