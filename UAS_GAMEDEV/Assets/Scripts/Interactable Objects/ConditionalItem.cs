using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConditionalItem : InteractableObject
{
    [SerializeField]
    [TextArea(1, 3)]
    private string[] initial_dialogue;

    [SerializeField]
    private InventoryItem[] conditions;

    [SerializeField]
    [TextArea(1, 3)]
    private string[] fulfilled_response;

    [SerializeField]
    [TextArea(1, 3)]
    private string[] exception_response;

    [SerializeField] private bool play_exception = false;
    [SerializeField] private bool action_onfulfilled = false;
    [SerializeField] private bool disabled_onfulfilled = false;
    [SerializeField] PuzzleAction _do;

    private bool disabled = false;
    private Collider col;

    private void Start()
    {
        if (disabled_onfulfilled)
        {
            col = GetComponent<Collider>();
        }
    }

    public override void TryInteract()
    {
        bool condition_fulfilled = true;
        string[] final_dialogue = initial_dialogue;

        foreach (var condition in conditions)
        {
            if (!PlayerInventory.instance.checkItem(condition))
            {
                if (play_exception) final_dialogue = final_dialogue.Concat(exception_response).ToArray();
                condition_fulfilled = false;
                break;
            }
        }

        if (condition_fulfilled)
        {
            final_dialogue = final_dialogue.Concat(fulfilled_response).ToArray();
            if (disabled_onfulfilled)
            {
                col.enabled = false;
            }

            if (action_onfulfilled)
            {
                Debug.Log("doing action");
                DialogueManager.instance.startDialogue(final_dialogue, _do);
                return;
            }
        }

        DialogueManager.instance.startDialogue(final_dialogue);
    }
}
