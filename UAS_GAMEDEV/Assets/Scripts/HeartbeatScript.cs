using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartbeatScript : MonoBehaviour
{
    [SerializeField] private Transform listener;
    [SerializeField] private Transform soundSource;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private float maxDistance;
    [SerializeField] private float minDistance;

    [SerializeField] private float maxVolume;

    private void Update()
    {
        float distance = Vector3.Distance(listener.position, soundSource.position);

        if (distance > minDistance && distance <= maxDistance)
        {
            audioSource.volume = Mathf.Lerp(maxVolume, 0f, (distance - minDistance) / (maxDistance - minDistance));
        }
        else if (distance <= minDistance)
        {
            audioSource.volume = maxVolume;
        }
        else
        {
            audioSource.volume = 0f;
        }
    }
}
