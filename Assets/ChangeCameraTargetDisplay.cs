using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ChangeCameraTargetDisplay : MonoBehaviour
{
    public Camera leapMotionCamera;
    bool isDisplayChanged;

    // Start is called before the first frame update
    void Start()
    {
        //leapMotionCamera.targetDisplay = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C) && !isDisplayChanged) {
            leapMotionCamera.targetDisplay = 1;
            isDisplayChanged = true;
        }
            
    }
}
