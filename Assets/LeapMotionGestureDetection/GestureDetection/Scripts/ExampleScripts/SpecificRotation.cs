using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificRotation : MonoBehaviour
{
    public EHand m_Hand;
    public EFinger m_Finger;

    public GameObject m_Object;

    public float m_RotationSpeed = 1.0f;

    Vector3 StartPosition;

    void Start()
    {

    }

    void Update()
    {

    }

    Vector3 GetPosition()
    {
        if (DetectionManager.Get().IsHandSet(m_Hand))
        {
            return DetectionManager.Get().GetHand(m_Hand).GetFinger(m_Finger).GetTipPosition();
        }

        return Vector3.zero;
    }

    public void SetInitalPosition()
    {
        StartPosition = GetPosition();
    }

    public void UpdateRotation()
    {
        Vector3 torque = GetPosition() - StartPosition;

        Vector3 newTorque = Vector3.zero;

        newTorque.y = -torque.x;
        newTorque.x = torque.y;

        Vector3 rot = newTorque * m_RotationSpeed;

        m_Object.transform.Rotate(m_Object.transform.worldToLocalMatrix * rot);
    }
}
