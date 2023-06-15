using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockPentaKey : PuzzleAction
{
    [SerializeField] private Light[] lights;
    [SerializeField] private GameObject key;

    private void Start()
    {
        key.SetActive(false);
    }

    public override void action()
    {
        foreach(Light l in lights)
        {
            l.color = Color.red;
        }
        key.SetActive(true);

    }
}
