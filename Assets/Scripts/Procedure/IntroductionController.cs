using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using UnityEngine.UI;

public class IntroductionController : MonoBehaviour
{
    [SerializeField] private Image point2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PincaAnatomica"))
        {
            GetComponent<Image>().color = Color.green;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PincaAnatomica") && point2.color == Color.green)
        {
            GetComponent<Image>().color = Color.blue;
            FindFirstObjectByType<CateterCentralController>().NextKeyframe();
        }
    }
}
