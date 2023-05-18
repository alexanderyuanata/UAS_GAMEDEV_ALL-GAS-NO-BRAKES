using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public abstract class InventoryItem : ScriptableObject
{
    public int ID = -1;
    public string _name;
    [TextArea(5, 10)] public string _description;
    public ushort _max_stacks;
    public Sprite _icon;

    public abstract void DeleteItem();

    public bool canAddItem(ushort initial, ushort to_add)
    {
        return (initial + to_add) <= this._max_stacks;
    }

    //get
    public int getID()
    {
        return ID;
    }

    public string getName()
    {
        return _name;
    }

    public ushort getMaxStacks()
    {
        return _max_stacks;
    }
}
