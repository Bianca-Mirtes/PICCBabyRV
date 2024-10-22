using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoomyCollider : MonoBehaviour
{
    [SerializeField]
    private TransitionNarrationMom narration;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            narration.ActiveLegendas();
        }
    }
}
