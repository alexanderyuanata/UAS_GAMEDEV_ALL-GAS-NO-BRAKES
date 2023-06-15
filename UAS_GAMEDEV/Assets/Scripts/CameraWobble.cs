using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWobble : MonoBehaviour
{
    [SerializeField] private Camera cam; // Reference to the main camera's transform

    // Wobble properties
    public float wobbleFrequency = 1f; // Frequency of the wobble effect
    public float wobbleAmplitude = 0.1f; // Amplitude of the wobble effect

    private Vector3 initialPosition; // Initial position of the camera

    void Awake()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
    }

    void OnEnable()
    {
        initialPosition = cam.transform.localPosition;
    }

    void Update()
    {
        /**
        // Calculate the wobble offset based on time
        float wobbleOffset = Mathf.Sin(Time.time * wobbleFrequency) * wobbleAmplitude;

        // Calculate the X and Y offsets based on the wobble offset
        float xOffset = Mathf.Sin(Time.time * wobbleFrequency * 2f) * wobbleOffset;
        float yOffset = Mathf.Sin(Time.time * wobbleFrequency) * wobbleOffset;

        // Apply the wobble effect to the camera's local position
        cam.transform.localPosition = initialPosition + new Vector3(xOffset, yOffset, 0f);
        **/
    }
}
