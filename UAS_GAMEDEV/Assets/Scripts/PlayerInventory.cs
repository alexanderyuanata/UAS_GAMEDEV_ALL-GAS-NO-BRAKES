using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<ItemInsideInventory> inventoryItems;
    [SerializeField] private ushort max_items;
    public static PlayerInventory instance;

    public List<ItemInsideInventory> InventoryItems
    {
        get { return inventoryItems; }
        set { inventoryItems = value; }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        inventoryItems = new List<ItemInsideInventory>();
    }

    [SerializeField] private Item_Weapons equipped_weapon;

    [System.Serializable]
    public class ItemInsideInventory
    {
        [SerializeField] private InventoryItem item;
        [SerializeField] private ushort count;

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
    public void setWeapon(Item_Weapons weapon)
    {
        this.equipped_weapon = weapon;
    }

    public void unequipWeapon()
    {
        this.equipped_weapon = null;
    }

    public bool checkItem(InventoryItem item_tocheck)
    {
        foreach (ItemInsideInventory element in inventoryItems)
        {
            if (element.getItem() == item_tocheck) return true;
        }

        return false;
    }

    public ItemInsideInventory getItem(InventoryItem item_toget)
    {
        foreach (ItemInsideInventory item in inventoryItems)
        {
            if (item.getItem() == item_toget) return item;
        }

        return null;
    }

    public bool addItem(InventoryItem item, ushort count)
    {
        bool duplicate_stack = false;
        ushort remainder = 0;

        //check if the item already exists inside the inventory
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
                    return true;
                }
                //if not then try making another stack
                else
                {
                    Debug.Log("Too much item in one stack, trying making another stack");
                    duplicate_stack = true;
                    //get how much item will overflow from the stack
                    remainder = (ushort)((element.getCount() + count) - element.getItem().getMaxStacks());
                    element.addCount((ushort)(element.getCount() - remainder));
                    break;
                }
            }
        }

        //if amount of items exceed max amount then abort
        if (inventoryItems.Count + 1 >= max_items)
        {
            Debug.Log("Too much item in inventory to add a new item");
            return (false || duplicate_stack);
        }
        else
        {
            //add new item to inventory
            Debug.Log("Added new item to inventory");
            if (duplicate_stack)
            {
                inventoryItems.Add(new ItemInsideInventory(item, remainder));
            }
            else
            {
                inventoryItems.Add(new ItemInsideInventory(item, count));
            }
            
            return true;
        }
    }

}
