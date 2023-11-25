using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Threading;

public class SoundManager : MonoBehaviour
{
    [Header("Volume Slider")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    public static float bgmVolume;
    public static float sfxVolume;

    [Header("Load Status Mute")]
    public GameObject onMute;
    public GameObject offMute;
    public GameObject onPlay, offPlay;

    private const string bgmVolumeKey = "BGMVolume";
    private const string sfxVolumeKey = "SFXVolume";

    [Header("Category")]
    [SerializeField] private AudioMixerGroup bgmMixerGroup;
    [SerializeField] private AudioMixerGroup sfxMixerGroup;

    [Header("Sound List")]
    [SerializeField] private Sound[] sounds;

    public static SoundManager Instance;
    public bool isMuted = false;

    private void Awake()
    {
        Instance = this;

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audioClip;
            s.source.loop = s.isLoop;
            s.source.volume = s.volume;

            switch (s.audioType)
            {
                case Sound.AudioTypes.SFX:
                    s.source.outputAudioMixerGroup = sfxMixerGroup;
                    break;

                case Sound.AudioTypes.BGM:
                    s.source.outputAudioMixerGroup = bgmMixerGroup;
                    break;
            }

            if (s.playOnAwake)
                s.source.Play();
        }
    }

    private void Start()
    {
        LoadSoundSettings();

        if (isMuted == true)
        {
            onMute.SetActive(true);
            offMute.SetActive(true);
            onPlay.SetActive(false); 
            offPlay.SetActive(false);

            bgmSlider.interactable = false;
            sfxSlider.interactable = false;
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

    public void TurnOn()
    {
        isMuted = false;

        AudioListener.volume = isMuted ? 0 : 1;

        bgmSlider.interactable = true;
        sfxSlider.interactable = true;

        bgmSlider.value = 100;
        sfxSlider.value = 100;
        SaveSoundSettings();
    }

    public void TurnOff()
    {
        isMuted = true;

        AudioListener.volume = isMuted ? 0 : 1;

        bgmSlider.interactable = false;
        sfxSlider.interactable = false;

        bgmSlider.value = 0;
        sfxSlider.value = 0;
        SaveSoundSettings();
    }

    public void UpdateMixerVolume()
    {
        bgmMixerGroup.audioMixer.SetFloat("BGM", Mathf.Log10(bgmVolume) * 20);
        sfxMixerGroup.audioMixer.SetFloat("SFX", Mathf.Log10(sfxVolume) * 20);
        SaveSoundSettings();
    }

    public void BgmSliderValueChange(float value)
    {
        bgmVolume = value;
        Mathf.RoundToInt(value * 100);
        UpdateMixerVolume();
    }

    public void SfxSliderValueChange(float value)
    {
        sfxVolume = value;
        Mathf.RoundToInt(value * 100);
        UpdateMixerVolume();
    }

    public void LoadSoundSettings()
    {
        isMuted = PlayerPrefs.GetInt("isMuted") == 1;

        if (PlayerPrefs.HasKey(bgmVolumeKey))
        {
            bgmVolume = PlayerPrefs.GetFloat(bgmVolumeKey);
            bgmSlider.value = bgmVolume;
        }
        else
        {
            bgmVolume = 1f;
            bgmSlider.value = bgmVolume;
        }

        if (PlayerPrefs.HasKey(sfxVolumeKey))
        {
            sfxVolume = PlayerPrefs.GetFloat(sfxVolumeKey);
            sfxSlider.value = sfxVolume;
        }
        else
        {
            sfxVolume = 1f;
            sfxSlider.value = sfxVolume;
        }
    }
    public void SaveSoundSettings()
    {
        PlayerPrefs.SetInt("isMuted", isMuted ? 1 : 0);

        PlayerPrefs.SetFloat(bgmVolumeKey, bgmVolume);
        PlayerPrefs.SetFloat(sfxVolumeKey, sfxVolume);
        PlayerPrefs.Save();
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
