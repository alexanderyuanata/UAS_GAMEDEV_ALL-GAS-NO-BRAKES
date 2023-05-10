using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Weapon")]
public class Item_Weapons : InventoryItem
{
    [SerializeField] private ushort damage;
    [SerializeField] private ushort max_ammo;
    [SerializeField] private float moving_inaccuracy;
    [SerializeField] private float recoil;
    [SerializeField] private float accuracy_recovery;

    public override void DeleteItem()
    {

    }

    public ushort getDamage()
    {
        return this.damage;
    }
}
