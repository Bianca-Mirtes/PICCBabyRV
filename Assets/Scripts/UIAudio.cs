using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIAudio : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public string clickAudioName = "click";
    public string hoverEnterAudioName = "hover_enter";
    public string hoverExitAudioName = "hover_exit";

    public void OnPointerClick(PointerEventData eventData)
    {

        if(clickAudioName == "")
        {
            clickAudioName = "click";
        }
        Debug.Log("click"); 
        AudioManager.instance.Play(clickAudioName);
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverEnterAudioName == "")
        {
            hoverEnterAudioName = "hover_enter";
        }
        AudioManager.instance.Play(hoverEnterAudioName);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverExitAudioName == "")
        {
            hoverExitAudioName = "hover_exit";
        }
        AudioManager.instance.Play(hoverExitAudioName);
        
    }
}
