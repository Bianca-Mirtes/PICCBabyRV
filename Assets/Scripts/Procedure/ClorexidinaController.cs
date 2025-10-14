using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClorexidinaController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Gaze"))
        {
            transform.GetChild(1).GetChild(2).GetComponent<ParticleSystem>().Play();
            Invoke("StartMinigame", 2f);
        }
    }

    private void StartMinigame() {
        transform.GetChild(1).GetChild(2).GetComponent<ParticleSystem>().Stop();
        FindFirstObjectByType<ControllerUTI>().SetMinigameAntissepsia();
    }
}
