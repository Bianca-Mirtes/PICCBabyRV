using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Transform menu;
    private bool controlMenu = false;
    private bool changedStatus = false;
    private List<InputDevice> devices = new List<InputDevice>();

    private void Start()
    {
        // Initialize the menu
        if (menu != null)
        {
            menu.GetChild(2).GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene(0));
            menu.GetChild(3).GetComponent<Button>().onClick.AddListener(DesactiveMenu);
        }
    }

    private void DesactiveMenu()
    {
        changedStatus = !changedStatus;
        controlMenu = !controlMenu;
        menu.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        InputDeviceCharacteristics leftHandCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(leftHandCharacteristics, devices);
        //devices[0].TryGetFeatureValue(CommonUsages.secondaryButton, out bool Ybutton);
        if (/*Ybutton*/Input.GetKeyDown(KeyCode.M)) // Y button pressed
        {
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                GameObject menu = GameObject.FindWithTag("MainMenu").transform.GetChild(0).gameObject;
                if (menu != null)
                {
                    if (!changedStatus)
                    {
                        controlMenu = !controlMenu;
                        menu.SetActive(controlMenu);
                        changedStatus = true;
                    }
                }
            }
        }
    }
}
