using System.Collections;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected Character Character;
    protected virtual float Timer => 10f;

    public abstract void OnPickUp(Character character);

    private void OnEnable()
    {
        StartCoroutine(StatTimer());
    }

    private void OnDisable()
    {
        StopCoroutine(StatTimer());
    }
    
    private IEnumerator StatTimer()
    {
        var wait = new WaitForSeconds(Timer);

        yield return wait;
        
        gameObject.SetActive(false);
        EventBus.OnComebackItem?.Invoke(this);
    }
}