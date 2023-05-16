using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float pickup_range;
    public LayerMask layermask;
    public Camera player_camera;

    private void Update()
    {
        //if player tries to pick up an item
        if (Input.GetKeyDown(KeyCode.E) && GameManager.instance.isInputEnabled())
        {
            Ray ray = player_camera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction, Color.magenta, 4f);
            //if raycast hits a valid item
            if (Physics.Raycast(ray, out RaycastHit hit, pickup_range, layermask) /**&& hit.collider.CompareTag("Item")**/)
            {
                hit.collider.gameObject.GetComponent<InteractableObject>().TryInteract();
            }
        }
    }
}
