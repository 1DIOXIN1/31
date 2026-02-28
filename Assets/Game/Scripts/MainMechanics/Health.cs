using System;
using UnityEngine;

public class Health
{
    public Health(int startHealth)
    {
        MaxHealth = new ReactiveVariable<int>(startHealth);
        CurrentHealth = new ReactiveVariable<int>(MaxHealth.Value);
        IsDead = new ReactiveVariable<bool>(false);
    }
    
    private ReactiveVariable<int> MaxHealth;
    private ReactiveVariable<int> CurrentHealth;
    private ReactiveVariable<bool> IsDead;

    public IReadOnlyVariable<int> GetCurrentHealth() => CurrentHealth;
    public IReadOnlyVariable<int> GetMaxHealth() => MaxHealth;
    public IReadOnlyVariable<bool> GetIsDead() => IsDead;

    public void Heal(int value)
    {
        if(value < 0)
            throw new IndexOutOfRangeException("Неправильное значение хила");
        
        if(CurrentHealth.Value + value >= MaxHealth.Value)
            CurrentHealth = MaxHealth;
        else
            CurrentHealth.Value += value;

        Debug.Log("Текущее хп: " + CurrentHealth);
    }

    public void TakeDamage(int value)
    {
        if(IsDead.Value)
            return;

        if(value < 0)
            throw new IndexOutOfRangeException("Неправильное значение урона");

        if(CurrentHealth.Value - value <= 0)
        {
            CurrentHealth.Value = 0;
            IsDead.Value = true;
            Debug.Log("Персонаж умер");
            return;
        }
        
        CurrentHealth.Value -= value;
        Debug.Log("Текущее хп: " + CurrentHealth.Value);
    }
}
