using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConditionalItem : InteractableObject
{
    [SerializeField]
    private PuzzleAction ActionScript;

    [Tooltip("Dialog inisial yang ditampilkan")]
    [SerializeField]
    [TextArea(1, 3)]
    private string[] initial_dialogue;

    [Tooltip("Kondisi-kondisi yang dicek")]
    [SerializeField]
    private ConditionBranches[] conditions;

    [Tooltip("Dialog jika tidak ada kondisi terpenuhi")]
    [SerializeField]
    [TextArea(1, 3)]
    private string[] exception_response;

    [SerializeField] private bool play_exception = false;
    [SerializeField] private bool do_action = false;
    private bool condition_found = false;

    [System.Serializable]
    private class ConditionBranches
    {
        [Tooltip("Item yang akan dicek keberadaannya")]
        [SerializeField] private InventoryItem condition_item;
        [Tooltip("Dialog jika kondisi dipenuhi")]
        [SerializeField][TextArea(1, 3)] private string[] condition_response;

        public ConditionBranches(InventoryItem item, string[] responses)
        {
            this.condition_item = item;
            this.condition_response = responses;
        }

        public string[] responses
        {
            get
            {
                return condition_response;
            }
        }

        public InventoryItem conditional_item
        {
            get
            {
                return condition_item;
            }
        }
    }

    private void Start()
    {
        if (ActionScript == null && do_action)
        {
            ActionScript = gameObject.GetComponent<PuzzleAction>();
        }
    }

    public override void TryInteract()
    {
        string[] final_dialogue = initial_dialogue;

        foreach (var condition in conditions)
        {
            if (PlayerInventory.instance.checkItem(condition.conditional_item))
            {
                final_dialogue = final_dialogue.Concat(condition.responses).ToArray();
                if (do_action) ActionScript.action();
                condition_found = true;
                break;
            }
        }

        if (!condition_found && play_exception) final_dialogue = final_dialogue.Concat(exception_response).ToArray();

        DialogueManager.instance.startDialogue(final_dialogue);
    }
}
