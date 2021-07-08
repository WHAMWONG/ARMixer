using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerExtendedRequirement : GestureRequirement
{

    public override bool JustifiesRequirement(EHand Hand, GestureRequirementData RequirementData)
    {
        DetectionManager.DetectionHand SelectedHand = DetectionManager.Get().GetHand(Hand);
        if (SelectedHand.IsSet())
        {
            if (RequirementData.m_BoolValue == SelectedHand.GetFinger(RequirementData.m_FingerValue).IsExtended())
            {
                return true;
            }
        }

        return false;
    }

    public override void SetupRequirements()
    {
        m_DataRequirements.SetFingerRequirement(true);
        m_DataRequirements.SetBoolRequirement(true, "IsExtended");
    }

    public override string GetName()
    {
        return "Finger Extended";
    }
}
