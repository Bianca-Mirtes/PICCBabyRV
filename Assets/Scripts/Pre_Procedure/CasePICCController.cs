using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CasePICCController : MonoBehaviour
{
    private Animator ani;
    [SerializeField] private bool isFinishCase = false;
    [SerializeField] private float count = 5f;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    public void setIsFinishCase()
    {
        isFinishCase = true;
    }

    private void Update()
    {
        if (gameObject.name.Equals("CanvasResult"))
        {
            if (isFinishCase)
            {
                if (count > 0f)
                    count -= Time.deltaTime;
                else
                {
                    FadeOut();
                    isFinishCase = false;
                    count = 5f;
                }
            }
        }
    }

    public void FadeIn()
    {
        ani.SetBool("isFadeIn", true);
        ani.SetBool("isFadeOut", false);
    }

    public void FadeOut()
    {
        ani.SetBool("isFadeIn", false);
        ani.SetBool("isFadeOut", true);
    }
}
