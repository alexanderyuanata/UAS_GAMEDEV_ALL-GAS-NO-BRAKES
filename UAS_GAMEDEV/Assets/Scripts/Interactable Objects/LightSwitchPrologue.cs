using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchPrologue : PuzzleAction
{
    [SerializeField] private GameObject[] objs;

    private bool off = true;

    private void Start()
    {
        foreach (GameObject o in objs)
        {
            o.SetActive(false);
        }
    }

    public override void action()
    {
        
        foreach (GameObject o in objs)
        {
            o.SetActive(off);
        }

        off = !off;
    }
}
