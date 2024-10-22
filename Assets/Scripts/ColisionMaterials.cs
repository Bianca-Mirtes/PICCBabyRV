using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionMaterials : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject material;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colocando como filho");
        //material.transform.parent = collision.transform;
    }
}
