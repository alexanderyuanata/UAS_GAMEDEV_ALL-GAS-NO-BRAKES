using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ...

public class InventoryUI : MonoBehaviour
{
    public GameObject inventorySlotPrefab; // Reference to the inventory slot prefab

    private List<GameObject> inventorySlots; // List to store instantiated inventory slot UI elements

    private void OnEnable()
    {
        // Subscribe to the inventory change event
        PlayerInventory.instance.OnInventoryChange += UpdateUI;
    }

    private void OnDisable()
    {
        // Unsubscribe from the inventory change event
        PlayerInventory.instance.OnInventoryChange -= UpdateUI;
    }

    private void UpdateUI()
    {
        // Iterate over the inventory items and update the UI accordingly
        PlayerInventory playerInventory = PlayerInventory.instance;

        // Clear existing inventory slots
        foreach (GameObject slot in inventorySlots)
        {
            Destroy(slot);
        }
        inventorySlots.Clear();

        // Iterate over the inventory items and create inventory slot UI elements
        foreach (PlayerInventory.ItemInsideInventory inventoryItem in playerInventory.InventoryItems)
        {
            // Instantiate or activate an inventory slot prefab/UI element
            GameObject slot = Instantiate(inventorySlotPrefab, transform);
            inventorySlots.Add(slot);

            // Set the properties of the inventory slot based on the item data
            InventoryItem slotUI = slot.GetComponent<InventoryItem>();
            
        }
    }
}

