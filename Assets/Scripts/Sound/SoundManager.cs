using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;

    public static SoundManager Instance;
    private void Awake()
    {
        Instance = this;

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audioClip;
            s.source.loop = s.isLoop;
            s.source.volume = s.volume;

            if (s.playOnAwake)
                s.source.Play();
        }
    }

    public void Play(string clipname)
    {
        Sound s = Array.Find(sounds, dummySound => dummySound.clipName == clipname);
        if (s == null)
        {
            Debug.LogError("Sound: " + clipname + " doesn't exist!");
            return;
        }
        s.source.Play();
    }

    public void Stop(string clipname)
    {
        Sound s = Array.Find(sounds, dummySound => dummySound.clipName == clipname);
        if (s == null)
        {
            Debug.LogError("Sound: " + clipname + " doesn't exist!");
            return;
        }
        s.source.Stop();
    }

    public void Pause()
    {
        foreach (Sound s in sounds)
        {
            if (s.audioType == Sound.AudioTypes.BGM)
            {
                s.source.Pause();
            }
        }
    }
    public void Resume()
    {
        foreach (Sound s in sounds)
        {
            if (s.audioType == Sound.AudioTypes.BGM)
            {
                s.source.UnPause();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Instance.Stop("Stage1");
            Instance.Play("BossBGM");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instance.Stop("BossBGM");
            Instance.Play("Stage1");
        }
    }
}
