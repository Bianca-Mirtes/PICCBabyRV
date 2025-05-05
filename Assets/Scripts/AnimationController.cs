using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void ChangePoint1()
    {
        obj.transform.GetChild(0).GetComponent<Image>().color = Color.green;
        obj.transform.GetChild(1).GetComponent<Image>().color = Color.blue;
    }

    public void ChangePoint2()
    {
        obj.transform.GetChild(0).GetComponent<Image>().color = Color.blue;
        obj.transform.GetChild(1).GetComponent<Image>().color = Color.green;
    }
}