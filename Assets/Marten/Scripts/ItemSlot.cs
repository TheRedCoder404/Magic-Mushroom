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
    
    private NextWaveScreen nextWaveScreen;
    private bool isPurchaseable = false;

    private void Start()
    {
        nextWaveScreen = transform.parent.gameObject.transform.parent.gameObject.GetComponent<NextWaveScreen>();

        icon.sprite = item.ItemIcon;
        name.text = item.ItemName;
        description.text = item.Description;
        stats.text = item.GenerateStatsString();
        price.text = item.Price.ToString();
        CheckPurchaseablity();
    }

    public void CheckPurchaseablity()
    {
        PlayerStats playerStats = GameObject.FindFirstObjectByType<PlayerStats>();
        if (playerStats.shrooms >= item.Price)
        {
            price.faceColor = Color.green;
            isPurchaseable = true;
        }
        else
        {
            price.faceColor = Color.red;
            isPurchaseable = false;
        }
    }

    public void SetItem(Item newItem)
    {
        this.item = newItem;
    }
    
    public void GiveItemToPlayer()
    {
        if (!isPurchaseable) return;
    
        PlayerStats playerStats = GameObject.FindFirstObjectByType<PlayerStats>();
        playerStats.SpendShroom(item.Price);
        nextWaveScreen.UpdateShroomText();
        
        Inventory playerinventory = GameObject.FindFirstObjectByType<Inventory>();
        playerinventory.AddItem(item);
        
        if (item.AttackPrefab)
        {
            Player player = GameObject.FindFirstObjectByType<Player>();
            player.AddAttack(item.AttackPrefab);
        }
        
        nextWaveScreen.CheckAllPurchaseablity();
        Destroy(this.gameObject);
    }
}
