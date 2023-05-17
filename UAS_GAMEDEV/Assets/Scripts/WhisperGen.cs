using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WhisperGen : MonoBehaviour
{
    [SerializeField] private float max_timer;
    [SerializeField] private int whisper_chance;
    public AudioClip[] whisper_clips;

    private AudioSource src;
    private float timer;

    private AudioClip getRandWhisper()
    {
        return whisper_clips[Random.Range(0, whisper_clips.Length-1)];
    }

    private void Start()
    {
        src = GetComponent<AudioSource>();
        timer = max_timer;
    }

    void Update()
    {
        if (timer <= 0)
        {
            if (Random.Range(0, 100) < whisper_chance)
            {
                src.clip = getRandWhisper();
                src.Play();
            }
            timer = max_timer;
        }
        else
        {
            timer -= Time.deltaTime;
        }

    }
}
