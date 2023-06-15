using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class WinCondition : PuzzleAction
{
    [SerializeField] private float timeElapsed = 0f;
    [SerializeField] private EyelidController eyelid;
    [SerializeField] private float eyelid_duration;
    [SerializeField] private AudioClip ending_music;
    [SerializeField] private GameObject blackscreen;
    [SerializeField] private GameObject win_screen;
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private AudioSource[] sources;

    private bool timer_running = true;

    public override void action()
    {
        //end game
        StartCoroutine(startEndGame());
    }

    private IEnumerator startEndGame()
    {
        Destroy(EnemyController.instance);
        timer_running = false;
        GameManager.instance.setPlaying(false);
        PlayerMovement.instance.disablePlayerMovement();
        MouseLook.instance.disableMouseLook();

        SFXManager1.instance.stopClip();
        SFXManager2.instance.stopClip();
        BGM_Manager.instance.stopBGM();

        foreach (AudioSource s in sources)
        {
            s.Stop();
        }

        eyelid.startBar(3f);
        yield return new WaitForSeconds(0.5f);

        BGM_Manager.instance.playBGM(ending_music);
        yield return new WaitForSeconds(eyelid_duration + 0.5f);

        blackscreen.SetActive(true);
        yield return new WaitForSeconds(2f);

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        win_screen.SetActive(true);
        time.SetText("You have struggled for: " + formatTime());
    }

    private string formatTime()
    {
        string minutes, seconds;
        int s = Mathf.RoundToInt(timeElapsed);
        int secs = (s % 60);
        seconds = secs.ToString();
        minutes = ((s - secs) / 60).ToString();

        return minutes + " minutes and " + seconds + " seconds.";
    }

    private void Update()
    {
        if (timer_running)
        {
            timeElapsed += Time.deltaTime;
        }
    }
}
