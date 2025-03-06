using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClorexidinaController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gaze"))
            GetComponent<Animator>().Play("PutClore");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Gaze"))
        {
            GetComponent<Animator>().Play("StopClore");
            FindFirstObjectByType<ControllerUTI>().SetMinigameAntissepsia();
        }
    }
    public void PutClorexidina()
    {
        transform.GetChild(1).GetChild(2).GetComponent<ParticleSystem>().Play();
    }

    public void StopPuttingCloredixina()
    {
        transform.GetChild(1).GetChild(2).GetComponent<ParticleSystem>().Stop();
    }
}
