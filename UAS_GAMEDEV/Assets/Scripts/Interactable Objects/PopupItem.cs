using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupItem : InteractableObject
{
    [SerializeField][TextArea(6, 3)] private string[] dialogue_text;

    public override void TryInteract()
    {
        DialogueManager.instance.startDialogue(dialogue_text);
    }
}
