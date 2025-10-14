using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class PatencyTestController : MonoBehaviour
{
    public Transform soro;
    public GameObject nextMaterial;
    public GameObject currentMaterial;
    private bool isOpen = false;
    private int count = 0;

    private void Seringa()
    {
        if (!isOpen)
        {
            soro.GetComponent<Animator>().Play("Refluir");
            soro.GetChild(1).GetChild(0).GetComponent<Image>().color = Color.red;
        }
        else
        {
            soro.GetComponent<Animator>().Play("Fluir");
            count++;
            if (count == 2)
            {
                currentMaterial.SetActive(false);
                ControllerUTI.Instance.SetCurrentMaterial(nextMaterial);
                ControllerUTI.Instance.ProcessRealizarTesteDePermeabilidade();
            }
        }
    }

    public void Control()
    {
        isOpen = !isOpen;
        Seringa();
    }
}
