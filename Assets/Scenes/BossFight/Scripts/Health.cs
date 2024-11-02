using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private float _health = 100.0f;
    
    public void TakeDamage(float damage)
    {
        _health -= damage;
    }
    
    public bool IsDead()
    {
        return _health <= 0.0f;
    }
    
    public void Heal(float heal)
    {
        _health += heal;
    }
}
