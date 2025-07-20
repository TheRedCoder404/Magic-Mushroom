using System;
using System.Collections.Generic;
using Marten.Scripts;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> items = new List<Item>();
    private PlayerStats playerStats;
    private int mushrooms = 0;

    private void Awake()
    {
        playerStats = GameObject.FindFirstObjectByType<PlayerStats>();
    }

    public int Mushrooms
    {
        get => mushrooms;
        set
        {
            if (value < 0) return;
            mushrooms = value;
        }
    }
    
    public void AddItem(Item item)
    {
        if (item is null) return;
        items.Add(item);
    }
    
    public void RemoveItem(Item item)
    {
        if (item is null) return;
        items.Remove(item);
    }
    
    public List<Item> GetItems()
    {
        return new List<Item>(items);
    }

    public void CalculateNewStats()
    {
        float maxHealth = playerStats.defaultMaxHealth;
        float currentHealth = playerStats.defaultMaxHealth;
        float damage = playerStats.defaultDamage;
        float speed = playerStats.defaultSpeed;
        float attackSpeed = playerStats.defaultAttackSpeed;
        float range = playerStats.defaultRange;
        float resistance = playerStats.defaultResistance;
        float critChance = playerStats.defaultCritChance;
        float critDamage = playerStats.defaultCritDamage;
        float lifesteal = playerStats.defaultLifesteal;
        float earning = playerStats.defaultEarning;

        foreach (var item in items)
        {
            for (int i = 0; i < item.statType.Length; i++)
            {
                switch (item.stats[i])
                {
                    case Stats.Health:
                        currentHealth = ChangeVariable(currentHealth, item.value[i], item.statType[i]);
                        break;
                    
                    case Stats.Damage:
                        damage = ChangeVariable(damage, item.value[i], item.statType[i]);
                        break;
                    
                    case Stats.Speed:
                        speed = ChangeVariable(speed, item.value[i], item.statType[i]);
                        break;
                    
                    case Stats.AttackSpeed:
                        attackSpeed = ChangeVariable(attackSpeed, item.value[i], item.statType[i]);
                        break;
                    
                    case Stats.Range:
                        range = ChangeVariable(range, item.value[i], item.statType[i]);
                        break;
                    
                    case Stats.Resistance:
                        resistance = ChangeVariable(resistance, item.value[i], item.statType[i]);
                        break;
                    
                    case Stats.CritChance:
                        critChance = ChangeVariable(critChance, item.value[i], item.statType[i]);
                        break;
                    
                    case Stats.CritDamage:
                        critDamage = ChangeVariable(critDamage, item.value[i], item.statType[i]);
                        break;
                    
                    case Stats.Lifesteal:
                        lifesteal = ChangeVariable(lifesteal, item.value[i], item.statType[i]);
                        break;
                    
                    case Stats.Earning:
                        earning = ChangeVariable(earning, item.value[i], item.statType[i]);
                        break;
                    
                    case Stats.MaxHealth:
                        maxHealth = ChangeVariable(maxHealth, item.value[i], item.statType[i]);
                        break;
                }
            }
        }
        
        playerStats.maxHealth = maxHealth;
        playerStats.currentHealth = currentHealth;
        playerStats.damage = damage;
        playerStats.speed = speed;
        playerStats.attackSpeed = attackSpeed;
        playerStats.range = range;
        playerStats.resistance = resistance;
        playerStats.critChance = critChance;
        playerStats.critDamage = critDamage;
        playerStats.lifesteal = lifesteal;
        playerStats.earning = earning;
    }

    private float ChangeVariable(float oldValue, float newValue, StatType statType)
    {
        switch (statType)
        {
            case StatType.Add:
                return oldValue + newValue;
            
            case StatType.Multiply:
                return oldValue * newValue;
            
            case StatType.Divide:
                return oldValue / newValue;
            
            case StatType.Subtract:
                return oldValue - newValue;
            
            case StatType.Set:
                return newValue;
            
            default:
                return oldValue;
        }
    }
}
