using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : InteractableObject
{
    [SerializeField] private Collider objcollider;
    [SerializeField] private InventoryItem item_data;
    [SerializeField] private ushort item_count;
    [SerializeField] private string[] pickup_text;
    [SerializeField] private bool enable_popup = false;
    [SerializeField] private bool enable_destroy = true;
    [SerializeField] private bool pickup_once = true;

    void Start()
    {
        objcollider = gameObject.GetComponent<Collider>();
    }

    public override void TryInteract()
    {
        if (pickup_once)
        {
            //try and add item, also play pickup SFX
            PlayerInventory.instance.addItem(item_data, item_count);
            SFXManager1.instance.playItemPickup();

            //when picked up
            if (enable_popup) DialogueManager.instance.startDialogue(pickup_text);
            if (enable_destroy) Destroy(gameObject);
            pickup_once = false;
        }
    }
}
