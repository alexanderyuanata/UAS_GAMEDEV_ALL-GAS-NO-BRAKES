using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Item Database")]

public class ItemDatabase : ScriptableObject
{
    [SerializeField] private List<InventoryItem> _ItemDatabase;

    [ContextMenu("Load Item Data")]
    public void loadItems()
    {
        _ItemDatabase = new List<InventoryItem>();

        var found_items = Resources.LoadAll<InventoryItem>("Items").OrderBy(i => i.ID).ToList();

        Debug.Log(found_items);

        var id_inrange = found_items.Where(i => i.ID != -1 && i.ID < found_items.Count).OrderBy(i => i.ID).ToList();
        var id_notinrange = found_items.Where(i => i.ID != -1 && i.ID >= found_items.Count).OrderBy(i => i.ID).ToList();
        var no_id = found_items.Where(i => i.ID <= -1).ToList();

        var index = 0;
        for (int i = 0; i < found_items.Count; i++)
        {
            InventoryItem to_add;
            to_add = id_inrange.Find(j => j.ID == i);

            if (to_add != null)
            {
                _ItemDatabase.Add(to_add);
            }
            else if (index < no_id.Count)
            {
                no_id[index].ID = i;
                to_add = no_id[index];
                index++;

                _ItemDatabase.Add(to_add);
            }
        }

        foreach(var item in id_notinrange)
        {
            _ItemDatabase.Add(item);
        }
    }

    public InventoryItem getItem(int find_id)
    {
        return _ItemDatabase.Find(i => i.ID == find_id);
    }

}


