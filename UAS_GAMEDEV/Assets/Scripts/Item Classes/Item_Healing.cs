using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Healing")]
public class Item_Healing : InventoryItem
{
    public ushort health_recovered;

    public override void DeleteItem()
    {

    }

}