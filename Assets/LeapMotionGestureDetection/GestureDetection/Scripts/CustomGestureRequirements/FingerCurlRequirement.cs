using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerCurlRequirement : GestureRequirement
{
    public override bool JustifiesRequirement(EHand Hand, GestureRequirementData RequirementData)
    {
        DetectionManager.DetectionHand SelectedHand = DetectionManager.Get().GetHand(Hand);
        if (SelectedHand.IsSet())
        {
            Vector3 FingerDirection = SelectedHand.GetFinger(RequirementData.m_FingerValue).GetFingerDirection();

            DetectionManager.DetectionFinger SelectedFinger = SelectedHand.GetFinger(RequirementData.m_FingerValue);

            Vector3 Bone1Dir = SelectedFinger.GetBoneDirection(ESpecificBone.eBone0);
            Vector3 Bone2Dir = SelectedFinger.GetBoneDirection(ESpecificBone.eBone1);
            Vector3 Bone3Dir = SelectedFinger.GetBoneDirection(ESpecificBone.eBone2);

            float boneDot;

            if(RequirementData.m_FingerValue == EFinger.eThumb)
            {
                boneDot = Mathf.Clamp(Vector3.Dot(Bone2Dir, Bone3Dir), 0.0f, 1.0f);
            }
            else
            {
                boneDot = Mathf.Clamp(Vector3.Dot(Bone1Dir, Bone3Dir), 0.0f, 1.0f);
            }

            float percentage = Mathf.Lerp(1.0f, 0.0f, boneDot);

            if(percentage > RequirementData.m_FloatValue)
            {
                return true;
            }
        }

        return false;
    }

    public override void SetupRequirements()
    {
        m_DataRequirements.SetFingerRequirement(true);
        m_DataRequirements.SetFloatRequirement(true);
    }

    public override string GetName()
    {
        return "Finger Curl Percentage";
    }
}
