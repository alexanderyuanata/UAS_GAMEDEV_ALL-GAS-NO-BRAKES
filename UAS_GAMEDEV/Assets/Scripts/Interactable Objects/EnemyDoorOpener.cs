using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDoorOpener : MonoBehaviour
{

    private void OnCollisionEnter(Collision other)
    {
        DoorScript door = other.gameObject.GetComponent<DoorScript>();
        if (door != null)
        {
            door.TryOpen();
        }
    }
}
