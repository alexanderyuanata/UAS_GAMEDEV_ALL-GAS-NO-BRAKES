using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject Inventory;
    public bool inventoryIsClose;
    void Start()
    {
        inventoryIsClose = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (inventoryIsClose == true)
            {
                Inventory.SetActive(true);
                inventoryIsClose = false;
            } else
            {
                Inventory.SetActive(false);
                inventoryIsClose = true;
            }
        }
    }
}
