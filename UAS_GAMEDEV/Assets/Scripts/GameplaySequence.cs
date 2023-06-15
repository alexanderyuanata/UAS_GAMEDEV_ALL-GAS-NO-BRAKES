using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySequence : MonoBehaviour
{
    [SerializeField] EyelidController eyelid;

    void Start()
    {
        StartCoroutine(sequence1());
    }

    public IEnumerator sequence1()
    {
        GameManager.instance.setPlaying(false);
        PlayerMovement.instance.disablePlayerMovement();
        MouseLook.instance.disableMouseLook();

        TooltipScript.instance.startTooltip("wake up, fool.", 1.5f, 0.15f);
        yield return new WaitForSeconds(2f);

        eyelid.startOpen(3f);
        yield return new WaitForSeconds(3.25f);

        PlayerMovement.instance.enablePlayerMovement();
        MouseLook.instance.enableMouseLook();
        GameManager.instance.setPlaying(true);
    }
}
