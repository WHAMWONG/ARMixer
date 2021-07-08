using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandDirectionRequirement : GestureRequirement
{
    Dictionary<EDirection, Vector3> GetDirections()
    {
        Dictionary<EDirection, Vector3> DirectionMap = new Dictionary<EDirection, Vector3>();

        Vector3 right = Vector3.Cross(Vector3.up, Camera.main.transform.forward);
        Vector3 left = -right;
        Vector3 forward = Camera.main.transform.forward;
        Vector3 inward = -forward;

        DirectionMap.Add(EDirection.eUpwards, Vector3.up);
        DirectionMap.Add(EDirection.eDownwards, Vector3.down);
        DirectionMap.Add(EDirection.eLeft, left);
        DirectionMap.Add(EDirection.eRight, right);
        DirectionMap.Add(EDirection.eInWards, inward);
        DirectionMap.Add(EDirection.eOutwards, forward);

        return DirectionMap;
    }

    EDirection GetClosestDirection(ref bool a_bDetected, EHand InHand, EHandAxis InAxis)
    {
        DetectionManager.DetectionHand detectionHand = DetectionManager.Get().GetHand(InHand);

        if (!detectionHand.IsSet())
        {
            a_bDetected = false;
            return EDirection.eDownwards;
        }

        Vector3 handDirection = detectionHand.GetHandAxis(InAxis);

        float currentDistance = float.MaxValue;
        EDirection currentDir = EDirection.eUpwards;

        Dictionary<EDirection, Vector3> directionMap = GetDirections();

        foreach (EDirection dir in directionMap.Keys)
        {
            float newDistance = Vector3.Distance(handDirection, directionMap[dir]);

            if (newDistance < currentDistance)
            {
                currentDistance = newDistance;
                currentDir = dir;
                a_bDetected = true;
            }
        }

        return currentDir;
    }

    public override string GetName()
    {
        return "Hand Direction";
    }

    public override void SetupRequirements()
    {
        m_DataRequirements.SetDirectionRequirement(true);
        m_DataRequirements.SetHandAxisRequirement(true);
    }

    public override bool JustifiesRequirement(EHand Hand, GestureRequirementData RequirementData)
    {
        bool bDetected = false;
        EDirection Direction = GetClosestDirection(ref bDetected, Hand, RequirementData.m_HandAxisValue);
        if(bDetected)
        {
            if(RequirementData.m_DirectionValue == Direction)
            {
                return true;
            }
        }

        return false;
    }
}
