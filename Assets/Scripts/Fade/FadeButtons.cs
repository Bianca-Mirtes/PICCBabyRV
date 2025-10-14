using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FadeButtons : MonoBehaviour
{

    private bool active = false;

    private CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }
    void Update()
    {
        if(active)
        {
            if(canvasGroup.alpha != 1)
            {
                canvasGroup.alpha += 0.1f;
            }
        } else
        {
            if (canvasGroup.alpha != 0)
            {
                canvasGroup.alpha -= 0.1f;
            }
            else
            {
                 gameObject.SetActive(false);
            }
        }
    }

    public void FadeInButton()
    {
        canvasGroup.alpha = 0;
        active = true;
        gameObject.SetActive(true);
    }

    public void FadeOutButton()
    {
        active = false;
    }
}
