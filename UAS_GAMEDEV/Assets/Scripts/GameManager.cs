using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private bool input_enabled = true;

    private void Awake()
    {
        instance = this;
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

    // Update is called once per frame
    void Update()
    {

    }
}
