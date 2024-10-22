using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    private bool isStart = false;
    private bool righHandInside = false;
    private bool leftHandInside = false;
    private bool handsWithSoap = false;
    public void SetHandsWithSoap(bool value)
    {
        handsWithSoap = value;
    }

    public bool GetHandsWithSoap() {  return handsWithSoap;  }

    private void BubbleSoapPlay(ParticleSystem bubbleSoap)
    {
        bubbleSoap.Play();
    }

    public void BubbleSoapStop(ParticleSystem bubbleSoap)
    {
        bubbleSoap.Stop();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("RightHand"))
        {
            righHandInside = true;
            Debug.Log("Direita entrou!!!");
        }
        if (other.gameObject.tag.Equals("LeftHand"))
        {
            leftHandInside = true;
            Debug.Log("Esquerda entrou!!");
        }

        if(righHandInside && leftHandInside)
        {
            Debug.Log("As duas entraram!!");
            if (handsWithSoap)
            {
                GameObject[] systemsBubble = GameObject.FindGameObjectsWithTag("BubbleSoap");
                if (!isStart)
                {
                    foreach(var system in systemsBubble)
                        BubbleSoapPlay(system.GetComponent<ParticleSystem>());
                    isStart = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("RightHand"))
        {
            righHandInside = false;
            Debug.Log("Direita saiu!!!");
        }
        if (other.gameObject.tag.Equals("LeftHand"))
        {
            leftHandInside = false;
            Debug.Log("Esquerda saiu!!");
        }

        if(!righHandInside && !leftHandInside)
        {
            if (handsWithSoap)
                if (isStart)
                    isStart = false;
        }
    }
}
