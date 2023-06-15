using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypress : PuzzleAction
{
    [SerializeField] private short key;

    public override void action()
    {
        Keypad.instance.addKeypress(key);
    }
}
