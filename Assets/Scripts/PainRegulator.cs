using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PainRegulatorController : MonoBehaviour
{
    public Color good;
    public Color medium;
    public Color bad;
    public AudioSource crySound;
    public GameObject socket;
    private float maxPain = 1f;
    private float painIncreasePerMinute = 0.05f;

    private float currentPain = 0f;

    private Slider slider;
    private Image fillArea;
    // Start is called before the first frame update
    void Start()
    {
        slider = transform.GetChild(0).GetChild(0).GetComponent<Slider>();
        fillArea = transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>();
        slider.value = currentPain;
        maxPain = slider.maxValue;
        socket.SetActive(false);
    }

    public void StartPainRegulation()
    {
        socket.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(PainProgression());
    }

    public void ApplyFentanil()
    {
        currentPain = 0;
        slider.value = currentPain;
    }

    IEnumerator PainProgression()
    {
        while (currentPain < maxPain)
        {
            yield return new WaitForSeconds(20f); // 1 minuto

            currentPain += painIncreasePerMinute;
            currentPain = Mathf.Clamp(currentPain, 0, maxPain);

            slider.value = currentPain;
            UpdatePainColor();
        }
    }

    private void UpdatePainColor()
    {
        if (slider.value < 0.5f)
        {
            fillArea.color = good;
            if (crySound.isPlaying) crySound.Stop();
        }
        else if (slider.value < 0.8f)
        {
            fillArea.color = medium;
            crySound.volume = 0.3f;
            if (!crySound.isPlaying)
                crySound.Play();
        }
        else
        {
            fillArea.color = bad;
            crySound.volume = 0.8f;
            if (!crySound.isPlaying)
                crySound.Play();
        }
    }
}
