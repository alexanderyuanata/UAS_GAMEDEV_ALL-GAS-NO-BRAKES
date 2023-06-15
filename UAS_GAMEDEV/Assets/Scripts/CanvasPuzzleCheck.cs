using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPuzzleCheck : MonoBehaviour
{
    public static CanvasPuzzleCheck instance;

    [SerializeField] private PAINTING[] code = new PAINTING[4];
    [SerializeField] private PuzzleAction _do;

    public enum PAINTING { 
        SUNRISE,
        NOON,
        SUNSET,
        DUSK
    }

    private Queue<PAINTING> queue = new Queue<PAINTING>();

    private void Awake()
    {
        instance = this;
    }   

    public void addPainting(PAINTING p)
    {
        queue.Enqueue(p);

        if (queue.Count >= code.Length)
        {
            for (int i = 0; i < 4; i++)
            {
                if (queue.Dequeue() != code[i])
                {
                    TooltipScript.instance.startTooltip("Something went wrong... you should try again.", 2f, 0.15f);
                    queue.Clear();
                    return;
                }
            }

            TooltipScript.instance.startTooltip("Something has appeared...", 2f, 0.15f);
            queue.Clear();
            _do.action();
        }
    }
}
