using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    private CanvasGroup canvas;
    private bool fadeIn = false;
    private bool fadeOut = false;
    private GameObject objForDesactivation = null;

    private void Start()
    {
        canvas = gameObject.GetComponent<CanvasGroup>();
    }

    public void FadeOut()
    {
        fadeOut = true;
    }

    public void FadeWithTime(string type, float time)
    {
        if (type.Equals("Out"))
            Invoke("FadeOut", time);
        else
            Invoke("FadeIn", time);
    }

    public void FadeWithDesactivationOfObject(GameObject obj)
    {
        objForDesactivation = obj;
        FadeInForFadeOut(5f);
    }

    public void FadeInForFadeOut(float time)
    {
        FadeIn();
        Invoke("FadeOut", time);
    }

    public void FadeOutForFadeIn(float time)
    {
        FadeOut();
        Invoke("FadeIn", time);
    }

    private void Update()
    {
        if (fadeIn)
        {
            if (canvas.alpha < 1f)
                canvas.alpha += Time.deltaTime;
            else
                fadeIn = false;
        }
        if (fadeOut)
        {
            if (canvas.alpha > 0f)
                canvas.alpha -= Time.deltaTime;
            else
            {
                fadeOut = false;
                if(objForDesactivation != null)
                {
                    objForDesactivation.SetActive(false);
                    objForDesactivation = null;
                }
            }
        }
    }
    public void FadeIn()
    {
        fadeIn = true;
    }
}
