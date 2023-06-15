using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoorScript : InteractableObject
{
    private static string[] locked_door_dialogue = {"You try opening the door.", "It's locked." };

    public override void TryInteract()
    {
        DialogueManager.instance.startDialogue(locked_door_dialogue);
    }
}
