using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinDoors : InteractableObject
{
    [SerializeField] private bool circle = false;
    [SerializeField] private bool square = false;
    [SerializeField] private bool penta = false;
    [SerializeField] private bool hexa = false;

    [TextArea(5,3)]
    [SerializeField] string[] no_win_dialogue;

    public override void TryInteract()
    {
        if (circle && square && penta && hexa)
        {

        }
        else
        {
            DialogueManager.instance.startDialogue(no_win_dialogue);
        }
    }
}
