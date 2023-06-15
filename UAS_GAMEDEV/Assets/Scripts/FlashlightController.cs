using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [SerializeField] private Transform target_camera;
    [SerializeField] private float approach_speed = 2f;
    [SerializeField] private Light _light;
    [SerializeField] private float flashlight_fade_duration;


    private bool on = true;
    private bool coroutine_flag = true;
    private float initial_intensity;

    private void Start()
    {
        initial_intensity = _light.intensity;
    }

    private IEnumerator FadeFlashlight(float start, float end)
    {
        float elapsed = 0f;
        coroutine_flag = false;

        while (elapsed < flashlight_fade_duration)
        {
            float t = elapsed / flashlight_fade_duration;

            _light.intensity = Mathf.Lerp(start, end, t);

            elapsed += Time.deltaTime;

            yield return null;
        }

        _light.intensity = end;
        coroutine_flag = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && coroutine_flag)
        {
            //if on then turn off
            if (on)
            {
                StartCoroutine(FadeFlashlight(initial_intensity, 0));
            }
            else
            {
                StartCoroutine(FadeFlashlight(0, initial_intensity));
            }

            on = !on;
        }

        //flashlight dynamic following
        Vector3 targetPosition = target_camera.position;
        Quaternion targetRotation = target_camera.rotation;

        transform.position = Vector3.Lerp(transform.position, targetPosition, approach_speed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, approach_speed * Time.deltaTime);
    }
}
