using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThumbGesture : GestureBase
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
        DetectionManager.DetectionHand detectHand = DetectionManager.Get().GetHand(m_Hand);

        if(detectHand.IsSet())
        {
            if(detectHand.GetFinger(EFinger.eThumb).IsExtended())
            {
                for (int i = (int)EFinger.eThumb; i <= (int)EFinger.ePinky; i++)
                {
                    EFinger finger = EFinger.eThumb + i;

                    if(finger != EFinger.eThumb && finger != EFinger.eUnknown)
                    {
                        if(detectHand.GetFinger(finger).IsExtended())
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }

        return false;
    }
}
