using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AntissepsiaController : MonoBehaviour
{
    [SerializeField] private Image point2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Gaze"))
        {
            GetComponent<Image>().color = Color.green;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Gaze") && point2.color == Color.green)
        {
            GetComponent<Image>().color = Color.blue;
            transform.parent.parent.GetChild(0).GetComponent<Slider>().value += 0.05f;
        }
    }
}
