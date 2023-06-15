using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : InteractableObject
{
    [SerializeField] private float initial_rotation = 0;
    [SerializeField] private float toggled_rotation = 0;
    [SerializeField] private static float duration = 0.25f;
    [SerializeField] private static AudioSource src;

    private bool toggled = false;
    private bool coroutine_running = false;

    private void Start()
    {
        Vector3 current_rotation = transform.eulerAngles;
        transform.rotation = Quaternion.Euler(current_rotation.x, initial_rotation, current_rotation.z);

        src = GetComponent<AudioSource>();
    }

    private IEnumerator DoorSwing(float start_value, float target_value)
    {
        coroutine_running = true;
        float elapsed = 0f;
        Vector3 current_rotation = transform.eulerAngles;

        float current_value;
        while (elapsed < duration)
        {
            current_value = Mathf.Lerp(start_value, target_value, elapsed / duration);

            transform.rotation = Quaternion.Euler(current_rotation.x, current_value, current_rotation.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        coroutine_running = false;
    }

    public void TryOpen()
    {
        if (!toggled) TryInteract();
    }
    public override void TryInteract()
    {
        if (!coroutine_running)
        {
            Debug.Log(src.clip);
            src.Play();
            if (!toggled)
            {
                StartCoroutine(DoorSwing(initial_rotation, toggled_rotation));
            }
            else
            {
                StartCoroutine(DoorSwing(toggled_rotation, initial_rotation));
            }

            toggled = !toggled;
        }
    }
}
