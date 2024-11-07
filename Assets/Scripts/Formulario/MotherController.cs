using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MotherController : MonoBehaviour
{
    private bool signatureMom = false;

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

    public void SignForm()
    {
        signatureMom = true;
    }
}
