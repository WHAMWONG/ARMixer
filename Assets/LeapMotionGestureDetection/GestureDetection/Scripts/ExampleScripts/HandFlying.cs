using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandFlying : MonoBehaviour
{
    public EHand m_Hand;

    public GameObject mMovementObject;

    public float mHeightSpeed;
    public float mSpeedMultiplier;

    Vector3 handStartPos;

    bool handPositionSet = false;
    bool startPosSet = false;
    bool paused = false;

    Vector3 GetHandPosition(EHand a_Hand)
    {
        if (DetectionManager.Get().IsHandSet(m_Hand))
        {
            return DetectionManager.Get().GetHand(a_Hand).GetRelativeHandPosition();
        }

        return Vector3.zero;
    }

    public void SetStartPosition()
    {
        handStartPos = GetHandPosition(m_Hand);
        startPosSet = true;
    }

    public void StartMovement()
    {
        handPositionSet = true;
    }

    public void Pause()
    {
        paused = true;
    }

    public void UnPause()
    {
        paused = false;
    }

    public void StopMovement()
    {
        handPositionSet = false;
    }

    void Update()
    {
        if (handPositionSet && startPosSet && !paused)
        {
            Vector3 currentHandPos = GetHandPosition(m_Hand);

            float speedTranslate = mSpeedMultiplier * Time.deltaTime;

            Vector3 diff = currentHandPos - handStartPos;

            float forward = -diff.y;
            float right = diff.x;
            float up = diff.z;

            mMovementObject.transform.position += mMovementObject.transform.forward * forward * speedTranslate;
            mMovementObject.transform.position += mMovementObject.transform.right * right * speedTranslate;
            mMovementObject.transform.position += mMovementObject.transform.up * up * speedTranslate;
        }
    }
}
