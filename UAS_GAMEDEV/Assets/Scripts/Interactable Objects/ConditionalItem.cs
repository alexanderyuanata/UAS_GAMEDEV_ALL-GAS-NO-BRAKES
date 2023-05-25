using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConditionalItem : InteractableObject
{
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

    [System.Serializable]
    private class ConditionBranches
    {
        [SerializeField] private InventoryItem condition_item;
        [SerializeField][TextArea(1, 3)] private string[] condition_response;
        [SerializeField] private PuzzleAction script;
        [SerializeField] private bool do_action = false;
        [SerializeField] private ushort priority = 0;

        public ConditionBranches(InventoryItem item, string[] responses)
        {
            this.condition_item = item;
            this.condition_response = responses;
        }

        public string[] responses
        {
            get { return condition_response; }
        }

        public InventoryItem conditional_item
        {
            get { return condition_item; }
        }

        public ushort priorities
        {
            get { return priority; }
        }

        public void playAction()
        {
            if (do_action) script.action();
        }
    }

    private void Start()
    {
    }

    public override void TryInteract()
    {
        string[] final_dialogue = initial_dialogue;
        ConditionBranches chosen_condition = null;

        foreach (var condition in conditions)
        {
            if (PlayerInventory.instance.checkItem(condition.conditional_item))
            {
                if (chosen_condition == null)
                {
                    chosen_condition = condition;
                }
                
                if (chosen_condition.priorities >= condition.priorities)
                {
                    chosen_condition = condition;
                }
                
            }
        }
        if (chosen_condition != null)
        {
            final_dialogue = final_dialogue.Concat(chosen_condition.responses).ToArray();
            chosen_condition.playAction();
        }

        if (chosen_condition == null && play_exception) final_dialogue = final_dialogue.Concat(exception_response).ToArray();

        DialogueManager.instance.startDialogue(final_dialogue);
    }
}
