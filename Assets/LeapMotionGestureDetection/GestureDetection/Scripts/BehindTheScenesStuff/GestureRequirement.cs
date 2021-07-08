using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DataRequirements
{
    [SerializeField]
    string m_Label;

    [SerializeField]
    bool m_RequiresBoolField;

    [SerializeField]
    bool m_RequiresFloatField;

    [SerializeField]
    bool m_RequiresFingerField;

    [SerializeField]
    bool m_RequiresDirection;

    [SerializeField]
    bool m_RequiresHandAxis;

    [SerializeField]
    float m_FloatMin;

    [SerializeField]
    float m_FloatMax;

    public void SetBoolRequirement(bool a_Requires, string a_Label = "")
    {
        m_RequiresBoolField = a_Requires;
        m_Label = a_Label;
    }

    public void SetFloatRequirement(bool a_Requires, float a_Min = 0.0f, float a_Max = 1.0f)
    {
        m_RequiresFloatField = a_Requires;
        m_FloatMin = a_Min;
        m_FloatMax = a_Max;
    }

    public void SetFingerRequirement(bool a_Requires)
    {
        m_RequiresFingerField = a_Requires;
    }

    public void SetDirectionRequirement(bool a_Requires)
    {
        m_RequiresDirection = a_Requires;
    }

    public void SetHandAxisRequirement(bool a_Requires)
    {
        m_RequiresHandAxis = a_Requires;
    }

    public bool GetRequiresBool()
    {
        return m_RequiresBoolField;
    }

    public bool GetRequiresFloat()
    {
        return m_RequiresFloatField;
    }

    public string GetLabel()
    {
        return m_Label;
    }

    public bool GetRequiresFinger()
    {
        return m_RequiresFingerField;
    }

    public bool GetRequiresDirection()
    {
        return m_RequiresDirection;
    }

    public bool GetRequiresHandAxis()
    {
        return m_RequiresHandAxis;
    }

    public float GetFloatMin()
    {
        return m_FloatMin;
    }

    public float GetFloatMax()
    {
        return m_FloatMax;
    }
}

[System.Serializable]
public class GestureRequirement : ScriptableObject
{
    [HideInInspector]
    public DataRequirements m_DataRequirements;

    public virtual bool JustifiesRequirement(EHand Hand, GestureRequirementData RequirementData) { return false; }
    public virtual string GetName() { return GetType().ToString(); }
    public virtual void SetupRequirements() { }

    private void Awake()
    {
        SetupRequirements();
    }
}