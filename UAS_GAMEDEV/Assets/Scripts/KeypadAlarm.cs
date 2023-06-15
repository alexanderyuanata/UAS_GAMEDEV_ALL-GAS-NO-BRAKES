using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadAlarm : MonoBehaviour
{
    [SerializeField] private Light alarm_light;
    [SerializeField] private Material material;

    private Color light_color;
    private Color bulb_color;
    private bool currently_on = false;

    private void Start()
    {
        light_color = alarm_light.color;
        bulb_color = material.color;
    }

    private IEnumerator startAlarm()
    {
        currently_on = true;

        alarm_light.color = Color.red;
        material.color = Color.red;

        yield return new WaitForSeconds(1.5f);

        alarm_light.color = light_color;
        material.color = bulb_color;

        currently_on = false;
    }

    public void alarm()
    {
        if (!currently_on)
        {
            StartCoroutine(startAlarm());
        }
    }

}
