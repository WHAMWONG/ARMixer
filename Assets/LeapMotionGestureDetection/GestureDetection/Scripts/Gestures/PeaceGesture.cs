using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaceGesture : GestureBase
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

        if( DetectionManager.Get().IsHandSet( m_Hand ) )
        {
            DetectionManager.DetectionHand hand = DetectionManager.Get().GetHand(m_Hand);

            return (hand.IsFingerExtended(EFinger.eIndex)
                &&  hand.IsFingerExtended(EFinger.eMiddle)
                && !hand.IsFingerExtended(EFinger.eThumb)
                && !hand.IsFingerExtended(EFinger.eRing)
                && !hand.IsFingerExtended(EFinger.ePinky));
        }

        return false;
    }
}
