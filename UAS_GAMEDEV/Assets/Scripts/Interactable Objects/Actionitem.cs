using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actionitem : InteractableObject
{
    [SerializeField] private PuzzleAction _do;
    [SerializeField] private bool delete_oninteract;
    [SerializeField] private bool play_sound;
    [SerializeField] private AudioClip sound;
    public override void TryInteract()
    {
        if (play_sound)
        {
            SFXManager1.instance.playClip(sound);
        }

        _do.action();
        if (delete_oninteract)
        {
            Destroy(gameObject);
        }
    }
}
