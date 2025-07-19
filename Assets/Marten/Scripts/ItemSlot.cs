using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text name;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text stats;
    [SerializeField] private TMP_Text price;

    private void Start()
    {
        name.text = item.ItemName;
        description.text = item.Description;
        stats.text = item.GenerateStatsString();
        price.text = item.Price.ToString();
    }

    public void SetItem(Item newItem)
    {
        this.item = newItem;
    }
}
