using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoapDishController : MonoBehaviour
{
    private bool handInsideCollisionRay = false;
    private bool triggerSoapDish = false;
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag.Equals("LeftHand") || other.gameObject.tag.Equals("RightHand"))
        {
            Debug.Log("Mao embaixo da saboneteira");
            handInsideCollisionRay = true;
        }
        else
        {
            handInsideCollisionRay = false;
        }
    }

    public void SetTriggerSoapDish(bool value)
    {
        if (handInsideCollisionRay)
        {
            Debug.Log("Trigger da saboneteira pressionado");
           triggerSoapDish = value;
        }
    }

    void Update()
    {
        if (handInsideCollisionRay && triggerSoapDish)
        {
            Debug.Log("Mão com o sabão!!!");
            FindObjectOfType<HandController>().SetHandsWithSoap(true);
        }
    }
}
