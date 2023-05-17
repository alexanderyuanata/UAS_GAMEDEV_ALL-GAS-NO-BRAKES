using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLights : MonoBehaviour
{
    [SerializeField] private float timer_max;
    [SerializeField] private int flicker_chance;

    private Light lights;
    private float timer;

    private void Start()
    {
        timer = timer_max;
        lights = GetComponent<Light>();
    }

    void Update()
    {
        if (timer <= 0f)
        {
            timer = timer_max;
            if (Random.Range(1, 100) < flicker_chance)
            {
                lights.enabled = false;
            }
        }
        else
        {
            lights.enabled = true;
            timer -= Time.deltaTime;
        }
    }
}
