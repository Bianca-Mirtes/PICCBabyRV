using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public GameObject obj;
    public string animationName;

    public void EnableObj()
    {
        gameObject.SetActive(false);
        obj.SetActive(true);
        Invoke("DisableObj", 1.5f);
    }

    private void DisableObj()
    {
        gameObject.SetActive(true);
        obj.SetActive(false);
        GetComponent<Animator>().Play(animationName);
    }
}