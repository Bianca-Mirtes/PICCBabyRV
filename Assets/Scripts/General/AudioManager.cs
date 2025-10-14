using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0,1)]
    public float volume = 1;
    [Range(-3,3)]
    public float pitch = 1;
    public bool loop = false;
    public AudioSource source;

    public Sound()
    {
        volume = 1;
        pitch = 1;
        loop = false;
    }
}

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    //AudioManager

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            if(!s.source)
                s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }

        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        s.source.Stop();
    }

    public IEnumerator FadeOut(string name, float FadeTime)
    {
        
        Sound s = Array.Find(sounds, sound => sound.name == name);

        AudioSource audioSource = s.source;

        float startVolume = s.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public IEnumerator FadeIn(string name, float FadeTime)
    {

        Sound s = Array.Find(sounds, sound => sound.name == name);

        AudioSource audioSource = s.source;

        float finalVolume = audioSource.volume;

        audioSource.volume = 0;

        audioSource.Play();

        while (audioSource.volume < finalVolume)
        {
            audioSource.volume += finalVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.volume = finalVolume;
    }
}