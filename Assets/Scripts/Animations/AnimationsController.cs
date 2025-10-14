using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsController : MonoBehaviour
{
    private Animator ani;
    private bool isOpen = true;

    void Start()
    {
        ani = GetComponent<Animator>();
    }

    private void StartAnimation()
    {
        if (!isOpen)
        {
            ani.Play("Open");
        }
        else
        {
            ani.Play("Close");
        }    
    }

    public void Control()
    {
        isOpen = !isOpen;
        StartAnimation();
    }
}
