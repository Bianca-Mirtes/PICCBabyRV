using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSoundController : MonoBehaviour
{
    public void Control()
    {
        StartAudio();
        Debug.Log("pego");
    }

    private void StartAudio()
    {
            System.Random random = new();
            int start2 = random.Next(1, 4);
            AudioManager.instance.Play("pickup_" + start2.ToString());
        
        
    }
}
