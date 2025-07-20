using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> items = new List<Item>();
    private int mushrooms = 0;
    
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
        
    }
}
