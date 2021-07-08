using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureRequirementStatics
{
    static string[] Requirements = new string[]
    {
        "ThumbExtended"
    };

    public static string[] GetListOfRequirements()
    {
        return Requirements;
    }

    public static bool JustifiesRequirement(EHand Hand, string InRequirement)
    {
        if(InRequirement == "ThumbExtended")
        {
            return ThumbExtended(Hand);
        }

        return false;
    }

    static bool ThumbExtended(EHand Hand)
    {
        DetectionManager.DetectionHand CurrentHand = DetectionManager.sInstance.GetHand(Hand);
        if (CurrentHand.IsSet())
        {
            return CurrentHand.GetFinger(EFinger.eIndex).IsExtended();
        }

        return false;
    }
}
