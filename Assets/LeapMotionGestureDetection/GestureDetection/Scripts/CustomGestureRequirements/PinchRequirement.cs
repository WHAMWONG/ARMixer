using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchRequirement : GestureRequirement
{
    public override string GetName()
    {
        return "Pinch";
    }

    public override void SetupRequirements()
    {
    }

    public override bool JustifiesRequirement(EHand Hand, GestureRequirementData RequirementData)
    {
        if (DetectionManager.Get().IsHandSet(Hand))
        {
            return DetectionManager.Get().GetHand(Hand).IsPinching();
        }

        return false;
    }
}
