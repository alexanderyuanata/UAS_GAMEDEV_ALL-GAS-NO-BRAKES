using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightPickup : PuzzleAction
{
    [SerializeField] GameObject flashlight;

    public override void action()
    {
        if (!flashlight.active)
        {
            flashlight.SetActive(true);
        }
    }
}
