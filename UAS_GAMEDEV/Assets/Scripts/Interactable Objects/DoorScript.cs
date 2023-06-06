using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : InteractableObject
{
    [SerializeField] private float initial_rotation = 0;
    [SerializeField] private float toggled_rotation = 0;

    private bool toggled = false;

    private void Start()
    {
        Vector3 current_rotation = transform.eulerAngles;
        transform.rotation = Quaternion.Euler(current_rotation.x, initial_rotation, current_rotation.z);
    }

    public override void TryInteract()
    {
        Vector3 current_rotation = transform.eulerAngles;
        if (!toggled)
        {
            transform.rotation = Quaternion.Euler(current_rotation.x, toggled_rotation, current_rotation.z);
        }
        else
        {
            transform.rotation = Quaternion.Euler(current_rotation.x, initial_rotation, current_rotation.z);
        }
        
        toggled = !toggled;
    }
}
