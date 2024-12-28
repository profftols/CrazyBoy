using System.Collections;
using UnityEngine;

public class ItemBonus : MonoBehaviour
{
    private float _timerSpeed = 2.8f;
    private float _timerInvulnerability = 1.5f;
    
    private void OnEnable()
    {
        EventBus.OnBonusSpeed += IncreaseSpeed;
        EventBus.OnBonusInvulnerability += ReceiveInvulnerability;
    }

    private void OnDisable()
    {
        EventBus.OnBonusSpeed -= IncreaseSpeed;
        EventBus.OnBonusInvulnerability -= ReceiveInvulnerability;
    }
    
    private void ReceiveInvulnerability(Character character, bool bonus)
    {
        character.isInvulnerable = bonus;
        StartCoroutine(StatTimer(character, bonus));
    }
    
    private void IncreaseSpeed(Character character, float bonus)
    {
        character.bonusSpeed = bonus;
        StartCoroutine(StatTimer(character));
    }
    
    private IEnumerator StatTimer(Character character, bool bonus)
    {
        var wait = new WaitForSeconds(_timerInvulnerability);
        yield return wait;
        character.isInvulnerable = !bonus;
    }
    
    private IEnumerator StatTimer(Character character)
    {
        var wait = new WaitForSeconds(_timerSpeed);
        yield return wait;
        character.bonusSpeed = 0f;
    }
}
