using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<InventoryItem> inventoryItems = new List<InventoryItem>();
    [SerializeField] private ushort max_items;
    public void addItem(InventoryItem item)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
