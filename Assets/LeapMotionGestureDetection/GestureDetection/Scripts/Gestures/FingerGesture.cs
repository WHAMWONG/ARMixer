using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerGesture : GestureBase
{
    public EHand m_Hand;
    public EFinger m_Finger;
    public bool m_OthersClosed;

    DetectionManager.DetectionHand m_DetectHand;

    void Start()
    {
    }

    void Update()
    {

    }

    public override bool Detected()
    {
        if(!DetectionManager.Get().IsHandSet(m_Hand))
        {
            return false;
        }

        m_DetectHand = DetectionManager.Get().GetHand(m_Hand);

        bool bExtended = false;

        for (int i = 0; i < (int)EFinger.ePinky; i++)
        {
            EFinger finger = EFinger.eThumb + i;

            if(m_DetectHand.GetFinger(finger).IsExtended())
            {
                if(finger == m_Finger)
                {
                    bExtended = true;
                }
                else
                {
                    if(m_OthersClosed)
                    {
                        return false;
                    }
                }
            }
        }

        return bExtended;
    }
}
