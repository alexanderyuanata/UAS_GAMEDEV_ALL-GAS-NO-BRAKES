using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerPry : PuzzleAction
{
    [SerializeField] private AudioClip pry_sound;

    public override void action()
    {
        SFXManager2.instance.playClip(pry_sound);
        EnemyController.instance.setOn(true);
        TooltipScript.instance.startTooltip("they are searching for you...", 2f, 0.15f);
        Destroy(gameObject);
    }
}
