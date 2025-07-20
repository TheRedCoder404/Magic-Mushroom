using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class NextWaveScreen : MonoBehaviour
{
    [SerializeField] private GameObject itemSlotPosition1, itemSlotPosition2, itemSlotPosition3, itemSlotPosition4, itemSlotPosition5;
    [SerializeField] private Item[] items;
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private TMP_Text shroomText, rerollText;
    [SerializeField] private int rerollCost, defaultRerollCost = 5, rerollsLeft, rerollsPerTurn = 3, rerollsPerformed = 0;
    [SerializeField] private float rerollPriceMultiplier = 1.5f;

    private bool isRerollPurchaseable = false;
    private PlayerStats playerStats;
    
    private void Start()
    {
        playerStats = GameObject.FindFirstObjectByType<PlayerStats>();
        rerollCost = defaultRerollCost;
        CheckRerollPurchaseablity();
        ResetRerolls();
        GenerateItemSlots();
    }

    public void UpdateShroomText()
    {
        if (playerStats) shroomText.text = "Shrooms: " + playerStats.shrooms;
    }

    public void ResetRerolls()
    {
        rerollsLeft = rerollCost;
        UpdateRerollText();
    }

    public void UpdateRerollText()
    {
        rerollText.text = "Reroll - " + rerollCost;
    }
    
    public void CheckRerollPurchaseablity()
    {
        if (!playerStats) return;
        
        if (playerStats.shrooms >= rerollCost)
        {
            rerollText.faceColor = Color.green;
            isRerollPurchaseable = true;
        }
        else
        {
            rerollText.faceColor = Color.red;
            isRerollPurchaseable = false;
        }
        
        UpdateShroomText();
        UpdateRerollText();
    }
    
    public void RerollItems()
    {
        rerollsPerformed++;
        GenerateItemSlots();
    }
    
    public void BuyReroll()
    {
        CheckRerollPurchaseablity();
        if (!isRerollPurchaseable) return;

        playerStats.SpendShroom(rerollCost);
        UpdateShroomText();
        
        rerollsLeft--;
        RerollItems();
        CheckRerollPurchaseablity();
        
        rerollCost = Mathf.RoundToInt(rerollCost * rerollPriceMultiplier);
        UpdateRerollText();
    }

    private void GenerateItemSlots()
    {
        ClearItemSlots();
        UpdateShroomText();
        
        List<Item> pool = new List<Item>();
        
        foreach (var item in items)
        {
            for (int i = 0; i < (int)item.Chance + 1; i++)
            {
                pool.Add(item);
            }
        }
        
        int randomIndex1 = Math.Clamp(Random.Range(0, pool.Count), 0, pool.Count - 1);
        int randomIndex2 = Math.Clamp(Random.Range(0, pool.Count), 0, pool.Count - 1);
        int randomIndex3 = Math.Clamp(Random.Range(0, pool.Count), 0, pool.Count - 1);
        int randomIndex4 = Math.Clamp(Random.Range(0, pool.Count), 0, pool.Count - 1);
        int randomIndex5 = Math.Clamp(Random.Range(0, pool.Count), 0, pool.Count - 1);
        
        GameObject itemSlot1 = Instantiate(itemSlotPrefab, itemSlotPosition1.transform.position, itemSlotPosition1.transform.rotation, itemSlotPosition1.transform);
        itemSlot1.GetComponent<ItemSlot>().SetItem(items[randomIndex1]);
        GameObject itemSlot2 = Instantiate(itemSlotPrefab, itemSlotPosition2.transform.position, itemSlotPosition2.transform.rotation, itemSlotPosition2.transform);
        itemSlot2.GetComponent<ItemSlot>().SetItem(items[randomIndex2]);
        GameObject itemSlot3 = Instantiate(itemSlotPrefab, itemSlotPosition3.transform.position, itemSlotPosition3.transform.rotation, itemSlotPosition3.transform);
        itemSlot3.GetComponent<ItemSlot>().SetItem(items[randomIndex3]);
        GameObject itemSlot4 = Instantiate(itemSlotPrefab, itemSlotPosition4.transform.position, itemSlotPosition4.transform.rotation, itemSlotPosition4.transform);
        itemSlot4.GetComponent<ItemSlot>().SetItem(items[randomIndex4]);
        GameObject itemSlot5 = Instantiate(itemSlotPrefab, itemSlotPosition5.transform.position, itemSlotPosition5.transform.rotation, itemSlotPosition5.transform);
        itemSlot5.GetComponent<ItemSlot>().SetItem(items[randomIndex5]);
    }
    
    private void ClearItemSlots()
    {
        if (itemSlotPosition1.transform.childCount > 0) Destroy(itemSlotPosition1.transform.GetChild(0).gameObject);
        if (itemSlotPosition2.transform.childCount > 0) Destroy(itemSlotPosition2.transform.GetChild(0).gameObject);
        if (itemSlotPosition3.transform.childCount > 0) Destroy(itemSlotPosition3.transform.GetChild(0).gameObject);
        if (itemSlotPosition4.transform.childCount > 0) Destroy(itemSlotPosition4.transform.GetChild(0).gameObject);
        if (itemSlotPosition5.transform.childCount > 0) Destroy(itemSlotPosition5.transform.GetChild(0).gameObject);
    }

    public void CheckAllPurchaseablity()
    {
        if (itemSlotPosition1.transform.childCount > 0) 
            itemSlotPosition1.transform.GetChild(0).GetComponent<ItemSlot>().CheckPurchaseablity();
        if (itemSlotPosition2.transform.childCount > 0) 
            itemSlotPosition2.transform.GetChild(0).GetComponent<ItemSlot>().CheckPurchaseablity();
        if (itemSlotPosition3.transform.childCount > 0) 
            itemSlotPosition3.transform.GetChild(0).GetComponent<ItemSlot>().CheckPurchaseablity();
        if (itemSlotPosition4.transform.childCount > 0) 
            itemSlotPosition4.transform.GetChild(0).GetComponent<ItemSlot>().CheckPurchaseablity();
        if (itemSlotPosition5.transform.childCount > 0) 
            itemSlotPosition5.transform.GetChild(0).GetComponent<ItemSlot>().CheckPurchaseablity();
    }
}
