using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGestureConnector : GestureBase
{
    public CustomGesture m_CustomGesture;

    

    public override bool Detected()
    {
        if(!m_CustomGesture)
        {
            return false;
        }

        if(JustifiesHand(EHand.eLeftHand) && JustifiesHand(EHand.eRightHand))
        {
            return true;
        }

        return false;
    }

    bool JustifiesHand(EHand Hand)
    {
        if(!m_CustomGesture)
        {
            return false;
        }


        List<GestureRequirementData> GestureRequirements = m_CustomGesture.GetHandRequirements(Hand);

        foreach (GestureRequirementData require in GestureRequirements)
        {
            if(require.m_Requirement.m_DataRequirements.GetRequiresFinger())
            {
                if(require.m_FingerValue == EFinger.eUnknown)
                {
                    Debug.Log("[GestureDetection] Object: " + gameObject.name + ": \"" + require.m_Requirement.GetType().ToString() + "\" Cannot use a Requirement with an unknown finger");
                    return false;
                }
            }
            if (!require.m_Requirement.JustifiesRequirement(Hand, require))
            {
                return false;
            }
        }

        return true;
    }
}
