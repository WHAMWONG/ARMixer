using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

partial class DetectionManager
{
    public class DetectionHand
    {
        DetectionFinger[] m_Fingers = new DetectionFinger[5];

        Hand m_Hand;

        void Start()
        {

        }

        public void SetHand(Hand a_Hand)
        {
            m_Hand = a_Hand;
            foreach (Finger finger in m_Hand.Fingers)
            {
                m_Fingers[(int)finger.Type] = new DetectionFinger();
                m_Fingers[(int)finger.Type].SetFinger(finger);
            }
        }

        public bool CheckWithDetails(FingerExtendedDetails fingerDetails)
        {
            return ((IsFingerExtended(EFinger.eThumb) == fingerDetails.bThumbExtended) &&
                    (IsFingerExtended(EFinger.eIndex) == fingerDetails.bIndexExtended) &&
                    (IsFingerExtended(EFinger.eMiddle) == fingerDetails.bMiddleExtended) &&
                    (IsFingerExtended(EFinger.eRing) == fingerDetails.bRingExtended) &&
                    (IsFingerExtended(EFinger.ePinky) == fingerDetails.bPinkeyExtended));
        }

        public int NumberOfFingersExtended()
        {
            int num = 0;

            for (int i = 0; i <= (int)EFinger.ePinky; i++)
            {
                EFinger finger = EFinger.eThumb + i;
                if(IsFingerExtended(finger))
                {
                    num++;
                }
            }

            return num;
        }

        public bool IsFingerExtended(EFinger a_finger)
        {
            return GetFinger(a_finger).IsExtended();
        }

        public bool IsSet()
        {
            foreach (DetectionFinger finger in m_Fingers)
            {
                if (finger != null)
                {
                    if(finger.GetFingerType() == EFinger.eUnknown)
                    {
                        return false;
                    }
                }
            }

            return m_Hand != null;
        }

        public bool IsClosed(float a_fTolerence)
        {
            return m_Hand.GetFistStrength() > a_fTolerence;
        }

        public bool IsPinching()
        {
            return m_Hand.IsPinching();
        }

        public float GetPinchDistance()
        {
            return m_Hand.PinchDistance;
        }

        public bool IsPinching(float a_fTolerence)
        {
            return m_Hand.PinchStrength > a_fTolerence;
        }

        public Vector3 GetDirectionToFingers()
        {
            return m_Hand.DistalAxis();
        }

        public Vector3 GetDirectionToThumb()
        {
            return m_Hand.RadialAxis();
        }

        public Vector3 GetDirectionAwayFromPalm()
        {
            return m_Hand.PalmarAxis();
        }

        public Vector3 GetPositionBetweenPinch()
        {
            return m_Hand.GetPinchPosition();
        }

        public Vector3 GetHandAxis(EHandAxis a_HandAxis)
        {
            switch (a_HandAxis)
            {
                case EHandAxis.ePalmDirection:
                    return m_Hand.PalmarAxis();

                case EHandAxis.eThumbDirection:
                    return m_Hand.RadialAxis();

                case EHandAxis.eFingerDirection:
                    return m_Hand.DistalAxis();

                default:
                    break;
            }

            return Vector3.zero;
        }

        public Vector3 GetHandPosition()
        {
            Vector pos = m_Hand.PalmPosition;
            return pos.ToVector3();
        }

        public Vector3 GetRelativeHandPosition()
        {
            Vector3 controllerPos = Get().GetLeapControllerTransform().position;

            return GetHandPosition() - controllerPos;
        }

        public Quaternion GetRotation()
        {
            return m_Hand.Rotation.ToQuaternion();
        }

        public Vector3 GetWristPosition()
        {
            return m_Hand.WristPosition.ToVector3();
        }

        public Vector3 GetVelocity()
        {
            return m_Hand.PalmVelocity.ToVector3();
        }

        public DetectionFinger GetFinger(EFinger a_FingerType)
        {
            return m_Fingers[(int)a_FingerType];
        }

        public float DistanceBetweenFingers(EFinger a_Finger0, EFinger a_Finger1)
        {
            if (a_Finger0 == a_Finger1)
            {
                return 0.0f;
            }
            else
            {
                Vector3 fingerPosition1 = GetFinger(a_Finger0).GetTipPosition();
                Vector3 fingerPosition2 = GetFinger(a_Finger1).GetTipPosition();
                return Vector3.Distance(fingerPosition1, fingerPosition2);
            }
        }

    }
}
