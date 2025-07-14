using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class PunctureController : MonoBehaviour
{
    private List<InputDevice> devices = new List<InputDevice>();
    public void SetPositionCateter()
    {
        UnityEngine.Transform cateter = transform.parent.parent.parent.GetChild(1);
        cateter.position = new Vector3(-1.2973f, -0.5998f, 1.1743f);
        cateter.eulerAngles = new Vector3(332.37f, 301.26f, 355.80f);
    }

    private bool boolean = false;
    private void OnTriggerEnter(Collider other)
    {
        InputDeviceCharacteristics leftHandCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(leftHandCharacteristics, devices);
        //devices[0].TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        if (/*triggerValue > 0.1f ||*/ Input.GetKeyDown(KeyCode.P))
        {
            if (other.gameObject.CompareTag("Correct"))
            {
                if (!boolean)
                {
                    transform.parent.GetComponent<Animator>().speed = 0;
                    transform.parent.parent.parent.GetChild(1).GetComponent<Animator>().Play("Introduzir");

                    Transform tabletInfo = GameObject.Find("TabletInfos").transform.GetChild(0);
                    tabletInfo.GetChild(7).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Retire a bainha protetora: ";
                    tabletInfo.GetChild(7).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Aponte o raio para a bainha protetora e pressione o gatilho do controle para retirá-la!";
                    GameObject.Find("TabletInfos").transform.GetChild(1).GetComponent<ParticleSystem>().Play();
                    tabletInfo.GetChild(7).GetChild(1).gameObject.SetActive(true);
                    tabletInfo.GetChild(7).GetChild(2).gameObject.SetActive(false);
                    transform.parent.parent.gameObject.SetActive(false);
                   boolean = true;
                }
            }
            else if(other.gameObject.CompareTag("Incorrect"))
            {
                transform.parent.GetComponent<Animator>().speed = 0;
                GameObject.FindWithTag("ResetCanva").transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
