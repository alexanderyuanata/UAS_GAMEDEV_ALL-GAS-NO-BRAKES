using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogueEntry : MonoBehaviour
{
    [SerializeField] string text_entry;

    public string getText()
    {
        return this.text_entry;
    }
}
