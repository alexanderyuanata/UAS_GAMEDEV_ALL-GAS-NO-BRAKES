using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpen : PuzzleAction
{
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource AudioSource;

    public override void action()
    {
        animator.SetTrigger("open_chest");
        AudioSource.Play();
    }
}
