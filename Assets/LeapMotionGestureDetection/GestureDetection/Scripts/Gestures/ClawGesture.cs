using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClawGesture : GestureBase
{
    public EHand m_Hand;
    [Range(0.1f,0.3f)]
    public float m_Strength = 0.14f;

    public override bool Detected()
    {
        if(DetectionManager.Get().IsHandSet(m_Hand))
        {
            return DetectionManager.Get().GetHand(m_Hand).IsClosed(m_Strength);
        }
        return false;
    }
}
