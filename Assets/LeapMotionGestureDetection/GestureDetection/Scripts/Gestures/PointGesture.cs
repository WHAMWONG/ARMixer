using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointGesture : GestureBase
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
        DetectionManager.DetectionHand specifiedHand = DetectionManager.Get().GetHand(m_Hand);

        if (!DetectionManager.Get().IsHandSet(m_Hand))
        {
            return false;
        }

        for (int i = 0; i < 5; i++)
        {
            EFinger type = EFinger.eThumb + i;

            if (type != EFinger.eIndex)
            {
                if (specifiedHand.GetFinger(type).IsExtended())
                {
                    return false;
                }
            }
        }

        bool bIndexExtended = specifiedHand.GetFinger(EFinger.eIndex).IsExtended();

        return bIndexExtended;
    }
}
