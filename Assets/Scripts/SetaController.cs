using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetaController : MonoBehaviour
{
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
