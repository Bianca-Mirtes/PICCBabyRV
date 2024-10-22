using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class SoundEventController : MonoBehaviour
{
    private bool isClicked = false;
    [Header("Audio Configs")]
    public string audioOnSelect;
    public string audioOnUnselect;
    public bool fade = false;
    public float fadeInTime = 0;
    public float fadeOutTime = 0;

    private void StartAudio()
    {
        if (fade)
        {
                if (isClicked)
                {
                StopAllCoroutines();
                    StartCoroutine(AudioManager.instance.FadeIn(audioOnSelect, fadeInTime));

                }
                else
                {
                    StopAllCoroutines();
                    StartCoroutine(AudioManager.instance.FadeOut(audioOnSelect, fadeOutTime));
                }
            
        } else
        {
            if (audioOnUnselect != null)
            {
                if (isClicked)
                {
                    AudioManager.instance.Play(audioOnSelect);
                    
                }
                else
                {
                    AudioManager.instance.Play(audioOnUnselect);
                }
            }
            else
            {
                AudioManager.instance.Play(audioOnSelect);
            }
        }
    }

    public void Control()
    {
        isClicked = !isClicked;
        StartAudio();
    }
}
