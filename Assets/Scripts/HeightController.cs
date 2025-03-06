using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightController : MonoBehaviour
{
    private float height = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Transform player = GameObject.FindWithTag("Player").transform;
        Transform initialPos = GameObject.Find("InitialPosition").transform;

        player.position = initialPos.position;
        player.rotation = initialPos.rotation;

        height = transform.position.y;
    }

    public void NewHeight(float value)
    {
        height = value;
    }

    // Update is called once per frame
    void Update()
    {
       FixedHeight();
    }

    public void FixedHeight()
    {
        transform.position = new Vector3(transform.position.x, height, transform.position.z);
    }
}
