using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject Inventory;
    public bool inventoryIsClose;

    // Start is called before the first frame update
    void Start()
    {
        inventoryIsClose = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
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
