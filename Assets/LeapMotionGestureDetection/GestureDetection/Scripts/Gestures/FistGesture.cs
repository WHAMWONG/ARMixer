using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistGesture : GestureBase
{
    public EHand m_Hand;

    [Range(0.0f,1.0f)]
    public float m_ClosedPercentage = 0.6f;

    public override bool Detected()
    {
        if(DetectionManager.Get().IsHandSet(m_Hand))
        {
            return DetectionManager.Get().GetHand(m_Hand).IsClosed(m_ClosedPercentage);
        }

        return false;
    }
}
