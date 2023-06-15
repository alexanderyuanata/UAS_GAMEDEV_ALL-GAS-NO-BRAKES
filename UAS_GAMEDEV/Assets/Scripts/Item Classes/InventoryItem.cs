using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public abstract class InventoryItem : ScriptableObject
{
    public int ID = -1;
    public string _name;
    [TextArea(5, 10)] public string _description;

    public abstract void DeleteItem();

    //get
    public int getID()
    {
        return ID;
    }

    public string getName()
    {
        return _name;
    }
}