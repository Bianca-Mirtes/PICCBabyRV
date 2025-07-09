using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class PatencyTestController : MonoBehaviour
{
    public Transform soro;

    private bool isOpen = false;

    private int count = 0;

    // Vector3(0.022,0.183,0.023)
    // Vector3(84.687,181.337,190.376)


    private void Seringa()
    {
        if (!isOpen)
        {
            soro.GetComponent<Animator>().Play("Refluir");
            soro.GetChild(1).GetChild(0).GetComponent<Image>().color = Color.red;
            count++;
            if (count == 2)
            {
                StateController.Instance.SetState(State.RealizarTesteDePermeabilidade);
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
