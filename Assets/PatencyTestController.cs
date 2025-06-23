using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class PatencyTestController : MonoBehaviour
{
    public GameObject blood;
    public GameObject soro;

    private bool isSoro = false;

    private void Faucet()
    {
        if (!isSoro)
        {
            blood.GetComponent<Animator>().Play("Refluir");
        }
        else
        {
            soro.GetComponent<Animator>().Play("SeringaClose");
        }
    }

    public void Control()
    {
        isSoro = !isSoro;
        Faucet();
    }
}
