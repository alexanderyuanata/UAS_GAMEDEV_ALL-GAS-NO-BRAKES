using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEasels : PuzzleAction
{
    [SerializeField] private GameObject this_easel;
    [SerializeField] private GameObject new_easel;

    public override void action()
    {
        new_easel.SetActive(true);
        Destroy(this_easel);
    }
}
