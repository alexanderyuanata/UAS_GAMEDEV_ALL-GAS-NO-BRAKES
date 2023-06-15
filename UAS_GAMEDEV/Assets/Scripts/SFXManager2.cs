using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager2 : MonoBehaviour
{
    public static SFXManager2 instance;
    [SerializeField] private AudioClip[] clips;

    private AudioSource self;


    private void Awake()
    {
        instance = this;
        self = GetComponent<AudioSource>();
    }

    public void playClip(AudioClip clip)
    {
        self.clip = clip;
        if (self.isPlaying) self.Stop();
        self.Play();
    }

    public void stopClip()
    {
        self.Stop();
    }

}
