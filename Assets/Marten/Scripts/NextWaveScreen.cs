using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class NextWaveScreen : MonoBehaviour
{
    [SerializeField] private GameObject itemSlotPosition1, itemSlotPosition2, itemSlotPosition3, itemSlotPosition4, itemSlotPosition5;
    [SerializeField] private Item[] items;
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private Button rerollButton;

    private void Start()
    {
        GenerateItemSlots();
    }

    private void GenerateItemSlots()
    {
        List<Item> pool = new List<Item>();
        
        foreach (var item in items)
        {
            for (int i = 0; i < (int)item.Chance; i++)
            {
                pool.Add(item);
            }
        }
        
        int randomIndex1 = Random.Range(0, pool.Count - 1);
        int randomIndex2 = Random.Range(0, pool.Count - 1);
        int randomIndex3 = Random.Range(0, pool.Count - 1);
        int randomIndex4 = Random.Range(0, pool.Count - 1);
        int randomIndex5 = Random.Range(0, pool.Count - 1);
        
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
        Destroy(itemSlotPosition1.transform.GetChild(0).gameObject);
        Destroy(itemSlotPosition2.transform.GetChild(0).gameObject);
        Destroy(itemSlotPosition3.transform.GetChild(0).gameObject);
        Destroy(itemSlotPosition4.transform.GetChild(0).gameObject);
        Destroy(itemSlotPosition5.transform.GetChild(0).gameObject);
    }
    
    public void RerollItems()
    {
        ClearItemSlots();
        GenerateItemSlots();
    }
}
