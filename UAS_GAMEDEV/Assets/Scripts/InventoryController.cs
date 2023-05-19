using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject Inventory;
    public GameObject Toolbar;
    public GameObject Crosshair;
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
                Crosshair.SetActive(false);
                inventoryIsClose = false;
            } else
            {
                Inventory.SetActive(false);
                Toolbar.SetActive(true);
                Crosshair.SetActive(true);
                inventoryIsClose = true;
            }
        }
    }
}
