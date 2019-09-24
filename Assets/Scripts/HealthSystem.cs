using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem
{
    public event Action OnHealthChanged;

    private int health;
    private int healthMax;

    public HealthSystem(int healthMax)
    {
        SetHealth(healthMax);
    }

    public void SetHealth(int healthMax)
    {
        this.healthMax = healthMax;
        health = healthMax;
    }

    public int GetHealth()
    {
        return health;
    }

    public float GetHealthPercent()
    {
        return (float)health / healthMax;
    }

    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        if (health < 0) health = 0;
        OnHealthChanged?.Invoke();
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        if (health > healthMax) health = healthMax;
        OnHealthChanged?.Invoke();
    }
}
