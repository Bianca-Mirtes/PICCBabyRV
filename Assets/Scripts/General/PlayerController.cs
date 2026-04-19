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
}
