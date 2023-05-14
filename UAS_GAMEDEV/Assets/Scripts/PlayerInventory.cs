using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<ItemInsideInventory> inventoryItems = new List<ItemInsideInventory>();
    [SerializeField] private ushort max_items;
    public static PlayerInventory instance;

    private void Awake()
    {
        instance = this;
    }

    public InventoryItem equipped_weapon;

    public class ItemInsideInventory
    {
        private InventoryItem item;
        private ushort count;

        public ItemInsideInventory(InventoryItem item, ushort count)
        {
            this.item = item;
            this.count = count;
        }

        //get
        public InventoryItem getItem()
        {
            return this.item;
        }

        public ushort getCount()
        {
            return this.count;
        }

        //set
        public void addCount(ushort addition)
        {
            this.count += addition;
        }

        public void reduceCount(ushort sub)
        {
            this.count -= sub;
        }
    }

    //methods
    public void setWeapon(InventoryItem weapon)
    {
        this.equipped_weapon = weapon;
    }

    public void unequipWeapon()
    {
        this.equipped_weapon = null;
    }

    public void addItem(InventoryItem item, ushort count)
    {
        //if amount of items exceed max amount then abort
        if (inventoryItems.Count + 1 >= max_items)
        {
            Debug.Log("Too much item in inventory to add");
        }

        foreach (ItemInsideInventory element in inventoryItems)
        {
            //if the item is already inside
            if (element.getItem().getID() == item.getID())
            {
                //if you can still add item
                if (element.getItem().canAddItem(element.getCount(), count))
                {
                    Debug.Log("Item count increased");
                    element.addCount(count);
                    return;
                }
                else
                {
                    Debug.Log("Too much item in one stack");
                    return;
                }
            }
        }

        //add new item to inventory
        Debug.Log("Added new item to inventory");
        inventoryItems.Add(new ItemInsideInventory(item, count));
    }
}
