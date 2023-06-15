using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BGM_Manager : MonoBehaviour
{
    public static BGM_Manager instance;

    [SerializeField] private float volume;
    [SerializeField] private float fadein_dur;

    private AudioSource src;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        src = GetComponent<AudioSource>();
    }

    private IEnumerator fadeInBGM(float duration)
    {
        float startTime = Time.time;
        float endTime = startTime + duration;

        float startVol = 0f;
        float endVol = volume;
        
        while (Time.time < endTime)
        {
            float normalizedTime = (Time.time - startTime) / duration;
            src.volume = Mathf.Lerp(startVol, endVol, normalizedTime);

            yield return null;
        }
    }

    private IEnumerator fadeOutBGM(float duration)
    {
        float startTime = Time.time;
        float endTime = startTime + duration;

        float startVol = volume;
        float endVol = 0f;

        while (Time.time < endTime)
        {
            float normalizedTime = (Time.time - startTime) / duration;
            src.volume = Mathf.Lerp(startVol, endVol, normalizedTime);

            yield return null;
        }
    }

    public void playBGM()
    {
        if (!src.isPlaying)
        {
            src.volume = 0;
            src.Play();
            StartCoroutine(fadeInBGM(fadein_dur)); 
        }

    }

    public void playBGM(AudioClip clip)
    {
        if (!src.isPlaying)
        {
            src.clip = clip;
            src.volume = 0;
            src.Play();
            StartCoroutine(fadeInBGM(fadein_dur));
        }

    }

    public void stopBGM()
    {
        if (src.isPlaying)
        {
            StartCoroutine(fadeOutBGM(fadein_dur));
            src.Stop();
        }
    }

    public void setBGM(AudioClip clip)
    {
        src.clip = clip;
    }
}
