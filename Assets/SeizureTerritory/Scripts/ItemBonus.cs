using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBonus : MonoBehaviour
{
    private float _timerSpeed = 2.8f;
    private float _timerInvulnerability = 1.5f;
    
    private void OnEnable()
    {
        EventBus.OnBonusSpeed += IncreaseSpeed;
        EventBus.OnBonusInvulnerability += ReceiveInvulnerability;
        EventBus.OnComeBackItem += ComebackItem;
    }

    private void OnDisable()
    {
        EventBus.OnBonusSpeed -= IncreaseSpeed;
        EventBus.OnBonusInvulnerability -= ReceiveInvulnerability;
        EventBus.OnComeBackItem -= ComebackItem;
    }

    private void ComebackItem(Item obj)
    {
        
    }
    
    private void ReceiveInvulnerability(Character character, bool bonus)
    {
        character.IsInvulnerable = bonus;
        StartCoroutine(StatTimer(character, bonus));
    }
    
    private void IncreaseSpeed(Character character, float bonus)
    {
        character.BonusSpeed = bonus;
        StartCoroutine(StatTimer(character));
    }
    
    private IEnumerator StatTimer(Character character, bool bonus)
    {
        var wait = new WaitForSeconds(_timerInvulnerability);
        yield return wait;
        character.IsInvulnerable = !bonus;
    }
    
    private IEnumerator StatTimer(Character character)
    {
        var wait = new WaitForSeconds(_timerSpeed);
        yield return wait;
        character.BonusSpeed = 0f;
    }
}
