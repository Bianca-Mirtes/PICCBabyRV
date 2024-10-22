using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TransitionNarrationMom : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup group;

    [SerializeField]
    private float timeOfLegendaByWord;

    [SerializeField]
    private TextMeshProUGUI textToLegendas;

    [SerializeField]
    private List<string> legendas;

    private int positionOfLegendaNow = 0;

    [SerializeField]
    private float fadeDuration = 1f;

    private bool stopLegendas = false;



    public void ActiveLegendas()
    {
        StartCoroutine(StartLegendas());
    }


    void Update()
    {
        if (stopLegendas)
        {
            //Passar para a scena do jogo

        }
    }


    private void StopParticles()
    {
        ParticleSystem[] particleSystems = FindObjectsOfType<ParticleSystem>();
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Stop();
        }
    }

    private IEnumerator StartLegendas()
    {
        Debug.Log("Iniciou legendas");
        for (int i = 0; i < legendas.Count; i++)
        {
            positionOfLegendaNow = i;
            yield return StartCoroutine(FadeOutText());
            ProcessLegendas();
            yield return StartCoroutine(FadeInText());
            yield return new WaitForSeconds(timeOfLegendaByWord * CountWords(legendas[i]));
        }
    }

    private void ProcessLegendas()
    {
        textToLegendas.text = legendas[positionOfLegendaNow];
        if (positionOfLegendaNow == legendas.Count - 1)
            stopLegendas = true;
    }

    private IEnumerator FadeInText()
    {
        float elapsedTime = 0f;
        Color color = textToLegendas.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            textToLegendas.color = color;
            yield return null;
        }
    }

    private IEnumerator FadeOutText()
    {
        float elapsedTime = 0f;
        Color color = textToLegendas.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1f - elapsedTime / fadeDuration);
            textToLegendas.color = color;
            yield return null;
        }
    }

    static int CountWords(string str)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return 0;
        }

        string[] words = str.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        return words.Length;
    }
}
