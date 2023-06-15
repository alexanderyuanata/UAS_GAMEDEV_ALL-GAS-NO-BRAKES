using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingSelect : PuzzleAction
{
    [SerializeField] private CanvasPuzzleCheck.PAINTING p;

    public override void action()
    {
        Debug.Log("adding " + p);
        CanvasPuzzleCheck.instance.addPainting(p);
    }
}
