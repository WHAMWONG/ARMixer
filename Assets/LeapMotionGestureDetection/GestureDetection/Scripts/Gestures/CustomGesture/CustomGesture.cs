using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public struct GestureRequirementData
{
    [SerializeField]
    public GestureRequirement m_Requirement;

    public float m_FloatValue;
    public bool m_BoolValue;
    public EFinger m_FingerValue;
    public EDirection m_DirectionValue;
    public EHandAxis m_HandAxisValue;

    public GestureRequirementData(GestureRequirement a_Requirement)
    {
        m_Requirement = a_Requirement;
        m_FloatValue = 0.0f;
        m_BoolValue = false;
        m_FingerValue = EFinger.eThumb;
        m_DirectionValue = EDirection.eOutwards;
        m_HandAxisValue = EHandAxis.ePalmDirection;
    }
}

[RequireComponent(typeof(CustomGestureConnector))]
public class CustomGesture : MonoBehaviour
{
    [SerializeField]
    List<GestureRequirementData> RightHandRequirements = new List<GestureRequirementData>();

    [SerializeField]
    List<GestureRequirementData> LeftHandRequirements = new List<GestureRequirementData>();

    void Reset()
    {
        CustomGestureConnector connector = gameObject.GetComponent<CustomGestureConnector>();
        if (connector)
        {
            connector.m_CustomGesture = this;
        }
    }

    public GestureRequirement[] GetListOfAllRequirements()
    {
        GestureRequirement[] GestureRequirements = Resources.LoadAll<GestureRequirement>("CustomGestureRequirements");

        return GestureRequirements;
    }

    public void AddRequirement(EHand Hand, GestureRequirement Requirement)
    {
        if(Hand == EHand.eRightHand)
        {
            RightHandRequirements.Add(new GestureRequirementData(Requirement));
        }
        else
        {
            LeftHandRequirements.Add(new GestureRequirementData(Requirement));
        }
    }

    public void RemoveRequirement(EHand Hand, GestureRequirement Requirement)
    {
        if (Hand == EHand.eRightHand)
        {
            foreach (GestureRequirementData item in RightHandRequirements)
            {
                if (item.m_Requirement == Requirement)
                {
                    RightHandRequirements.Remove(item);
                }
            }
        }
        else
        {
            foreach (GestureRequirementData item in LeftHandRequirements)
            {
                if(item.m_Requirement == Requirement)
                {
                    LeftHandRequirements.Remove(item);
                    return;
                }
            }
        }

    }

    public List<GestureRequirementData> GetHandRequirements(EHand Hand)
    {
        return Hand == EHand.eRightHand ? RightHandRequirements : LeftHandRequirements;
    }
}
