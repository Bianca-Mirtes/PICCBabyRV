using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MotherController : MonoBehaviour
{
    public bool signatureMom = false;
    private bool stepStarted = false;

    private void Update()
    {
        if(signatureMom)
        {
            Transform canvaForm = GameObject.FindWithTag("Form").transform.GetChild(1);
            if (canvaForm != null)
            {
                canvaForm.Find("Signature").GetComponent<TextMeshProUGUI>().text = canvaForm.Find("Responsavel").GetComponent<TextMeshProUGUI>().text;
            }
            signatureMom = false;
        }
    }

    public void Options()
    {
        if (!stepStarted)
        {
            transform.GetChild(1).GetComponent<DialogueTrigger>().Trigger();
            Invoke("Enable", 10f);
            stepStarted = true;
        }
    }

    private void Enable()
    {
        transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
    }

    public void ResetStep()
    {
        transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
        Invoke("ResetDialogue", 10f);
    }
    private void ResetDialogue()
    {
        Transform player = GameObject.FindWithTag("Player").transform;
        player.position = new Vector3(-1.5f, player.position.y, -6.7f);
    }

    public void SignForm()
    {
        signatureMom = true;
    }
}
