using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClapGesture : GestureBase
{
    public float m_handDistance = 0.09f;
    [Range(0.1f, 6.0f)]
    public float m_SpeedThreshold = 1.5f;

    float m_CooldownTime = 0.2f;
    float m_CoolDownLeft = 0.0f;

    void Start()
    {

    }

    void Update()
    {
        if (m_CoolDownLeft > 0.0f)
        {
            m_CoolDownLeft -= Time.deltaTime;
            if (m_CoolDownLeft < 0.0f)
            {
                m_CoolDownLeft = 0.0f;
            }
        }
    }

    public override bool Detected()
    {

        if( DetectionManager.Get().AreBothHandsVisible() && m_CoolDownLeft <= 0.0f)
        {
            DetectionManager.DetectionHand leftHand = DetectionManager.Get().GetHand(EHand.eLeftHand);
            DetectionManager.DetectionHand rightHand = DetectionManager.Get().GetHand(EHand.eRightHand);

            if(leftHand.GetVelocity().magnitude >= m_SpeedThreshold &&
                rightHand.GetVelocity().magnitude >= m_SpeedThreshold)
            {
                if (DetectionManager.Get().GetDistanceBetweenHands() <= m_handDistance)
                {
                    m_CoolDownLeft = m_CooldownTime;
                    return true;
                }
            }

        }

        return false;
    }
}
