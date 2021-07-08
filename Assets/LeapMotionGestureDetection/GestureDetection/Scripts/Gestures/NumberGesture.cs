using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberGesture : GestureBase
{
    public EHand m_Hand;

    [Range(0, 5)]
    public int m_Number;

    public override bool Detected()
    {
        if (DetectionManager.Get().IsHandSet(m_Hand))
        {
            int currentNumFingers = DetectionManager.Get().GetHand(m_Hand).NumberOfFingersExtended();

            return currentNumFingers == m_Number;
        }

        return false;
    }
}

