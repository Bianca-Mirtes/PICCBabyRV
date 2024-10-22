using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabController : MonoBehaviour
{
    private XRGrabInteractable tool;
    public XRSocketInteractor socket;

    // Start is called before the first frame update
    void Start()
    {
        tool = GetComponent<XRGrabInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        //ResetPositionTool();
    }

    private void ResetPositionTool()
    {
        Rigidbody rb;
        if (tool != null)
        {
            rb = tool.GetComponent<Rigidbody>();
            if (rb != null && rb.useGravity)
            {
                socket.allowHover = true;
                tool.transform.position = socket.transform.position;
            }
        }
    }

    public void SetTool(XRGrabInteractable tool)
    {
        this.tool = tool;
        tool.transform.Rotate(Vector3.right, -30f);
    }

    public void SetSocket(XRSocketInteractor socket)
    {
        this.socket = socket;
    }

    public void resetTool()
    {
        if(tool != null)
            tool.transform.Rotate(Vector3.right, 30f);
        tool = null;
    }

    public bool isGrab(XRGrabInteractable tool)
    {
        if (this.tool == null)
            return false; 
        return this.tool.Equals(tool);
    }

    public bool isToolNull()
    {
        return this.tool == null;
    }
}
