using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PunctureController : MonoBehaviour
{
    // Vector3(-1.17869997,-0.554499984,1.10880005) pos
    // Vector3(12.445899,298.497253,356.197784) rotation
    public void SetPositionCateter()
    {
        Transform cateter = transform.parent.parent.parent.GetChild(1);
        cateter.position = new Vector3(-1.2973f, -0.5998f, 1.1743f);
        cateter.eulerAngles = new Vector3(332.37f, 301.26f, 355.80f);
    }

    private bool boolean = false;
    private void OnTriggerEnter(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (other.gameObject.CompareTag("Correct"))
            {
                if (!boolean)
                {
                    transform.parent.GetComponent<Animator>().speed = 0;
                    transform.parent.parent.parent.GetChild(1).GetComponent<Animator>().Play("Introduzir");

                    Transform tabletInfo = GameObject.Find("TabletInfos").transform.GetChild(5);
                    tabletInfo.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Retire a bainha protetora: ";
                    tabletInfo.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Aponte o raio para a bainha protetora e pressione o gatilho do controle para retirá-la!";
                    GameObject.Find("TabletInfos").transform.GetChild(1).GetComponent<ParticleSystem>().Play();
                    tabletInfo.gameObject.SetActive(true);
                    boolean = true;
                }
            }
        }
    }
}
