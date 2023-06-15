using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrologueSequence : MonoBehaviour
{
    public static PrologueSequence instance;

    [SerializeField] private EyelidController eyelid;
    [SerializeField] private float eyelid_duration;
    [SerializeField] private float end_duration;
    [SerializeField] private GameObject blackscreen;
    [SerializeField] private GameObject lights;

    private void Awake()
    {
        instance = this;    
    }

    void Start()
    {
        StartCoroutine(initialSequence());
    }

    public IEnumerator initialSequence()
    {
        MouseLook.instance.disableMouseLook();
        PlayerMovement.instance.disablePlayerMovement();

        yield return new WaitForSeconds(1.5f);

        TooltipScript.instance.startTooltip("Whoever fights demons should see to it that in the process he does not become a monster.", 3.5f, 0.15f);

        yield return new WaitForSeconds(3.75f);

        TooltipScript.instance.startTooltip("Because if you gaze long enough into the abyss,", 3f, 0.15f);

        eyelid.startOpen(eyelid_duration);

        yield return new WaitForSeconds(eyelid_duration);

        MouseLook.instance.enableMouseLook();
        PlayerMovement.instance.enablePlayerMovement();

        BGM_Manager.instance.playBGM();
    }

    public IEnumerator endSequence()
    {
        BGM_Manager.instance.stopBGM();

        MouseLook.instance.disableMouseLook();
        PlayerMovement.instance.disablePlayerMovement();

        eyelid.startBar(eyelid_duration);

        yield return new WaitForSeconds(eyelid_duration);

        TooltipScript.instance.startTooltip("The abyss gazes back.", end_duration, 0.2f);
        yield return new WaitForSeconds(end_duration + 2f);

        lights.SetActive(false);
        AudioListener.pause = true;

        yield return new WaitForSeconds(1.5f);

        blackscreen.SetActive(true);

        SceneManagement.instance.loadScene("Gameplay");
    }
}
