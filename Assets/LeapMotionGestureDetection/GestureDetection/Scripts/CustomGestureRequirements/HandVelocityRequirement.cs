using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandVelocityRequirement : GestureRequirement
{
    public override string GetName()
    {
        return "Hand Velocity";
    }

    public override void SetupRequirements()
    {
        m_DataRequirements.SetFloatRequirement(true, 0, 5);
        m_DataRequirements.SetDirectionRequirement(true);
    }

    bool IsSwiping(ref EDirection a_swipeDirection, float InVelocity, EHand InHand)
    {
        DetectionManager.DetectionHand detectHand = DetectionManager.Get().GetHand(InHand);

        if(!detectHand.IsSet())
        {
            a_swipeDirection = EDirection.eOutwards;
            return false;
        }

        Vector3 velocity = detectHand.GetVelocity();

        velocity = Camera.main.transform.InverseTransformDirection(velocity);

        if (velocity.x >= InVelocity) //right
        {
            a_swipeDirection = EDirection.eRight;
            return true;
        }
        else if (velocity.x <= -InVelocity)//left
        {
            a_swipeDirection = EDirection.eLeft;
            return true;
        }
        else if (velocity.y >= InVelocity) //up
        {
            a_swipeDirection = EDirection.eUpwards;
            return true;
        }
        else if (velocity.y <= -InVelocity)//down
        {
            a_swipeDirection = EDirection.eDownwards;
            return true;
        }
        else if (velocity.z >= InVelocity) //forward
        {
            a_swipeDirection = EDirection.eOutwards;
            return true;
        }
        else if (velocity.z <= -InVelocity)//back
        {
            a_swipeDirection = EDirection.eInWards;
            return true;
        }

        return false;
    }

    public override bool JustifiesRequirement(EHand Hand, GestureRequirementData RequirementData)
    {
        EDirection Dir = EDirection.eOutwards;
        if(IsSwiping(ref Dir, RequirementData.m_FloatValue, Hand))
        {
            if(Dir == RequirementData.m_DirectionValue)
            {
                return true;
            }
        }

        return false;
    }
}
