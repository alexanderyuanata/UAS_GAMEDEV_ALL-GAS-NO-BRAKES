using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;

    [SerializeField] private List<InventoryItem> inventory;

    private void Awake()
    {
        instance = this;
    }

    public bool checkItem(InventoryItem check)
    {
        foreach (InventoryItem item in inventory)
        {
            if (item.Equals(check)) return true;
        }
        return false;
    }

    public void addItem(InventoryItem item)
    {
        inventory.Add(item);
    }

}
