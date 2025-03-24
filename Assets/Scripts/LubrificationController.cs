using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LubrificationController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == 8)
        {
            transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
        }
    }
}
