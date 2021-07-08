using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BothHandsPinchingGesture_R : GestureBase
{
    public override bool Detected()
    {
        if (DetectionManager.Get().IsHandSet(EHand.eRightHand))
        {
           // bool bLeftHandPinching = DetectionManager.Get().GetHand(EHand.eLeftHand).IsPinching();

            bool bRightHandPinching = DetectionManager.Get().GetHand(EHand.eRightHand).IsPinching();
            if (Max.Initialize.isReverb)
            {
               // Max.Initialize.l.SetActive(bLeftHandPinching);
                Max.Initialize.r.SetActive(bRightHandPinching);
            }
            else
            {
                Max.Initialize.l.SetActive(false);
                Max.Initialize.r.SetActive(false);
            }
            return  bRightHandPinching && Max.Initialize.isReverb;
        }

        return false;
    }
}
