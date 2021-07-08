using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementExample : MonoBehaviour
{
    public EHand m_Hand;

    public GameObject m_Object;

    public float m_Speed;

    Vector3 m_CurrentPosition;

    Vector3 m_LastPos;

    bool m_bTranslating = false;
    bool m_bPaused = false;

    void Start()
    {

    }

    void Update()
    {
        if (m_bTranslating && !m_bPaused && DetectionManager.Get().IsHandSet(m_Hand))
        {
            Vector3 newPos = GetHandPosition(m_Hand);
            Vector3 newAxis = GetHandAxis(m_Hand);

            Vector3 ChangeInDistance = newPos - m_LastPos;

            m_Object.transform.position += ChangeInDistance;
            m_Object.transform.rotation = Quaternion.Euler(newAxis);

            m_LastPos = newPos;
        }
    }


    Vector3 GetHandPosition(EHand a_Hand)
    {
        if (DetectionManager.Get().IsHandSet(m_Hand))
        {
            return DetectionManager.Get().GetHand(a_Hand).GetHandPosition();
        }

        return Vector3.zero;
    }

    public Vector3 GetHandAxis(EHand a_Hand)
    {
        if (DetectionManager.Get().IsHandSet(m_Hand))
        {
            return DetectionManager.Get().GetHand(a_Hand).GetHandAxis(EHandAxis.eFingerDirection);
        }

        return Vector3.zero;
    }

    public void SetForwardMovementStartPosition()
    {
        m_LastPos = GetHandPosition(m_Hand);
        m_bTranslating = true;
    }


    public void EndMovement()
    {
        m_bTranslating = false;
    }

    public void StopMovement()
    {
        m_bPaused = true;
    }

    public void StartMovement()
    {
        m_bPaused = false;
        m_LastPos = GetHandPosition(m_Hand);
    }

}
