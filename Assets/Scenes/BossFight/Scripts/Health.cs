using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float _health = 100.0f;
    
    public void TakeDamage(float damage)
    {
        if (IsDead(damage))
        {
            
            return;
        }
        
        _health -= damage;
    }
    
    private bool IsDead(float damage)
    {
        return _health <= damage;
    }
}
