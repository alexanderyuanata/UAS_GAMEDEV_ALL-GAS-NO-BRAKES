using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : InteractableObject
{
    [SerializeField] private Collider objcollider;
    [SerializeField] private InventoryItem item_data;
    [SerializeField] private ushort item_count;

    void Start()
    {
        objcollider = gameObject.GetComponent<Collider>();
    }

    public override void TryInteract()
    {
        Debug.Log(item_data.getName());
        //try and add item, also play pickup SFX
        PlayerInventory.instance.addItem(item_data, item_count);
        SFXManager1.instance.playItemPickup();
        //when picked up, fade out and destroy the item
        Destroy(gameObject);
    }
}
