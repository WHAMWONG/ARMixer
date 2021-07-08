using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandFacingGesture : GestureBase
{
    public EHand m_Hand;
    public EHandAxis m_HandAxis;
    public EDirection m_Direction;

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

    EDirection GetClosestDirection(ref bool a_bDetected)
    {
        DetectionManager.DetectionHand detectionHand = DetectionManager.Get().GetHand(m_Hand);

        if(!detectionHand.IsSet())
        {
            a_bDetected = false;
            return EDirection.eDownwards;
        }

        Vector3 handDirection = detectionHand.GetHandAxis(m_HandAxis);

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

    public override bool Detected()
    {
        bool bFound = false;
        EDirection currentDirection = GetClosestDirection(ref bFound);

        if (bFound)
        {
            return currentDirection == m_Direction;
        }

        return false;
    }
}
