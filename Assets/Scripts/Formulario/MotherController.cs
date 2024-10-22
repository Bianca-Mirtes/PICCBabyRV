using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherController : MonoBehaviour
{
    private bool signatureMom = false;

    public bool IsSigned()
    {
        return signatureMom;
    }

    public void SignForm()
    {
        signatureMom = true;
    }
}
