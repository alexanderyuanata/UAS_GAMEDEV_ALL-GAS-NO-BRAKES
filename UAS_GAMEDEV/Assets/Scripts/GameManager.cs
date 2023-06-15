using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject gameover_screen;
    [SerializeField] private Image crosshair;
    [SerializeField] private GameObject pause_screen;

    private bool input_enabled = true;
    private bool paused = false;
    private bool playing = true;

    private void Awake()
    {
        instance = this;
    }

    public void setPlaying(bool b)
    {
        playing = b;
    }

    private void Start()
    {
        gameover_screen.SetActive(false);
        pause_screen.SetActive(false);
        resumeGame();
    }

    public bool isInputEnabled()
    {
        return input_enabled;
    }

    public void enableInputs()
    {
        input_enabled = true;
        MouseLook.instance.enabled = true;
        PlayerMovement.instance.enabled = true;
    }

    public void disableInputs()
    {
        input_enabled = false;
        MouseLook.instance.enabled = false;
        PlayerMovement.instance.enabled = false;
    }

    public void gameOver()
    {
        pauseGame();
        gameover_screen.SetActive(true);
        playing = false;
    }

    private void pauseGame()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        crosshair.enabled = false;

        AudioListener.pause = true;

        Time.timeScale = 0f;
    }

    private void resumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        crosshair.enabled = true;

        AudioListener.pause = false;

        Time.timeScale = 1f;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && playing)
        {
            if (!paused)
            {
                pause_screen.SetActive(true);
                RandomPauseText.instance.genRandText();
                pauseGame();
            }
            else
            {
                pause_screen.SetActive(false);
                resumeGame();
            }
            paused = !paused;
        }
    }
}
