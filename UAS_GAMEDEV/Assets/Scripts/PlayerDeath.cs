using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public static PlayerDeath instance;
    [SerializeField] private Camera player_camera;
    [SerializeField] private Camera death_camera;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        player_camera.enabled = true;
        death_camera.enabled = false;
    }

    public void triggerDeathCam()
    {
        player_camera.enabled = false;
        death_camera.enabled = true;
    }
}
