using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void enableInputs()
    {
        MouseLook.instance.enabled = true;
        PlayerMovement.instance.enabled = true;
    }

    public void disableInputs()
    {
        MouseLook.instance.enabled = false;
        PlayerMovement.instance.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
