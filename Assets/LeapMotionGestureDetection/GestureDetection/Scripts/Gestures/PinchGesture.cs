using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchGesture : GestureBase
{

    public EHand m_Hand;

    void Start()
    {

    }

    void Update()
    {

    }

    public override bool Detected()
    {
        if(DetectionManager.Get().IsHandSet(m_Hand))
        {
            return DetectionManager.Get().GetHand(m_Hand).IsPinching();
        }

        return false;
    }
}
