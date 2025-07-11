using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class PatencyTestController : MonoBehaviour
{
    public Transform soro;
    public GameObject nextMaterial;
    private bool isOpen = false;
    private int count = 0;

    private void Seringa()
    {
        if (!isOpen)
        {
            soro.GetComponent<Animator>().Play("Refluir");
            soro.GetChild(1).GetChild(0).GetComponent<Image>().color = Color.red;
            count++;
            if (count == 2)
            {
                ControllerUTI.Instance.GetCurrentMaterial(nextMaterial);
                ControllerUTI.Instance.ProcessRealizarTesteDePermeabilidade();
            }
        }
        else
        {
            soro.GetComponent<Animator>().Play("Fluir");
            soro.GetChild(1).GetChild(0).GetComponent<Image>().color = Color.cyan;
        }
    }

    public void Control()
    {
        isOpen = !isOpen;
        Seringa();
    }
}
