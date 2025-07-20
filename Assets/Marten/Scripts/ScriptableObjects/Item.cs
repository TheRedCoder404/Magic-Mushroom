using System;
using Marten.Scripts;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    [Header("Item Properties")]
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemIcon;
    [SerializeField] private string description;
    [SerializeField] private int price;
    [SerializeField] private GameObject attackPrefab;
    [SerializeField] private ChanceWeight chance;
    
    [Header("Item Stats")]
    [SerializeField] public StatType[] statType;
    [SerializeField] public Stats[] stats;
    [SerializeField] public float[] value;

    public string ItemName => itemName;
    public Sprite ItemIcon => itemIcon;
    public string Description => description;
    public int Price => price;
    public GameObject AttackPrefab => attackPrefab;
    public ChanceWeight Chance => chance;

    public string GenerateStatsString()
    {
        if (statType.Length == stats.Length && stats.Length == value.Length)
        {
            String statsBuilder = "";
            for (int i = 0; i < statType.Length; i++)
            {
                if (statsBuilder != "") statsBuilder += "\n";
                switch (statType[i])
                {
                    case StatType.Add:
                        statsBuilder += $"{stats[i]}: +{value[i]}";
                        break;
                    case StatType.Multiply:
                        statsBuilder += $"{stats[i]}: x{value[i]}";
                        break;
                    case StatType.Divide:
                        statsBuilder += $"{stats[i]}: /{value[i]}";
                        break;
                    case StatType.Subtract:
                        statsBuilder += $"{stats[i]}: -{value[i]}";
                        break;
                    case StatType.Set:
                        statsBuilder += $"{stats[i]}: = {value[i]}";
                        break;
                }
            }
            
            return statsBuilder;
        }
        
        return "";
    }
}
