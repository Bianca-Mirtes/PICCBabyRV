using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetMaterial : MonoBehaviour
{
    private Vector3 initialPos;
    private Transform parent;
    private void Start()
    {
        initialPos = transform.position;
        parent = transform.parent ?? GameObject.Find("CampoCirurgico").transform;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 9)
        {
            transform.parent = parent;
            transform.position = initialPos; //new Vector3(-1f, 0.15f, 0.9f);
        }
    }
}
