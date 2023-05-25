using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private InventorySlot[] slots;

    public static InventoryManager instance;

    private bool inventory_open = false;
    private CanvasGroup inventory_ui;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        inventory_ui = GetComponent<CanvasGroup>();
    }

    public void updateInventory()
    {
        Debug.Log("updating sprites");
        List<PlayerInventory.ItemInsideInventory> items = PlayerInventory.instance.InventoryItems;
        ushort i = 0;
        foreach (PlayerInventory.ItemInsideInventory item in items)
        {
            slots[i].Item = item;
            Debug.Log(item.getItem().getName());
            slots[i].updateSprite(item.getItem().icon);
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventory_open)
            {
                inventory_ui.alpha = 0;
            }
            else
            {
                updateInventory();
                inventory_ui.alpha = 1;
            }
            inventory_open = !inventory_open;
        }
    }
}
