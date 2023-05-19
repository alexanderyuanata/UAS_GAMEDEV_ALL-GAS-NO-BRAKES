using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject Inventory;
    public GameObject Toolbar;
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
                Toolbar.SetActive(false);
                inventoryIsClose = false;
            } else
            {
                Inventory.SetActive(false);
                Toolbar.SetActive(true);
                inventoryIsClose = true;
            }
        }
    }
}
